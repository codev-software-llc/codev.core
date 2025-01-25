//-----------------------------------------------------------------------------
// <copyright file="LicenseService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Licensing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    return this.Create(identityEntity, applicationName, name, cost, interval, intervalCount, trialDays, features);
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
        /// Delete the license.
        /// </summary>
        ///--------------------------------------------------------------------
        void ILicenseService.Delete(
            Reference<License> licenseReference)
        {
            Validation.ValidateParameter<Reference<License>>("licenseReference", licenseReference);

            try
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
                EntityCollection<LicenseEntity> entities = this.LicenseRepository.GetAllByApplication(applicationName);

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
        /// Locate a license for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        License ILicenseService.Get(
            Reference<License> licenseReference)
        {
            Validation.ValidateParameter<Reference<License>>("licenseReference", licenseReference);

            try
            {
                LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                if (licenseEntity != null)
                {
                    return this.Get(licenseEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
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
                LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                if (licenseEntity != null)
                {
                    this.Update(licenseEntity, name, features);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
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
                LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                if (licenseEntity != null)
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetByLicense(licenseEntity);

                    if (identityEntity != null)
                    {
                        return this.Subscribe(licenseEntity, identityEntity, dateExpiration);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                    }
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
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
        /// Unsubscribe from a license.
        /// </summary>
        ///--------------------------------------------------------------------
        void ILicenseService.Unsubscribe(
            Reference<Subscription> subscriptionReference)
        {
            Validation.ValidateParameter<Reference<Subscription>>("subscriptionReference", subscriptionReference);

            try
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
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);
                
                if (identityEntity != null)
                {
                    EntityCollection<SubscriptionEntity> entities = this.SubscriptionRepository.GetAllByIdentity(identityEntity);

                    return entities.Select(x => x.ToModel()).ToList();
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
                LicenseEntity licenseEntity = this.LicenseRepository.GetById(licenseReference.Id);

                if (licenseEntity != null)
                {
                    EntityCollection<SubscriptionEntity> entities = this.SubscriptionRepository.GetAllByLicense(licenseEntity);

                    return entities.Select(x => x.ToModel()).ToList();
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.LicenseDoesNotExistMessage);
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
        #endregion
    }
}
