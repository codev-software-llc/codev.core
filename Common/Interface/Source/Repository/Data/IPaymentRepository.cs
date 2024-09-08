//-----------------------------------------------------------------------------
// <copyright file="IPaymentRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the payment repository access.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IPaymentRepository : IRepository<PaymentEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return payment method for the payment transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        PaymentEntity GetByPaymentTransaction(
            PaymentTransactionEntity paymentTransaction);
        #endregion
    }
}
