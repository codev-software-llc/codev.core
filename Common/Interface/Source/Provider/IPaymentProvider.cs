//-----------------------------------------------------------------------------
// <copyright file="IPaymentProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using System;
    using Codev.Core.Common.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the means to perform payments.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IPaymentProvider : IProvider
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Perform an authorization and capture at the same transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        Token Charge(
            Instant       instantNow,
            LocalDate     currentDate,
            Token         paymentMethodToken,
            PaymentAmount paymentAmount,
            String        orderNumber);

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Refund a "Captured" payment.
        /// </summary>
        ///--------------------------------------------------------------------
        Token Refund(
            Instant       instantNow,
            LocalDate     currentDate,
            Token         transactionToken,
            PaymentAmount paymentAmount);
        #endregion
    }
}
