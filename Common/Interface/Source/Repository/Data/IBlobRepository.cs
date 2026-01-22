//-----------------------------------------------------------------------------
// <copyright file="IBlobRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the access to the Blob storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IBlobRepository : IRepository<BlobEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Cancel all draft content blobs.
        /// </summary>
        ///--------------------------------------------------------------------
        void CancelDrafts(
            BlobEntity blobEntity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the blob by its unique name.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobEntity GetByName(
            String name);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the blob by its content reference.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobEntity GetByBlobContent(
            BlobContentEntity blobContent);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the blob by a partial name.  This is the same as using a
        /// "Contains".
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<BlobEntity> GetAllByPartialName(
            String partialName);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Purge all blobs for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void PurgeAllByIdentity(
            IdentityEntity identity);
        #endregion
    }
}
