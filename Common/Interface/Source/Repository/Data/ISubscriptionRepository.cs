//-----------------------------------------------------------------------------
// <copyright file="ISubscriptionRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the licensing subscription.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISubscriptionRepository : IRepository<SubscriptionEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all the subscriptions for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<SubscriptionEntity> GetAllByIdentity(
            IdentityEntity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all the subscriptions for the license.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<SubscriptionEntity> GetAllByLicense(
            LicenseEntity license);
        #endregion
    }
}
