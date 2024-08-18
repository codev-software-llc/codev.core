//-----------------------------------------------------------------------------
// <copyright file="ScheduledTask.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;
    using Codev.Core.Common.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the ScheduledTask model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ScheduledTask : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTask() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the key.
        /// </summary>
        ///--------------------------------------------------------------------
        public Guid Key { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date created.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateCreated { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date modified.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateModified { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the next attention.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime NextAttentionAt { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the priority.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTaskPriorityType Priority { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the category.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Category { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the detail type.
        /// </summary>
        ///--------------------------------------------------------------------
        public String DetailType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the detail.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Detail { get; set; }
        #endregion
    }
}
