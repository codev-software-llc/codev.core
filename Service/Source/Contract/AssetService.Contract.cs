//-----------------------------------------------------------------------------
// <copyright file="AssetService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Asset
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Blob Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class AssetService : IAssetService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new blob.
        /// </summary>
        ///--------------------------------------------------------------------
        Blob IAssetService.Create(
            Reference<Identity> identityReference,
            String              blobName,
            String              mimeType,
            Byte[]              content,
            Boolean             isTemporary)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<String>             ("blobName"         , blobName         );
            Validation.ValidateParameter<String>             ("mimeType"         , mimeType         );

            content = Validation.ValidateDefault<Byte[]>("content", content, new Byte[] { });

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    BlobEntity blobEntity = this.BlobRepository.GetByName(blobName);

                    if (blobEntity == null)
                    {
                        return this.Create(identityEntity, blobName, mimeType, content, isTemporary);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.Duplicate, ExceptionMessage.BlobAlreadyExistMessage);
                    }
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAssetService.Delete(
            Reference<Blob> blobReference)
        {
            Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

            try
            {
                BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                if (blobEntity != null)
                {
                    this.BlobRepository.Purge(blobEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete all blobs for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAssetService.Delete(
            Reference<Identity> identityReference)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    this.BlobRepository.PurgeAllByIdentity(identityEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate a blob by its name.
        /// </summary>
        ///--------------------------------------------------------------------
        Blob IAssetService.GetByName(
            Reference<Identity> identityReference,
            String              blobName)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<String>             ("blobName"         , blobName         );

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    BlobEntity blobEntity = this.BlobRepository.GetByName(blobName);

                    if (blobEntity != null)
                    {
                        return this.GetByName(blobEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                    }
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Make a draft slot for the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobContent IAssetService.MakeDraft(
            Reference<Blob> blobReference)
        {
            Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

            try
            {
                BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                if (blobEntity != null)
                {
                    return this.MakeDraft(blobEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Cancel all drafts.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAssetService.CancelDrafts(
            Reference<Blob> blobReference)
        {
            Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

            try
            {
                BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                if (blobEntity != null)
                {
                    this.BlobRepository.CancelDrafts(blobEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the contents of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobContent IAssetService.GetContents(
            Reference<Blob> blobReference)
        {
            Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

            try
            {
                BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                if (blobEntity != null)
                {
                    BlobContentEntity blobContentEntity = this.BlobContentRepository.GetCurrent(blobEntity);

                    if (blobContentEntity != null)
                    {
                        return this.GetContents(blobContentEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Blob content does not exist");
                    }
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the contents of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        BlobContent IAssetService.GetContents(
            Reference<BlobContent> blobContentReference)
        {
            Validation.ValidateParameter<Reference<BlobContent>>("blobContentReference", blobContentReference);

            try
            {
                BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                if (blobContentEntity != null)
                {
                    return this.GetContents(blobContentEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Blob Content does not exist");
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the blob contents as the current.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAssetService.SetCurrent(
            Reference<BlobContent> blobContentReference)
        {
            Validation.ValidateParameter<Reference<BlobContent>>("blobContentReference", blobContentReference);

            try
            {
                BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                if (blobContentEntity != null)
                {
                    this.SetCurrent(blobContentEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Rename the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAssetService.Rename(
            Reference<Blob> blobReference,
            String          blobName)
        {
            Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);
            Validation.ValidateParameter<String>         ("blobName"     , blobName     );

            try
            {

                BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                if (blobEntity != null)
                {
                    this.Rename(blobEntity, blobName);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the blob contents.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAssetService.UpdateContents(
            Reference<BlobContent> blobContentReference,
            String                 mimeType,
            Byte[]                 content)
        {
            Validation.ValidateParameter<Reference<BlobContent>>("blobContentReference", blobContentReference);
            Validation.ValidateParameter<String>                ("mimeType"            , mimeType            );

            content = Validation.ValidateDefault<Byte[]>("content", content, new Byte[] { });

            try
            {
                BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                if (blobContentEntity != null)
                {
                    this.UpdateContents(blobContentEntity, mimeType, content);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Blob content does not exist");
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
