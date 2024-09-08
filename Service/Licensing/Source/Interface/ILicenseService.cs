//-----------------------------------------------------------------------------
// <copyright file="ILicenseService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Licensing
{
    using System;
    using System.Collections.Generic;
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
            Identity             identity,
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
            License license);

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
            Int32 id);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the license.
        /// </summary>
        ///--------------------------------------------------------------------
        void Update(
            License              license,
            String               name,
            List<LicenseFeature> features);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Subscribe an identity to a license.
        /// </summary>
        ///--------------------------------------------------------------------
        Subscription Subscribe(
            License   license,
            Identity  identity,
            LocalDate dateExpiration);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Unsubscribe from a license.
        /// </summary>
        ///--------------------------------------------------------------------
        void Unsubscribe(
            Subscription subscription);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the subscriptions assigned to the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Subscription> GetSubscriptions(
            Identity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the subscriptions to the license.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Subscription> GetSubscriptions(
            License license);
        #endregion
    }
}
