//-----------------------------------------------------------------------------
// <copyright file="LicenseService.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Licensing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Codev.Core.Service.Clock;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the service for managing licenses and subscription
    /// payments.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class LicenseService : BaseService, ILicenseService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public LicenseService(
            ICoreDataSource           dataSource,
            IClockService             clockService,
            IIdentityRepository       identityRepository,
            ILicenseRepository        licenseRepository,
            ISubscriptionRepository   subscriptionRepository,
            ISubscriptionPlanProvider subscriptionPlanProvider) : base(dataSource, clockService)
        {
            Validation.ValidateParameter<IIdentityRepository>      ("identityRepository"    , identityRepository      );
            Validation.ValidateParameter<ILicenseRepository>       ("licenseRepository"     , licenseRepository       );
            Validation.ValidateParameter<ISubscriptionRepository>  ("subscriptionRepository", subscriptionRepository  );
            Validation.ValidateParameter<ISubscriptionPlanProvider>("clockService"          , subscriptionPlanProvider);

            this.IdentityRepository       = identityRepository;
            this.LicenseRepository        = licenseRepository;
            this.SubscriptionRepository   = subscriptionRepository;
            this.SubscriptionPlanProvider = subscriptionPlanProvider;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the store.
        /// </summary>
        ///---------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the store.
        /// </summary>
        ///---------------------------------------------------------------
        private ILicenseRepository LicenseRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for subscriptions.
        /// </summary>
        ///---------------------------------------------------------------
        private ISubscriptionRepository SubscriptionRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the provider that will handling the subscription
        /// management.
        /// </summary>
        ///---------------------------------------------------------------
        private ISubscriptionPlanProvider SubscriptionPlanProvider { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new license (plan).
        /// </summary>
        ///--------------------------------------------------------------------
        public License Create(
            IdentityEntity       identityEntity,
            String               applicationName,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays,
            List<LicenseFeature> features)
        {
            // Invoke the provider to establish a plan.
            //
            Token token = this.SubscriptionPlanProvider.Create(applicationName, name, cost, interval, intervalCount, trialDays);

            // Persist our own information about the license.
            //
            Instant instantNow = this.ClockService.GetCurrentInstant();

            LicenseEntity entity = new LicenseEntity(instantNow)
                {
                    Identity    = identityEntity,
                    Flags       = LicenseFlags.None,
                    Application = applicationName,
                    Name        = name,
                    Cost        = cost,
                    Features    = features,
                    Token       = token
                };

            this.LicenseRepository.Add(entity);

            return entity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the license.  We will only allow the license to be deleted
        /// if there are no subscriptions.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Delete(
            LicenseEntity licenseEntity)
        {
            EntityCollection<SubscriptionEntity> subscriptions = this.SubscriptionRepository.GetAllByLicense(licenseEntity);

            if (subscriptions.Count == 0)
            {
                // Purge the license.
                //
                this.LicenseRepository.Purge(licenseEntity);

                // Remove the plan from our provider.
                //
                this.SubscriptionPlanProvider.Delete(licenseEntity.Token);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.InvalidOperation, ExceptionMessage.LicenseHasSubscriptionsMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate license by the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public License Get(
            LicenseEntity licenseEntity)
        {
            return licenseEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the license.  We only allow an update on the name, features
        /// and expiration.  Cost and interval are not changed.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            LicenseEntity        licenseEntity,
            String               name,
            List<LicenseFeature> features)
        {
            // Update the information to the provider.
            //
            Token token = this.SubscriptionPlanProvider.Update(licenseEntity.Token, name);

            // Persist the licence data.
            //
            licenseEntity.DateModified = this.ClockService.GetCurrentInstant();
            licenseEntity.Name         = name;
            licenseEntity.Features     = features;
            licenseEntity.Token        = token;

            // Call the provider to update the details for payment
            // processing.
            //
            this.LicenseRepository.Update(licenseEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Subscribe the entity to the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public Subscription Subscribe(
            LicenseEntity   licenseEntity,
            IdentityEntity  identityEntity,
            LocalDate       dateExpiration)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            // Subscribe to the license through the provider.
            //
            Token identityToken = new Token()
                {
                    Id          = identityEntity.Id.ToString(),
                    Description = "Identity Customer Token",
                    Title       = "Token",
                    Type        = "Customer",
                    IsSuccess   = true
                };

            Token token = this.SubscriptionPlanProvider.Subscribe(licenseEntity.Token, identityToken);

            // Persist the subscription against the identity.
            //
            SubscriptionEntity subscriptionEntity = new SubscriptionEntity(instantNow)
                {
                    Flags          = SubscriptionFlags.None,
                    DateExpiration = this.GetExpirationInstant(identityEntity, dateExpiration),
                    Identity       = identityEntity,
                    License        = licenseEntity,
                    Token          = token
                };

            this.SubscriptionRepository.Add(subscriptionEntity);

            return subscriptionEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Unsubscribe an identity from a license.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Unsubscribe(
            SubscriptionEntity subscriptionEntity)
        {
            // First we invoke the 3rd party provider to remove the
            // subscription.
            //
            this.SubscriptionPlanProvider.Unsubscribe(subscriptionEntity.Token);

            // Now remove from our repository tracking permanently.
            //
            this.SubscriptionRepository.Purge(subscriptionEntity);
        }
        #endregion

        #region Methods (Private)       
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the expiration instant.
        /// </summary>
        ///--------------------------------------------------------------------
        private Instant GetExpirationInstant(
            IdentityEntity identityEntity,
            LocalDate      dateExpiration)
        {
            Instant instantExpiration;

            if (dateExpiration.CompareTo(NodaExtensions.LocalDateMaxValue) == 0)
            {
                instantExpiration = NodaExtensions.InstantMaxValue;
            }
            else
            {
                instantExpiration = dateExpiration.ToEndOfDay().ToInstant(identityEntity.TimeZone);
            }

            return instantExpiration;
        }
        #endregion
    }
}
