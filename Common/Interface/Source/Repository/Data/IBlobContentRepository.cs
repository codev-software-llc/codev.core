//-----------------------------------------------------------------------------
// <copyright file="IBlobConentRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the access to the Blob Content storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IBlobContentRepository : IRepository<BlobContentEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all the contents for the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<BlobContentEntity> GetAllByBlob(
            BlobEntity blob);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all the current blob contents.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobContentEntity GetCurrent(
            BlobEntity blob);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the current blob and remove all drafts.
        /// </summary>
        ///--------------------------------------------------------------------
        void SetCurrent(
            BlobContentEntity blobContent);
        #endregion
    }
}
