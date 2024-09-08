//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using Codev.Core.Service.Common;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IServiceLinkService interface.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ServiceLinkService : BaseService, IServiceLinkService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public ServiceLinkService(
            ICoreUnitOfWork        unitOfWork,
            IIdentityRepository    identityRepository,
            IServiceLinkRepository serviceLinkRepository,
            IClockService          clockService) : base(unitOfWork)
        {
            Validation.ValidateParameter<IIdentityRepository>   ("identityRepository"   , identityRepository   );
            Validation.ValidateParameter<IServiceLinkRepository>("serviceLinkRepository", serviceLinkRepository);
            Validation.ValidateParameter<IClockService>         ("clockService"         , clockService         );

            this.IdentityRepository    = identityRepository;
            this.ServiceLinkRepository = serviceLinkRepository;
            this.ClockService          = clockService;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the repository for identities.
        /// </summary>
        ///--------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the repository for service links.
        /// </summary>
        ///--------------------------------------------------------------------
        private IServiceLinkRepository ServiceLinkRepository { get; set; }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the service for a clock.
        /// </summary>
        ///--------------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a service link.  The key can be used to group or provide a
        /// unique reference that can allow other consumers to access.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceLink Add(
            Identity identity,
            String   detailTypeName,
            String   serializedDetail)
        {
            IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

            if (identityEntity != null)
            {
                Instant instantNow = this.ClockService.GetCurrentInstant();

                ServiceLinkEntity entity = new ServiceLinkEntity(instantNow)
                    {
                        Identity   = identityEntity,
                        Flags      = ServiceLinkFlags.None,
                        TinyUrl    = this.GenerateServiceLinkTinyURL(),
                        DetailType = detailTypeName,
                        Detail     = serializedDetail
                    };

                this.ServiceLinkRepository.Add(entity);

                return entity.ToModel();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete all service links that share the same key.
        /// </summary>
        ///--------------------------------------------------------------------
        public void DeleteAll(
            Identity identity)
        {
            IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

            if (identityEntity != null)
            {
                this.ServiceLinkRepository.PurgeAllByIdentity(identityEntity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the service link using the tiny url.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceLink GetByTinyUrl(
            String tinyUrl)
        {
            ServiceLinkEntity entity = this.ServiceLinkRepository.GetByTinyUrl(tinyUrl);

            if (entity != null)
            {
                return entity.ToModel();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ServiceLinkDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get all service links.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ServiceLink> GetAll()
        {
            EntityCollection<ServiceLinkEntity> entities = this.ServiceLinkRepository.GetAll();

            return entities.Select(x => x.ToModel()).ToList();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the service links that match the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ServiceLink> GetAll(
            Identity identity)
        {
            IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

            if (identityEntity != null)
            {
                EntityCollection<ServiceLinkEntity> entities = this.ServiceLinkRepository.GetAllByIdentity(identityEntity);

                return entities.Select(x => x.ToModel()).ToList();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Remove(
            ServiceLink serviceLink)
        {
            ServiceLinkEntity entity = this.ServiceLinkRepository.GetById(serviceLink.Id);

            if (entity != null)
            {
                this.ServiceLinkRepository.Purge(entity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ServiceLinkDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            ServiceLink serviceLink,
            String      detailTypeName,
            String      serializedDetail)
        {
            ServiceLinkEntity entity = this.ServiceLinkRepository.GetById(serviceLink.Id);
            
            if (entity != null)
            {
                entity.DetailType   = detailTypeName;
                entity.Detail       = serializedDetail;
                entity.DateModified = this.ClockService.GetCurrentInstant();
            
                this.ServiceLinkRepository.Update(entity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ServiceLinkDoesNotExistMessage);
            }
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a unique URL for representing outside the domain.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GenerateServiceLinkTinyURL()
        {
            return TinyUrlHelper.GenerateUnique(6, (url => this.ServiceLinkRepository.GetByTinyUrl(url) != null));
        }
        #endregion
    }
}