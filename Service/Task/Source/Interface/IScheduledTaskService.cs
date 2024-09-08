//-----------------------------------------------------------------------------
// <copyright file="IScheduledTaskService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Task
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
            ScheduledTask task);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the task using the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        ScheduledTask Get(
            Int32 id);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get all scheduled tasks for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ScheduledTask> GetAll(
            Identity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Schedule a new task.
        /// </summary>
        ///--------------------------------------------------------------------
        void Schedule(
            Identity                  identity,
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
            ScheduledTaskPriorityType                     priority,
            Action<ScheduledTask, ScheduledTaskEventArgs> clientWorker);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Run tasks.  The caller will be invoked upon the running of the
        /// task.
        /// </summary>
        ///--------------------------------------------------------------------
        void Run(
            String                                        category,
            ScheduledTaskPriorityType                     priority,
            Action<ScheduledTask, ScheduledTaskEventArgs> clientWorker);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clean up any tasks that are obsolete, complete or cancelled.
        /// </summary>
        ///--------------------------------------------------------------------
        void GarbageCollect();
        #endregion
    }
}
