//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.ServiceLink
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Codev.Core.Service.Clock;
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
            ICoreDataSource        dataSource,
            IClockService          clockService,
            IIdentityRepository    identityRepository,
            IServiceLinkRepository serviceLinkRepository) : base(dataSource, clockService)
        {
            Validation.ValidateParameter<IIdentityRepository>   ("identityRepository"   , identityRepository   );
            Validation.ValidateParameter<IServiceLinkRepository>("serviceLinkRepository", serviceLinkRepository);

            this.IdentityRepository    = identityRepository;
            this.ServiceLinkRepository = serviceLinkRepository;
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
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a service link.  The key can be used to group or provide a
        /// unique reference that can allow other consumers to access.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceJob Add(
            IdentityEntity identityEntity,
            String         detailTypeName,
            String         serializedDetail)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            ServiceLinkEntity entity = new ServiceLinkEntity(instantNow)
                {
                    DateModified = instantNow,
                    Identity     = identityEntity,
                    Flags        = ServiceLinkFlags.None,
                    TinyUrl      = this.GenerateServiceLinkTinyUrl(),
                    DetailType   = detailTypeName,
                    Detail       = serializedDetail
                };

            this.ServiceLinkRepository.Add(entity);

            return entity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete all service links that share the same key.
        /// </summary>
        ///--------------------------------------------------------------------
        public void DeleteAll(
            IdentityEntity identityEntity)
        {
            this.ServiceLinkRepository.PurgeAllByIdentity(identityEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Remove(
            ServiceLinkEntity serviceLinkEntity)
        {
            this.ServiceLinkRepository.Purge(serviceLinkEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            ServiceLinkEntity serviceLinkEntity,
            String            detailTypeName,
            String            serializedDetail)
        {
            serviceLinkEntity.DetailType   = detailTypeName;
            serviceLinkEntity.Detail       = serializedDetail;
            serviceLinkEntity.DateModified = this.ClockService.GetCurrentInstant();
            
            this.ServiceLinkRepository.Update(serviceLinkEntity);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a unique URL for representing outside the domain.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GenerateServiceLinkTinyUrl()
        {
            return TinyUrlHelper.GenerateUnique(6, (url => this.ServiceLinkRepository.GetByTinyUrl(url) != null));
        }
        #endregion
    }
}