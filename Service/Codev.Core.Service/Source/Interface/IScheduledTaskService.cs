//-----------------------------------------------------------------------------
// <copyright file="IScheduledTaskService.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.ScheduledTask
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the means to manage scheduled tasks.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IScheduledTaskService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Cancel task.
        /// </summary>
        ///--------------------------------------------------------------------
        void Cancel(
            Reference<TaskJob> scheduledTaskReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the task using the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        TaskJob Get(
            Reference<TaskJob> scheduledTaskReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get all scheduled tasks for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<TaskJob> GetAll(
            Reference<Identity> identityReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Schedule a new task.
        /// </summary>
        ///--------------------------------------------------------------------
        void Schedule(
            Reference<Identity>       identityReference,
            LocalDateTime             nextAttentionAt,
            ScheduledTaskPriorityType priority,
            String                    category,
            String                    detailTypeName,
            String                    serializedDetail);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Run tasks.  The caller will be invoked upon the running of the
        /// task.
        /// </summary>
        ///--------------------------------------------------------------------
        void Run(
            ScheduledTaskPriorityType               priority,
            Action<TaskJob, ScheduledTaskEventArgs> clientWorker);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Run tasks.  The caller will be invoked upon the running of the
        /// task.
        /// </summary>
        ///--------------------------------------------------------------------
        void Run(
            String                                  category,
            ScheduledTaskPriorityType               priority,
            Action<TaskJob, ScheduledTaskEventArgs> clientWorker);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clean up any tasks that are obsolete, complete or cancelled.
        /// </summary>
        ///--------------------------------------------------------------------
        void GarbageCollect();
        #endregion
    }
}
