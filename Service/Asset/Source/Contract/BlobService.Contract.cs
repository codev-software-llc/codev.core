//-----------------------------------------------------------------------------
// <copyright file="BlobService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Asset
{
    using System;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
    using Codev.Core.Repository.Ado;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Blob Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class BlobService : IBlobService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new blob.
        /// </summary>
        ///--------------------------------------------------------------------
        Blob IBlobService.Create(
            Reference<Identity> identityReference,
            String              blobName,
            String              mimeType,
            Byte[]              content,
            Boolean             isTemporary)
        {
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
                Validation.ValidateParameter<String>             ("blobName"         , blobName);
                Validation.ValidateParameter<String>             ("mimeType"         , mimeType);

                content = Validation.ValidateDefault<Byte[]>("content", content, new Byte[] { });

                Blob blob = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
        void IBlobService.Delete(
            Reference<Blob> blobReference)
        {
            try
            {
                Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                    if (blobEntity != null)
                    {
                        this.Delete(blobEntity);
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
        void IBlobService.Delete(
            Reference<Identity> identityReference)
        {
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        this.Delete(identityEntity);
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
        Blob IBlobService.GetByName(
            Reference<Identity> identityReference,
            String              blobName)
        {
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
                Validation.ValidateParameter<String>             ("blobName"         , blobName         );

                Blob blob = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        BlobEntity blobEntity = this.BlobRepository.GetByName(blobName);

                        if (blobEntity != null)
                        {
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
        BlobContent IBlobService.MakeDraft(
            Reference<Blob> blobReference)
        {
            try
            {
                Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

                BlobContent blobContent = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                    if (blobEntity != null)
                    {
                        blobContent = this.MakeDraft(blobEntity);
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
        void IBlobService.CancelDrafts(
            Reference<Blob> blobReference)
        {
            try
            {
                Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                    if (blobEntity != null)
                    {
                        this.CancelDrafts(blobEntity);
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
        BlobContent IBlobService.GetContents(
            Reference<Blob> blobReference)
        {
            try
            {
                Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);

                BlobContent blobContent = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    BlobEntity blobEntity = this.BlobRepository.GetById(blobReference.Id);

                    if (blobEntity != null)
                    {
                        BlobContentEntity blobContentEntity = this.BlobContentRepository.GetCurrent(blobEntity);

                        if (blobContentEntity != null)
                        {
                            blobContent = this.GetContents(blobContentEntity);
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
        BlobContent IBlobService.GetContents(
            Reference<BlobContent> blobContentReference)
        {
            try
            {
                Validation.ValidateParameter<Reference<BlobContent>>("blobContentReference", blobContentReference);

                BlobContent blobContent = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                    if (blobContentEntity != null)
                    {
                        blobContent = this.GetContents(blobContentEntity);
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
        void IBlobService.SetCurrent(
            Reference<BlobContent> blobContentReference)
        {
            try
            {
                Validation.ValidateParameter<Reference<BlobContent>>("blobContentReference", blobContentReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
        void IBlobService.Rename(
            Reference<Blob> blobReference,
            String          blobName)
        {
            try
            {
                Validation.ValidateParameter<Reference<Blob>>("blobReference", blobReference);
                Validation.ValidateParameter<String>         ("blobName"     , blobName     );

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
        void IBlobService.UpdateContents(
            Reference<BlobContent> blobContentReference,
            String                 mimeType,
            Byte[]                 content)
        {
            try
            {
                Validation.ValidateParameter<Reference<BlobContent>>("blobContentReference", blobContentReference);
                Validation.ValidateParameter<String>                ("mimeType"            , mimeType            );

                content = Validation.ValidateDefault<Byte[]>("content", content, new Byte[] { });

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    BlobContentEntity blobContentEntity = this.BlobContentRepository.GetById(blobContentReference.Id);

                    if (blobContentEntity != null)
                    {
                        this.UpdateContents(blobContentEntity, mimeType, content);
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
        #endregion
    }
}
