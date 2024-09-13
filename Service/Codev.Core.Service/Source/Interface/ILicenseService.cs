//-----------------------------------------------------------------------------
// <copyright file="ILicenseService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Licensing
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the Licensing service interaction.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ILicenseService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new License.
        /// </summary>
        ///--------------------------------------------------------------------
        License Create(
            Reference<Identity>  identityReference,
            String               applicationName,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays,
            List<LicenseFeature> features);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the license.
        /// </summary>
        ///--------------------------------------------------------------------
        void Delete(
            Reference<License> licenseReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all licenses for the application.
        /// </summary>
        ///--------------------------------------------------------------------
        List<License> GetAll(
            String applicationName);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate a license for the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        License Get(
            Reference<License> licenseReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the license.
        /// </summary>
        ///--------------------------------------------------------------------
        void Update(
            Reference<License>   licenseReference,
            String               name,
            List<LicenseFeature> features);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Subscribe an identity to a license.
        /// </summary>
        ///--------------------------------------------------------------------
        Subscription Subscribe(
            Reference<License>  licenseReference,
            Reference<Identity> identityReference,
            LocalDate           dateExpiration);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Unsubscribe from a license.
        /// </summary>
        ///--------------------------------------------------------------------
        void Unsubscribe(
            Reference<Subscription> subscriptionReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the subscriptions assigned to the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Subscription> GetSubscriptions(
            Reference<Identity> identityReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the subscriptions to the license.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Subscription> GetSubscriptions(
            Reference<License> licenseReference);
        #endregion
    }
}
