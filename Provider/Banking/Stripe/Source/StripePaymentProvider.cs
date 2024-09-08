//-----------------------------------------------------------------------------
// <copyright file="StripePaymentProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the payment provider for direct charges and refunds.
    /// </summary>
    ///------------------------------------------------------------------------
    public class StripePaymentProvider : IPaymentProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public StripePaymentProvider()
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