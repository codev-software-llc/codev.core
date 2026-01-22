//-----------------------------------------------------------------------------
// <copyright file="MemoryPaymentProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    using System;
    using Codev.Core.Model;
    using Codev.Core.Interface;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the payment provider for direct charges and refunds.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MemoryPaymentProvider : IPaymentProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public MemoryPaymentProvider()
        {
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Perform an authorization and capture at the same transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Charge(
            Instant       instantNow,
            LocalDate     currentDate,
            Token         paymentMethodToken,
            PaymentAmount paymentAmount,
            String        orderNumber)
        {
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Refund a "Captured" payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Refund(
            Instant        instantNow,
            LocalDate      currentDate,
            Token          paymentMethodToken,
            PaymentAmount  paymentAmount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}