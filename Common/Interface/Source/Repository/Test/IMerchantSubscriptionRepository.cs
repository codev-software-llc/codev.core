//-----------------------------------------------------------------------------
// <copyright file="IMerchantSubscriptionRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for storing payment tokens.  This is use
    /// primarily for testing purposes, but could be leveraged to support those
    /// providers who don't support tokenization.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IMerchantSubscriptionRepository : IRepository<MerchantSubscriptionEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Return all subscriptions for the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<MerchantSubscriptionEntity> GetAllByPlan(
            MerchantPlanEntity plan);

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Return all subscriptions for the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<MerchantSubscriptionEntity> GetAllByCustomer(
            MerchantCustomerEntity customer);
        #endregion
    }
}
