//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
    using Codev.Core.Repository.Ado;

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

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    serviceLink = this.Add(identity, detailTypeName, serializedDetail);

                    work.Commit();
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

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.DeleteAll(identity);

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

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    serviceLink = this.GetByTinyUrl(tinyUrl);

                    work.Commit();
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

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    serviceLinks = this.GetAll();

                    work.Commit();
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

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    serviceLinks = this.GetAll(identity);

                    work.Commit();
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

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Remove(serviceLink);

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

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Update(serviceLink, detailTypeName, serializedDetail);

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
