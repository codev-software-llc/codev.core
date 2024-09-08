//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
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
        ServiceLink IServiceLinkService.Add(
            Identity identity,
            String   detailTypeName,
            String   serializedDetail)
        {
            try
            {
                Validation.ValidateParameter<Identity>("identity"        , identity        );
                Validation.ValidateParameter<String>  ("detailTypename"  , detailTypeName  );
                Validation.ValidateParameter<String>  ("serializedDetail", serializedDetail);

                ServiceLink serviceLink = null;

                using (this.UnitOfWork.Begin())
                {
                    serviceLink = this.Add(identity, detailTypeName, serializedDetail);

                    this.UnitOfWork.Commit();
                }

                return serviceLink;
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
            try
            {
                Validation.ValidateParameter<Identity>("identity", identity);

                using (this.UnitOfWork.Begin())
                {
                    this.DeleteAll(identity);

                    this.UnitOfWork.Commit();
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
        ServiceLink IServiceLinkService.GetByTinyUrl(
            String tinyUrl)
        {
            try
            {
                Validation.ValidateParameter<String>("tinyUrl", tinyUrl);

                ServiceLink serviceLink = null;

                using (this.UnitOfWork.Begin())
                {
                    serviceLink = this.GetByTinyUrl(tinyUrl);

                    this.UnitOfWork.Commit();
                }
                return serviceLink;
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
        List<ServiceLink> IServiceLinkService.GetAll()
        {
            try
            {
                List<ServiceLink> serviceLinks = new List<ServiceLink>();

                using (this.UnitOfWork.Begin())
                {
                    serviceLinks = this.GetAll();

                    this.UnitOfWork.Commit();
                }

                return serviceLinks;
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
        List<ServiceLink> IServiceLinkService.GetAll(
            Identity identity)
        {
            try
            {
                Validation.ValidateParameter<Identity>("identity", identity);

                List<ServiceLink> serviceLinks = new List<ServiceLink>();

                using (this.UnitOfWork.Begin())
                {
                    serviceLinks = this.GetAll(identity);

                    this.UnitOfWork.Commit();
                }

                return serviceLinks;
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
            ServiceLink serviceLink)
        {
            try
            {
                Validation.ValidateParameter<ServiceLink>("serviceLink", serviceLink);

                using (this.UnitOfWork.Begin())
                {
                    this.Remove(serviceLink);

                    this.UnitOfWork.Commit();
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
            ServiceLink serviceLink,
            String      detailTypeName,
            String      serializedDetail)
        {
            try
            {
                Validation.ValidateParameter<ServiceLink>("serviceLink"     , serviceLink     );
                Validation.ValidateParameter<String>     ("detailTypeName"  , detailTypeName  );
                Validation.ValidateParameter<String>     ("serializedDetail", serializedDetail);

                using (this.UnitOfWork.Begin())
                {
                    this.Update(serviceLink, detailTypeName, serializedDetail);

                    this.UnitOfWork.Commit();
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
