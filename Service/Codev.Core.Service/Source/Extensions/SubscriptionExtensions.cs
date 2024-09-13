//-----------------------------------------------------------------------------
// <copyright file="SubscriptionExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the subscriptions.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class SubscriptionExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Subscription ToModel(
            this SubscriptionEntity entity)
        {
            return new Subscription()
                {
                    Id       = entity.Id,
                    Identity = entity.Identity.ToModel(),
                    License  = entity.License.ToModel()
                };
        }
        #endregion
    }
}
