//-----------------------------------------------------------------------------
// <copyright file="LicenseService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Licensing
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains the explicit interface hooks that we will use to perform
    /// call validation on the parameters.  Each of these methods will in-turn
    /// call the actual implementations.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class LicenseService : ILicenseService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new License.
        /// </summary>
        ///--------------------------------------------------------------------
        License ILicenseService.Create(
            Reference<Identity>  identityReference,
            String               applicationName,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays,
            List<LicenseFeature> features)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<String>             ("applicationName"  , applicationName  );
            Validation.ValidateParameter<String>             ("name"             , name             );
            Validation.ValidateParameter<PaymentAmount>      ("cost"             , cost             );

            try
            {
                features = Validation.ValidateDefault<List<LicenseFeature>>("features", features, new List<LicenseFeature>());

                License license = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        license = this.Create(identityEntity, applicationName, name, cost, interval, intervalCount, trialDays, features);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return license;
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
        /// Delete the license.
        /// </summary>
        ///--------------------------------------------------------------------
        void ILicenseService.Delete(
            Reference<License> licenseReference)
        {
            Validation.ValidateParameter<Reference<License>>("licenseReference", licenseReference);

            try
            {
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                    if (licenseEntity != null)
                    {
                        this.Delete(licenseEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
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
        /// Locate a license entry by the user key.
        /// </summary>
        ///--------------------------------------------------------------------
        List<License> ILicenseService.GetAll(
            String applicationName)
        {
            Validation.ValidateParameter<String>("applicationName", applicationName);

            try
            {
                List<License> licenses = new List<License>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    EntityCollection<LicenseEntity> entities = this.LicenseRepository.GetAllByApplication(applicationName);

                    foreach (LicenseEntity entity in entities)
                    {
                        licenses.Add(entity.ToModel());
                    }

                    work.Commit();
                }

                return licenses;
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
        /// Locate a license for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        License ILicenseService.Get(
            Reference<License> licenseReference)
        {
            Validation.ValidateParameter<Reference<License>>("licenseReference", licenseReference);

            try
            {
                License license = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                    if (licenseEntity != null)
                    {
                        license = this.Get(licenseEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return license;
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
        /// Update the license.
        /// </summary>
        ///--------------------------------------------------------------------
        void ILicenseService.Update(
            Reference<License>   licenseReference,
            String               name,
            List<LicenseFeature> features)
        {
            Validation.ValidateParameter<Reference<License>>("licenseReference", licenseReference);
            Validation.ValidateParameter<String>            ("name"            , name            );

            features = Validation.ValidateDefault<List<LicenseFeature>>("features", features, new List<LicenseFeature>());

            try
            {
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                    if (licenseEntity != null)
                    {
                        this.Update(licenseEntity, name, features);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
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
        /// Subscribe an identity to a license.
        /// </summary>
        ///--------------------------------------------------------------------
        Subscription ILicenseService.Subscribe(
            Reference<License>  licenseReference,
            Reference<Identity> identityReference,
            LocalDate           dateExpiration)
        {
            Validation.ValidateParameter<Reference<License>> ("licenseReference" , licenseReference);
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<LocalDate>          ("dateExpiration"   , dateExpiration  );

            try
            {
                Subscription subscription = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                    if (licenseEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByLicense(licenseEntity);

                        if (identityEntity != null)
                        {
                            subscription = this.Subscribe(licenseEntity, identityEntity, dateExpiration);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return subscription;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
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
        /// Unsubscribe from a license.
        /// </summary>
        ///--------------------------------------------------------------------
        void ILicenseService.Unsubscribe(
            Reference<Subscription> subscriptionReference)
        {
            Validation.ValidateParameter<Reference<Subscription>>("subscriptionReference", subscriptionReference);

            try
            {
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    SubscriptionEntity subscriptionEntity = this.SubscriptionRepository.GetById(subscriptionReference.Id);

                    if (subscriptionEntity != null)
                    {
                        this.Unsubscribe(subscriptionEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.SubscriptionDoesNotExist);
                    }

                    work.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
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
        /// Get the subscriptions to the license.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Subscription> ILicenseService.GetSubscriptions(
            Reference<Identity> identityReference)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            try
            {
                List<Subscription> subscriptions = new List<Subscription>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        EntityCollection<SubscriptionEntity> entities = this.SubscriptionRepository.GetAllByIdentity(identityEntity);

                        foreach (SubscriptionEntity entity in entities)
                        {
                            subscriptions.Add(entity.ToModel());
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return subscriptions;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
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
        /// Get the subscriptions to the license.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Subscription> ILicenseService.GetSubscriptions(
            Reference<License> licenseReference)
        {
            Validation.ValidateParameter<Reference<License>>("licenseReference", licenseReference);

            try
            {
                List<Subscription> subscriptions = new List<Subscription>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                    if (licenseEntity != null)
                    {
                        EntityCollection<SubscriptionEntity> entities = this.SubscriptionRepository.GetAllByLicense(licenseEntity);

                        foreach (SubscriptionEntity entity in entities)
                        {
                            subscriptions.Add(entity.ToModel());
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return subscriptions;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
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
