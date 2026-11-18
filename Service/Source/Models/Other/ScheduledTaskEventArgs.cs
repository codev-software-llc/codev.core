//-----------------------------------------------------------------------------
// <copyright file="ScheduledTaskEventArgs.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This argument is used for notifications on the Task Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ScheduledTaskEventArgs : EventArgs
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTaskEventArgs()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Get or set the next attention to invoke the task.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime NextAttentionAt { get; set; }

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Get or set whether the task is complete.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsCanceled { get; set; }
        #endregion
    }
}