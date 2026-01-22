//-----------------------------------------------------------------------------
// <copyright file="IMerchantCardRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for storing payment tokens.  This is use
    /// primarily for testing purposes, but could be leveraged to support those
    /// providers who don't support tokenization.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IMerchantCardRepository : IRepository<MerchantCardEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Return all the cards for the transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        MerchantCardEntity GetByTransaction(
            MerchantTransactionEntity transaction);

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Return all the cards for the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<MerchantCardEntity> GetAllByCustomer(
            MerchantCustomerEntity customer);
        #endregion
    }
}
