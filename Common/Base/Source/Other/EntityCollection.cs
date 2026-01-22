//-----------------------------------------------------------------------------
// <copyright file="EntityCollection.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Collections.Generic;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains a class to track collection changes.
    /// </summary>
    ///------------------------------------------------------------------------
    public class EntityCollection<T> : HashSet<T>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// This construct the collection.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection() : base(new EntityComparer<T>())
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This construct the collection.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection(
            IEnumerable<T> enumerable) : base(enumerable, new EntityComparer<T>())
        {
        }
        #endregion

        #region Events
        ///--------------------------------------------------------------------
        /// <summary>
        /// This event allows a listener to be notified of an entity being
        /// reset.
        /// </summary>
        ///--------------------------------------------------------------------
        public delegate void CollectionChangedEventHandler(
            Object                    sender,
            EntityCollectionEventArgs e);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This event allows a listener to be notified of a persistence
        /// conflict.
        /// </summary>
        ///--------------------------------------------------------------------
        public event CollectionChangedEventHandler CollectionChanged;
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the owner of the collection.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity Owner { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or sets whether the entity collection has been dirtied.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsDirty { get; private set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add entity to the collection.
        /// </summary>
        ///--------------------------------------------------------------------
        public new void Add(
            T entity)
        {
            BaseEntity baseEntity = entity as BaseEntity;

            if (baseEntity != null)
            {
                this.IsDirty = true;

                base.Add(entity);

                // Notify any listeners that a change was made to the 
                // collection.
                //
                if (this.CollectionChanged != null)
                {
                    this.CollectionChanged(this, new EntityCollectionEventArgs(this.Owner, baseEntity));
                }
            }
            else
            {
                throw new InvalidCastException("Item is not derived from a BaseEntity");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove entity from collection.
        /// </summary>
        ///--------------------------------------------------------------------
        public new void Remove(
            T entity)
        {
            BaseEntity baseEntity = entity as BaseEntity;

            if (baseEntity != null)
            {
                this.IsDirty = true;

                base.Remove(entity);

                // Notifiy the collection listener that a change was made to the
                // collection.
                //
                if (this.CollectionChanged != null)
                {
                    this.CollectionChanged(this, new EntityCollectionEventArgs(this.Owner, baseEntity));
                }
            }
            else
            {
                throw new InvalidCastException("Item is not derived from a BaseEntity");
            }
        }
        #endregion
    }
}
