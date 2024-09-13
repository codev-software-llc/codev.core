//-----------------------------------------------------------------------------
// <copyright file="ScheduledTaskService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.ScheduledTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Codev.Core.Service.Clock;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IScheduledTaskService to schedule background work.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ScheduledTaskService : BaseService, IScheduledTaskService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public ScheduledTaskService(
            ICoreUnitOfWork          unitOfWork,
            IIdentityRepository      identityRepository,
            IScheduledTaskRepository taskRepository,
            IClockService            clockService) : base(unitOfWork)
        {
            Validation.ValidateParameter<IIdentityRepository>     ("identityRepository", identityRepository);
            Validation.ValidateParameter<IScheduledTaskRepository>("taskRepository"    , taskRepository    );
            Validation.ValidateParameter<IClockService>           ("clockService"      , clockService      );

            this.IdentityRepository = identityRepository;
            this.TaskRepository     = taskRepository;
            this.ClockService       = clockService;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the repository for identities.
        /// </summary>
        ///--------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the repository for persisting tasks.
        /// </summary>
        ///--------------------------------------------------------------------
        private IScheduledTaskRepository TaskRepository { get; set; }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the service for a clock.
        /// </summary>
        ///--------------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Cancel task.  This will update the cancelled flag regardless of
        /// the state of what the current state of the task is.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Cancel(
            ScheduledTaskEntity scheduledTaskEntity)
        {
            this.TaskRepository.Purge(scheduledTaskEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the scheduled task using the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public TaskJob Get(
            ScheduledTaskEntity scheduledTaskEntity)
        {
            return scheduledTaskEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Schedule a new task.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Schedule(
            IdentityEntity            identityEntity,
            LocalDateTime             nextAttentionAt,
            ScheduledTaskPriorityType priority,
            String                    category,
            String                    detailType,
            String                    detail)
        {
            Instant instant          = this.ClockService.GetCurrentInstant();
            Instant instantScheduled = nextAttentionAt.ToInstant(identityEntity.TimeZone);

            ScheduledTaskEntity entity = new ScheduledTaskEntity(instant)
                {
                    Identity    = identityEntity,
                    Flags       = ScheduledTaskFlags.Idle,
                    AttentionAt = instantScheduled,
                    Priority    = priority,
                    Category    = category,
                    DetailType  = detailType,
                    Detail      = detail
                };

            this.TaskRepository.Add(entity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Run any tasks that require attention.  This will run only those
        /// tasks of the specified priority.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Run(
            ScheduledTaskPriorityType               priority,
            Action<TaskJob, ScheduledTaskEventArgs> clientWorker)
        {           
            // Get the tasks that conform to the priority.
            //
            List<ScheduledTaskEntity> tasks = this.GetPriorityTasks(priority);

            DateTimeZone dtz = this.ClockService.GetDefaultTimeZone();

            // Run the task.  Make a callback to the listeners to do work.
            //
            foreach (ScheduledTaskEntity task in tasks)
            {
                // Invoke worker (caller) to do the work.  The callee
                // can then adjust (reschedule) the task if it deems
                // it not complete.
                //
                if (clientWorker != null)
                {
                    this.Reserve(task);

                    ScheduledTaskEventArgs tea = new ScheduledTaskEventArgs()
                        {
                            IsCanceled      = false,
                            NextAttentionAt = task.AttentionAt.ToLocalDateTime(dtz)
                        };

                    try
                    {
                        clientWorker(task.ToModel(), tea);
                    }
                    catch (Exception)
                    {
                        //
                        // Log Error.
                        //
                    }

                    task.AttentionAt = tea.IsCanceled || (tea.NextAttentionAt >= NodaExtensions.LocalDateTimeMaxValue) ? NodaExtensions.InstantMaxValue : tea.NextAttentionAt.ToInstant(dtz);

                    this.Release(task);  
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Run any tasks that require attention.  This will run only those
        /// tasks of the specified priority.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Run(
            String                                  category,
            ScheduledTaskPriorityType               priority,
            Action<TaskJob, ScheduledTaskEventArgs> clientWorker)
        {
            List<ScheduledTaskEntity> tasks = this.GetCategorizedTasks(category, priority);

            DateTimeZone dtz = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault();

            // Run the task.  Make a callback to the listeners to do work.
            //
            foreach (ScheduledTaskEntity task in tasks)
            {
                // Invoke worker (caller) to do the work.  The callee
                // can then adjust (reschedule) the task if it deems
                // it not complete.
                //
                if (clientWorker != null)
                {
                    this.Reserve(task);

                    ScheduledTaskEventArgs tea = new ScheduledTaskEventArgs()
                        {
                            IsCanceled      = false,
                            NextAttentionAt = task.AttentionAt.ToLocalDateTime(dtz)
                        };

                    try
                    {
                        clientWorker(task.ToModel(), tea);
                    }
                    catch (Exception)
                    {
                        //
                        // Log Error.
                        //
                    }

                    task.AttentionAt = tea.IsCanceled || (tea.NextAttentionAt >= NodaExtensions.LocalDateTimeMaxValue) ? NodaExtensions.InstantMaxValue : tea.NextAttentionAt.ToInstant(dtz);

                    this.Release(task);
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Purge all tasks which are no longer requiring work.
        /// </summary>
        ///--------------------------------------------------------------------
        public void GarbageCollect()
        {
            // Get all the tasks that have their next attention set to 
            // infinity.
            //
            List<ScheduledTaskEntity> tasks = this.TaskRepository.GetAll().Where(x => (x.AttentionAt == Instant.MaxValue)).ToList();

            tasks.ForEach(x => this.TaskRepository.Purge(x));
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get all tasks that meet the category tag and are scheduled for
        /// pickup.
        /// </summary>
        ///--------------------------------------------------------------------
        private List<ScheduledTaskEntity> GetCategorizedTasks(
            String                    category,
            ScheduledTaskPriorityType priority)
        {
            List<ScheduledTaskEntity> scheduledTasks = new List<ScheduledTaskEntity>();

            Instant instant = this.ClockService.GetCurrentInstant();

            // Get all the tasks needing attention.
            //
            if (priority == ScheduledTaskPriorityType.Any)
            {
                scheduledTasks = this.TaskRepository.GetAll().Where(x => (x.AttentionAt.CompareTo(instant) < 1) && (x.Flags.HasFlag(ScheduledTaskFlags.Reserved) == false) && (x.Category == category)).ToList();
            }
            else
            {
                scheduledTasks = this.TaskRepository.GetAll().Where(x => (x.AttentionAt.CompareTo(instant) < 1) && (x.Flags.HasFlag(ScheduledTaskFlags.Reserved) == false) && (x.Category == category) && (x.Priority == priority)).ToList();
            }

            return scheduledTasks;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all outstanding tasks based on the priority.
        /// </summary>
        ///--------------------------------------------------------------------
        private List<ScheduledTaskEntity> GetPriorityTasks(
            ScheduledTaskPriorityType priority)
        {
            List<ScheduledTaskEntity> scheduledTasks = new List<ScheduledTaskEntity>();

            Instant instant = this.ClockService.GetCurrentInstant();

            // Get all the tasks needing attention.
            //
            if (priority == ScheduledTaskPriorityType.Any)
            {
                scheduledTasks = this.TaskRepository.GetAll().Where(x => (x.AttentionAt.CompareTo(instant) < 1) && (x.Flags.HasFlag(ScheduledTaskFlags.Reserved) == false) && (x.Priority == priority)).ToList();
            }
            else
            {
                scheduledTasks = this.TaskRepository.GetAll().Where(x => (x.AttentionAt.CompareTo(instant) < 1) && (x.Flags.HasFlag(ScheduledTaskFlags.Reserved) == false)).ToList();
            }

            return scheduledTasks;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Reserve the task to do work on it.  This prevents anyone from 
        /// performing work on the same task.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Reserve(
            ScheduledTaskEntity task)
        {
            task.Flags |= ScheduledTaskFlags.Reserved;

            task.DateModified = this.ClockService.GetCurrentInstant();

            this.TaskRepository.Update(task);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Complete the task.  This will mark it as never needing attention
        /// again.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Release(
            ScheduledTaskEntity task)
        {
            task.Flags &= ~ScheduledTaskFlags.Reserved;

            task.DateModified = this.ClockService.GetCurrentInstant();
            
            this.TaskRepository.Update(task);
        }
        #endregion
    }
}