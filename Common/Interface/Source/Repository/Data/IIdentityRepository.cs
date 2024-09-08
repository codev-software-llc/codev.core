//-----------------------------------------------------------------------------
// <copyright file="IIdentityRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the repository for managing identities.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IIdentityRepository : IRepository<IdentityEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetByDestination(
            DestinationEntity destination);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the error log.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetByErrorLog(
            ErrorLogEntity errorLog);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetByBlob(
            BlobEntity errorLog);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the license.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetByLicense(
            LicenseEntity errorLog);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity with the payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetByPaymentMethod(
            PaymentMethodEntity paymentMethod);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetByServiceLink(
            ServiceLinkEntity serviceLink);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the session.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetBySession(
            SessionEntity session);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the scheduled task.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetByScheduledTask(
            ScheduledTaskEntity scheduledTask);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the user by the license subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        IdentityEntity GetBySubscription(
            SubscriptionEntity subscription);
        #endregion
    }
}
