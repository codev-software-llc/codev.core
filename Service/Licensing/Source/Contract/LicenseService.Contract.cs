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
            Identity             identity,
            String               applicationName,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays,
            List<LicenseFeature> features)
        {
            try
            {
                Validation.ValidateParameter<Identity>     ("identity"       , identity       );
                Validation.ValidateParameter<String>       ("applicationName", applicationName);
                Validation.ValidateParameter<String>       ("name"           , name           );
                Validation.ValidateParameter<PaymentAmount>("cost"           , cost           );

                features = Validation.ValidateDefault<List<LicenseFeature>>("features", features, new List<LicenseFeature>());

                License license = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    license = this.Create(identity, applicationName, name, cost, interval, intervalCount, trialDays, features);

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
            License license)
        {
            try
            {
                Validation.ValidateParameter<License>("license", license);

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    this.Delete(license);

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
            try
            {
                Validation.ValidateParameter<String>("applicationName", applicationName);

                List<License> licenses = new List<License>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    licenses = this.GetAll(applicationName);

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
            Int32 id)
        {
            try
            {
                Validation.ValidateParameter<Int32>("id", id);

                License license = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    license = this.Get(id);

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
            License              license,
            String               name,
            List<LicenseFeature> features)
        {
            try
            {
                Validation.ValidateParameter<License>("license", license);
                Validation.ValidateParameter<String> ("name"   , name   );

                features = Validation.ValidateDefault<List<LicenseFeature>>("features", features, new List<LicenseFeature>());

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    this.Update(license, name, features);

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
            License   license,
            Identity  identity,
            LocalDate dateExpiration)
        {
            try
            {
                Validation.ValidateParameter<License>  ("license"       , license       );
                Validation.ValidateParameter<Identity> ("identity"      , identity      );
                Validation.ValidateParameter<LocalDate>("dateExpiration", dateExpiration);

                Subscription subscription = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    subscription = this.Subscribe(license, identity, dateExpiration);

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
            Subscription subscription)
        {
            try
            {
                Validation.ValidateParameter<Subscription> ("subscription" , subscription);

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    this.Unsubscribe(subscription);

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
            Identity identity)
        {
            try
            {
                Validation.ValidateParameter<Identity>("identity", identity);

                List<Subscription> subscriptions = new List<Subscription>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    subscriptions = this.GetSubscriptions(identity);

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
            License license)
        {
            try
            {
                Validation.ValidateParameter<License>("license", license);

                List<Subscription> subscriptions = new List<Subscription>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    subscriptions = this.GetSubscriptions(license);

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
