//-----------------------------------------------------------------------------
// <copyright file="LicenseService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Licensing
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
            ICoreUnitOfWork           unitOfWork,
            IIdentityRepository       identityRepository,
            ILicenseRepository        licenseRepository,
            ISubscriptionRepository   subscriptionRepository,
            ISubscriptionPlanProvider subscriptionPlanProvider,
            IClockService             clockService) : base(unitOfWork)
        {
            Validation.ValidateParameter<IIdentityRepository>      ("identityRepository"    , identityRepository      );
            Validation.ValidateParameter<ILicenseRepository>       ("licenseRepository"     , licenseRepository       );
            Validation.ValidateParameter<ISubscriptionRepository>  ("subscriptionRepository", subscriptionRepository  );
            Validation.ValidateParameter<ISubscriptionPlanProvider>("clockService"          , subscriptionPlanProvider);
            Validation.ValidateParameter<IClockService>            ("clockService"          , clockService            );

            this.IdentityRepository       = identityRepository;
            this.LicenseRepository        = licenseRepository;
            this.SubscriptionRepository   = subscriptionRepository;
            this.SubscriptionPlanProvider = subscriptionPlanProvider;
            this.ClockService             = clockService;
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
        /// Create a new license (plan).
        /// </summary>
        ///--------------------------------------------------------------------
        public License Create(
            Identity             identity,
            String               applicationName,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays,
            List<LicenseFeature> features)
        {
            IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

            if (identityEntity != null)
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
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the license.  We will only allow the license to be deleted
        /// if there are no subscriptions.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Delete(
            License license)
        {
            LicenseEntity licenseEntity = this.LicenseRepository.GetById(license.Id);

            if (licenseEntity != null)
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
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate license by the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public License Get(
            Int32 id)
        {
            LicenseEntity entity = this.LicenseRepository.GetById(id);

            if (entity != null)
            {
                return entity.ToModel();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all licenses for the application.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<License> GetAll(
            String applicationName)
        {
            EntityCollection<LicenseEntity> entities =  this.LicenseRepository.GetAllByApplication(applicationName);

            return entities.Select(x => x.ToModel()).ToList();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the license.  We only allow an update on the name, features
        /// and expiration.  Cost and interval are not changed.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            License              license,
            String               name,
            List<LicenseFeature> features)
        {
            LicenseEntity entity = this.LicenseRepository.GetById(license.Id);

            if (entity != null)
            {
                // Update the information to the provider.
                //
                Token token = this.SubscriptionPlanProvider.Update(entity.Token, name);

                // Persist the licence data.
                //
                entity.DateModified = this.ClockService.GetCurrentInstant();
                entity.Name         = name;
                entity.Features     = features;
                entity.Token        = token;

                // Call the provider to update the details for payment
                // processing.
                //
                this.LicenseRepository.Update(entity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Subscribe the entity to the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public Subscription Subscribe(
            License   license,
            Identity  identity,
            LocalDate dateExpiration)
        {
            LicenseEntity licenseEntity = this.LicenseRepository.GetById(license.Id);

            if (licenseEntity != null)
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

                if (identityEntity != null)
                {
                    Instant instantNow = this.ClockService.GetCurrentInstant();

                    // Subscribe to the license through the provider.
                    //
                    Token identityToken = new Token()
                        {
                            Id          = identity.Id.ToString(),
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
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
                }
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Unsubscribe an identity from a license.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Unsubscribe(
            Subscription subscription)
        {
            SubscriptionEntity subscriptionEntity = this.SubscriptionRepository.GetById(subscription.Id);

            if (subscriptionEntity != null)
            {
                // First we invoke the 3rd party provider to remove the
                // subscription.
                //
                this.SubscriptionPlanProvider.Unsubscribe(subscriptionEntity.Token);

                // Now remove from our repository tracking permanently.
                //
                this.SubscriptionRepository.Purge(subscriptionEntity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.SubscriptionDoesNotExist);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the subscriptions to the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<Subscription> GetSubscriptions(
            Identity identity)
        {
            IdentityEntity identityEntity = this.IdentityRepository.GetById(identity.Id);

            if (identityEntity != null)
            {
                EntityCollection<SubscriptionEntity> subscriptions = this.SubscriptionRepository.GetAllByIdentity(identityEntity);

                return subscriptions.Select(x => x.ToModel()).ToList();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the subscriptions to the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<Subscription> GetSubscriptions(
            License license)
        {
            LicenseEntity licenseEntity = this.LicenseRepository.GetById(license.Id);

            if (licenseEntity != null)
            {
                EntityCollection<SubscriptionEntity> subscriptions = this.SubscriptionRepository.GetAllByLicense(licenseEntity);

                return subscriptions.Select(x => x.ToModel()).ToList();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
            }
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
