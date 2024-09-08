//-----------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This represents the base of all entity objects.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BaseEntity : IEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity(
            Int32   rowId,
            Byte[]  rowVersion,
            Boolean isActive,
            Instant dateCreated,
            Instant dateModified)
        {
            this.Id           = rowId;
            this.Version      = rowVersion;
            this.IsActive     = isActive;
            this.DateCreated  = dateCreated;
            this.DateModified = dateModified;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity(
            BaseEntity baseEntity)
        {
            this.Id           = baseEntity.Id;
            this.Version      = baseEntity.Version;
            this.IsActive     = baseEntity.IsActive;
            this.DateCreated  = baseEntity.DateCreated;
            this.DateModified = baseEntity.DateModified;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseEntity(
            Instant instantNow) : this(0, new Byte[] { }, true, instantNow, instantNow)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// This represents the RowId of the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 Id { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This represents the RowVersion of the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Byte[] Version { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the entity is active or not.  We differentiate
        /// this from a standard entity property in that it is required for
        /// all entities.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsActive { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the creation date of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant DateCreated { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date the entity has been touched.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant DateModified { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Reset the entity with the latest identifier and version.
        /// </summary>
        ///--------------------------------------------------------------------
        public void ResetEntityState(
            Int32  rowId,
            Byte[] rowVersion)
        {
            this.Id      = rowId;
            this.Version = rowVersion;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Reset the entity with the latest identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public void ResetEntityState(
            Int32 rowId)
        {
            this.Id      = rowId;
            this.Version = new Byte[] { };
        }
        #endregion
    }
}
