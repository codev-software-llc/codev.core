//-----------------------------------------------------------------------------
// <copyright file="StubSubscriptionCustomerProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stub
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call for a connect.
    /// </summary>
    ///------------------------------------------------------------------------
    public class StubSubscriptionCustomerProvider : ISubscriptionCustomerProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the provider.
        /// </summary>
        ///--------------------------------------------------------------------
        public StubSubscriptionCustomerProvider()
        {
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Creeate a new customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Create(
            Instant instantNow,
            String  name,
            String  email)
        {
            return new Token()
                {
                    Id          = "1234567890ABCDEF",
                    Title       = "Stub Token",
                    Description = "Stub Token",
                    IsSuccess   = true,
                    Type        = "Customer Stub"
                };
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void  Delete(
            Token token)
        {
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Update the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            Token   token,
            Instant instantNow,
            String  name,
            String  email)
        {
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Add a card to the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token AddCard(
            Token             customerToken,
            PaymentCreditCard creditCard,
            Instant           instantNow)
        {
            return new Token()
                {
                    Id          = "1234567890ABCDEF",
                    Title       = "Stub Token",
                    Description = "Stub Token",
                    IsSuccess   = true,
                    Type        = "Card Stub"
                };
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Remove a card from the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void RemoveCard(
            Token cardToken)
        {
        }
        #endregion
    }
}