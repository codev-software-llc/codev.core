//-----------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the interface of which all entities implement.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IEntity
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the entity unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        Int32 Id { get; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This represents the RowVersion of the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        Byte[] Version { get; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the entity is active or not.  We differentiate
        /// this from a standard entity property in that it is required for
        /// all entities.
        /// </summary>
        ///--------------------------------------------------------------------
        Boolean IsActive { get; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the creation date of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        Instant DateCreated { get; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date the entity has been touched.
        /// </summary>
        ///--------------------------------------------------------------------
        Instant DateModified { get; }
        #endregion
    }
}