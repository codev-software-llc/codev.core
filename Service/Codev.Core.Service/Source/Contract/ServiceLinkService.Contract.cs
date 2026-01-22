//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.ServiceLink
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains the explicit interface hooks that we will use to perform
    /// call validation on the parameters.  Each of these methods will in-turn
    /// call the actual implementations.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ServiceLinkService : IServiceLinkService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a service link.
        /// </summary>
        ///--------------------------------------------------------------------
        ServiceJob IServiceLinkService.Add(
            Identity identity,
            String   detailTypeName,
            String   serializedDetail)
        {
            Validation.ValidateParameter<Identity>("identity"        , identity        );
            Validation.ValidateParameter<String>  ("detailTypename"  , detailTypeName  );
            Validation.ValidateParameter<String>  ("serializedDetail", serializedDetail);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

                if (identityEntity != null)
                {
                    return this.Add(identityEntity, detailTypeName, serializedDetail);
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
        /// Delete all service links by the shared key.
        /// </summary>
        ///--------------------------------------------------------------------
        void IServiceLinkService.DeleteAll(
            Identity identity)
        {
            Validation.ValidateParameter<Identity>("identity", identity);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

                if (identityEntity != null)
                {
                    this.DeleteAll(identityEntity);
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
        /// Get the service link by tiny url.
        /// </summary>
        ///--------------------------------------------------------------------
        ServiceJob IServiceLinkService.GetByTinyUrl(
            String tinyUrl)
        {
            Validation.ValidateParameter<String>("tinyUrl", tinyUrl);

            try
            {
                ServiceLinkEntity serviceLinkEntity = this.ServiceLinkRepository.GetByTinyUrl(tinyUrl);

                if (serviceLinkEntity != null)
                {
                    return serviceLinkEntity.ToModel();
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ServiceLinkDoesNotExistMessage);
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
        /// Get all service links.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ServiceJob> IServiceLinkService.GetAll()
        {
            try
            {
                EntityCollection<ServiceLinkEntity> entities = this.ServiceLinkRepository.GetAll();

                return entities.Select(x => x.ToModel()).ToList();
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
        /// Get the service links by identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ServiceJob> IServiceLinkService.GetAll(
            Identity identity)
        {
            Validation.ValidateParameter<Identity>("identity", identity);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

                if (identityEntity != null)
                {
                    EntityCollection<ServiceLinkEntity> entities = this.ServiceLinkRepository.GetAllByIdentity(identityEntity);

                    return entities.Select(x =>x.ToModel()).ToList();
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
        /// Remove the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        void IServiceLinkService.Remove(
            ServiceJob serviceLink)
        {
            Validation.ValidateParameter<ServiceJob>("serviceLink", serviceLink);

            try
            {
                ServiceLinkEntity serviceLinkEntity = this.ServiceLinkRepository.GetById(serviceLink.Id);

                if (serviceLinkEntity != null)
                {
                    this.Remove(serviceLinkEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ServiceLinkDoesNotExistMessage);
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
        /// Update the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        void IServiceLinkService.Update(
            ServiceJob serviceLink,
            String     detailTypeName,
            String     serializedDetail)
        {
            Validation.ValidateParameter<ServiceJob>("serviceLink"     , serviceLink     );
            Validation.ValidateParameter<String>    ("detailTypeName"  , detailTypeName  );
            Validation.ValidateParameter<String>    ("serializedDetail", serializedDetail);

            try
            {
                ServiceLinkEntity serviceLinkEntity = this.ServiceLinkRepository.GetById(serviceLink.Id);

                if (serviceLinkEntity != null)
                {
                    this.Update(serviceLinkEntity, detailTypeName, serializedDetail);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ServiceLinkDoesNotExistMessage);
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
