//-----------------------------------------------------------------------------
// <copyright file="EntityCollectionEventArgs.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This argument is used for notifications from the EntityCollection
    /// object.
    /// </summary>
    ///------------------------------------------------------------------------
    public class EntityCollectionEventArgs : EventArgs
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollectionEventArgs(
            BaseEntity owner,
            BaseEntity entity)
        {
            this.Owner  = owner;
            this.Entity = entity;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Get or set the owner of the collection.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity Owner { get; private set; }

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Get or set the entity that changed.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity Entity { get; private set; }
        #endregion
    }
}