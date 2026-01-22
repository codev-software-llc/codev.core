//-----------------------------------------------------------------------------
// <copyright file="EntityConflictEventArgs.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the event argument for an entity override.
    /// </summary>
    ///------------------------------------------------------------------------
    public class EntityConflictEventArgs : EventArgs
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a URL with a User.TinyURL in its proper "namespace".
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityConflictEventArgs()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the event is handled by the listener.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsHandled { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity that is stale.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity StaleEntity { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity that is currently the persisted one.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity CurrentEntity { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set any user defined arguments to pass into the hander.
        /// </summary>
        ///--------------------------------------------------------------------
        public Object Arguments { get; set; }
        #endregion
    }
}
