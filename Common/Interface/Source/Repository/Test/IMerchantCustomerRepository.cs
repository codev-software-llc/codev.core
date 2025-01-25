//-----------------------------------------------------------------------------
// <copyright file="IMerchantCustomerRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for storing payment tokens.  This is use
    /// primarily for testing purposes, but could be leveraged to support those
    /// providers who don't support tokenization.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IMerchantCustomerRepository : IRepository<MerchantCustomerEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Locate the card by the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        MerchantCustomerEntity GetByCard(
            MerchantCardEntity card);

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Locate the token information by its token value.
        /// </summary>
        ///--------------------------------------------------------------------
        MerchantCustomerEntity GetBySubscription(
            MerchantSubscriptionEntity subscription);
        #endregion
    }
}
