//-----------------------------------------------------------------------------
// <copyright file="IBlobService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Asset
{
    using System;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This service provides for blob (unstructured) storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IBlobService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new blob with contents.
        /// </summary>
        ///--------------------------------------------------------------------
        Blob Create(
            Reference<Identity> identityReference,
            String              blobName,
            String              mimeType,
            Byte[]              content,
            Boolean             isTemporary = false);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the blob and any content it may have.
        /// </summary>
        ///--------------------------------------------------------------------
        void Delete(
            Reference<Blob> blobReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete all blobs for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void Delete(
            Reference<Identity> identityReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate a blob by its name.
        /// </summary>
        ///--------------------------------------------------------------------
        Blob GetByName(
            Reference<Identity> identityReference,
            String              blobName);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a draft content of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobContent MakeDraft(
            Reference<Blob> blobReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Cancel the draft.
        /// </summary>
        ///--------------------------------------------------------------------
        void CancelDrafts(
            Reference<Blob> blobReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the contents of the blob.  This returns only the current
        /// main content blob.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobContent GetContents(
            Reference<Blob> blobReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the contents of the blob.  This returns the explicit 
        /// contents.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobContent GetContents(
            Reference<BlobContent> blobContentReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Rename the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        void Rename(
            Reference<Blob> blobReference,
            String          blobName);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the blob contents to be the current blob.  Any other (draft)
        /// blobs are removed.
        /// </summary>
        ///--------------------------------------------------------------------
        void SetCurrent(
            Reference<BlobContent> blobContentReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the blob contents.
        /// </summary>
        ///--------------------------------------------------------------------
        void UpdateContents(
            Reference<BlobContent> blobContentReference,
            String                 mimeType,
            Byte[]                 blobContent);
        #endregion
    }
}
