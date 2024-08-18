//-----------------------------------------------------------------------------
// <copyright file="IMerchantPlanRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for storing payment tokens.  This is use
    /// primarily for testing purposes, but could be leveraged to support those
    /// providers who don't support tokenization.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IMerchantPlanRepository : IRepository<MerchantPlanEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Locate the token information by its token value.
        /// </summary>
        ///--------------------------------------------------------------------
        MerchantPlanEntity GetBySubscription(
            MerchantSubscriptionEntity subscription);
        #endregion
    }
}
