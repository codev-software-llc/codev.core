//-----------------------------------------------------------------------------
// <copyright file="StubPaymentProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stub
{
    using System;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the payment provider for stub payments.
    /// </summary>
    ///------------------------------------------------------------------------
    public class StubPaymentProvider : IPaymentProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public StubPaymentProvider()
        {
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Perform a full charge on the card.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Charge(
            Instant       instantNow,
            LocalDate     currentDate,
            Token         cardToken,
            PaymentAmount paymentAmount,
            String        orderNumber)
        {
            return new Token()
                {
                    Id          = "1234567890ABCDEF",
                    Title       = "Stub Token",
                    Description = "Stub Token",
                    IsSuccess   = true,
                    Type        = "Payment Charge Stub"
                };
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Refund a "Captured" payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Refund(
            Instant        instantNow,
            LocalDate      currentDate,
            Token          transactionToken,
            PaymentAmount  paymentAmount)
        {
            return new Token()
                {
                    Id          = "1234567890ABCDEF",
                    Title       = "Stub Token",
                    Description = "Stub Token",
                    IsSuccess   = true,
                    Type        = "Payment Refund Stub"
                };
        }
        #endregion
    }
}