//-----------------------------------------------------------------------------
// <copyright file="AssetService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
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

            try
            {
                content = Validation.ValidateDefault<Byte[]>("content", content, new Byte[] { });

                Blob blob = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        BlobEntity blobEntity = this.BlobRepository.GetByName(blobName);

                        if (blobEntity == null)
                        {
                            blob = this.Create(identityEntity, blobName, mimeType, content, isTemporary);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.Duplicate, ExceptionMessage.BlobAlreadyExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return blob;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
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

                    work.Commit();
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        this.BlobRepository.PurgeAllByIdentity(identityEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
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
                Blob blob = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        BlobEntity blobEntity = this.BlobRepository.GetByName(blobName);

                        if (blobEntity != null)
                        {
                            blobEntity.Identity = identityEntity;

                            blob = this.GetByName(blobEntity);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return blob;
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
                BlobContent blobContent = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                    if (blobEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByBlob(blobEntity);

                        if (identityEntity != null)
                        {
                            blobEntity.Identity = identityEntity;

                            blobContent = this.MakeDraft(blobEntity);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return blobContent;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
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

                    work.Commit();
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
                BlobContent blobContent = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                    if (blobEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByBlob(blobEntity);

                        if (identityEntity != null)
                        {
                            BlobContentEntity blobContentEntity = this.BlobContentRepository.GetCurrent(blobEntity);

                            if (blobContentEntity != null)
                            {
                                blobEntity.Identity = identityEntity;

                                blobContentEntity.Blob = blobEntity;
                                
                                blobContent = this.GetContents(blobContentEntity);
                            }
                            else
                            {
                                throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Blob content does not exist");
                            }
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return blobContent;
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
                BlobContent blobContent = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                    if (blobContentEntity != null)
                    {
                        BlobEntity blobEntity = this.BlobRepository.GetByBlobContent(blobContentEntity);

                        if (blobEntity != null)
                        {
                            IdentityEntity identityEntity = this.IdentityRepository.GetByBlob(blobEntity);

                            if (identityEntity != null)
                            {
                                blobEntity.Identity = identityEntity;

                                blobContentEntity.Blob = blobEntity;

                                blobContent = this.GetContents(blobContentEntity);
                            }
                            else
                            {
                                throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                            }
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Blob does not exist");
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Blob Content does not exist");
                    }

                    work.Commit();
                }

                return blobContent;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                    if (blobContentEntity != null)
                    {
                        BlobEntity blobEntity = this.BlobRepository.GetByBlobContent(blobContentEntity);

                        if (blobEntity != null)
                        {
                            blobContentEntity.Blob = blobEntity;

                            this.SetCurrent(blobContentEntity);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                    }

                    work.Commit();
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                    if (blobEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByBlob(blobEntity);

                        if (identityEntity != null)
                        {
                            blobEntity.Identity = identityEntity;

                            this.Rename(blobEntity, blobName);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                    }

                    work.Commit();
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                    if (blobContentEntity != null)
                    {
                        BlobEntity blobEntity = this.BlobRepository.GetByBlobContent(blobContentEntity);

                        if (blobEntity != null)
                        {
                            IdentityEntity identityEntity = this.IdentityRepository.GetByBlob(blobEntity);

                            if (identityEntity != null)
                            {
                                blobEntity.Identity = identityEntity;

                                blobContentEntity.Blob = blobEntity;

                                this.UpdateContents(blobContentEntity, mimeType, content);
                            }
                            else
                            {
                                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                            }
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.BlobDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Blob content does not exist");
                    }

                    work.Commit();
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
