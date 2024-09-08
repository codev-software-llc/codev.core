//-----------------------------------------------------------------------------
// <copyright file="StripeSubscriptionCustomerProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
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
    public class StripeSubscriptionCustomerProvider : ISubscriptionCustomerProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the provider.
        /// </summary>
        ///--------------------------------------------------------------------
        public StripeSubscriptionCustomerProvider(
            IMerchantCardRepository   cardRepository,
            IMerchantCustomerRepository customerRepository)
        {
            this.CustomerRepository = customerRepository;
            this.CardRepository     = cardRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing customers.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCustomerRepository CustomerRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing cards.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCardRepository CardRepository { get; set; }
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
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void  Delete(
            Token token)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Remove a card from the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void RemoveCard(
            Token cardToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}