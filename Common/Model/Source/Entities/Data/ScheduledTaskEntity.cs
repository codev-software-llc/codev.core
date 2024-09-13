//-----------------------------------------------------------------------------
// <copyright file="ScheduledTaskEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This entity is used to represent a background task item.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ScheduledTaskEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTaskEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTaskEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTaskFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the time we should deal with the task.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant AttentionAt { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the task priority.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTaskPriorityType Priority { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the category for the task.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Category { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set type of the detail parameter.  Usually this is the type
        /// name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String DetailType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the task specific details.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Detail { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual IdentityEntity Identity { get; set; }
        #endregion
    }
}
