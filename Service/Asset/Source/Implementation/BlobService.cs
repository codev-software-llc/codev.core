//-----------------------------------------------------------------------------
// <copyright file="BlobService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Asset
{
    using System;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;
    using Codev.Core.Service.Common;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IBlobService interface for storage of blobs.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class BlobService : BaseService, IBlobService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the cache client.
        /// </summary>
        ///---------------------------------------------------------------
        public BlobService(
            ICoreDataSource        dataSource,
            IIdentityRepository    identityRepository,
            IBlobRepository        blobRepository,
            IBlobContentRepository blobContentRepository,
            IClockService          clockService) : base(dataSource)
        {
            Validation.ValidateParameter<IIdentityRepository>   ("identityRepository", identityRepository   );
            Validation.ValidateParameter<IBlobRepository>       ("blobRepository"    , blobRepository       );
            Validation.ValidateParameter<IBlobContentRepository>("blobRepository"    , blobContentRepository);
            Validation.ValidateParameter<IClockService>         ("clockService"      , clockService         );

            this.IdentityRepository    = identityRepository;
            this.BlobRepository        = blobRepository;
            this.BlobContentRepository = blobContentRepository;
            this.ClockService          = clockService;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the identity.
        /// </summary>
        ///---------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the store.
        /// </summary>
        ///---------------------------------------------------------------
        private IBlobRepository BlobRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the blob content store.
        /// </summary>
        ///---------------------------------------------------------------
        private IBlobContentRepository BlobContentRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        ///---------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public Blob Create(
            IdentityEntity identityEntity,
            String         blobName,
            String         mimeType,
            Byte[]         blobContent,
            Boolean        isTemporary)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            BlobEntity entity = new BlobEntity(instantNow)
                {
                    Identity = identityEntity,
                    Flags    = isTemporary ? BlobFlags.Temporary : BlobFlags.None,
                    Name     = blobName,
                    MimeType = mimeType,
                    Size     = blobContent.Length
                };

            this.BlobRepository.Add(entity);

            BlobContentEntity blobContentEntity = new BlobContentEntity(instantNow)
                {
                    Blob     = entity,
                    Flags    = BlobContentFlags.Current,
                    MimeType = mimeType,
                    Size     = blobContent.Length,
                    Content  = blobContent
                };

            this.BlobContentRepository.Add(blobContentEntity);

            return entity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Delete(
            BlobEntity blobEntity)
        {
            this.BlobRepository.Purge(blobEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete all blobs for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Delete(
            IdentityEntity identityEntity)
        {
            this.BlobRepository.PurgeAllByIdentity(identityEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate blob by name.
        /// </summary>
        ///--------------------------------------------------------------------
        public Blob GetByName(
            BlobEntity blobEntity)
        {
            return blobEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a draft entry for the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public BlobContent MakeDraft(
            BlobEntity blobEntity)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            BlobContentEntity blobContentEntity = new BlobContentEntity(instantNow)
                {
                    Blob     = blobEntity,
                    Flags    = BlobContentFlags.None,
                    MimeType = String.Empty,
                    Size     = 0,
                    Content  = new Byte[] { }
                };

            this.BlobContentRepository.Add(blobContentEntity);

            return blobContentEntity.ToModel();
        }
        
        ///--------------------------------------------------------------------
        /// <summary>
        /// Cancel all drafts.
        /// </summary>
        ///--------------------------------------------------------------------
        public void CancelDrafts(
            BlobEntity blobEntity)
        {
            this.BlobRepository.CancelDrafts(blobEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the contents of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public BlobContent GetContents(
            BlobContentEntity blobContentEntity)
        {
            return blobContentEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Rename the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Rename(
            BlobEntity blobEntity,
            String     blobName)
        {
            blobEntity.DateModified = this.ClockService.GetCurrentInstant();
            blobEntity.Name         = blobName;

            this.BlobRepository.Update(blobEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the blob contents to be the current blob.  Any other (draft)
        /// blobs are removed.
        /// </summary>
        ///--------------------------------------------------------------------
        public void SetCurrent(
            BlobContentEntity blobContentEntity)
        {
            blobContentEntity.DateModified = this.ClockService.GetCurrentInstant();
            blobContentEntity.Flags        = BlobContentFlags.Current;

            this.BlobContentRepository.SetCurrent(blobContentEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the blob contents.
        /// </summary>
        ///--------------------------------------------------------------------
        public void UpdateContents(
            BlobContentEntity blobContentEntity,
            String            mimeType,
            Byte[]            content)
        {
            blobContentEntity.DateModified = this.ClockService.GetCurrentInstant();
            blobContentEntity.MimeType     = mimeType;
            blobContentEntity.Size         = content.Length;
            blobContentEntity.Content      = content;

            this.BlobContentRepository.Update(blobContentEntity);
        }
        #endregion
    }
}
