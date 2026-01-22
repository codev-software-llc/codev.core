//-----------------------------------------------------------------------------
// <copyright file="ISubscriptionCustomerProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines a subscription customer.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISubscriptionCustomerProvider : IProvider
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Creeate a new customer.
        /// </summary>
        ///--------------------------------------------------------------------
        Token Create(
            Instant instantNow,
            String  name,
            String  email);

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the customer.  This will invariantly delete all the 
        /// subscriptions the customer has.
        /// </summary>
        ///--------------------------------------------------------------------
        void Delete(
            Token token);

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Update the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        void Update(
            Token   token,
            Instant instantNow,
            String  name,
            String  email);

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Add a card to the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        Token AddCard(
            Token             customerToken,
            PaymentCreditCard creditCard,
            Instant           instantNow);

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Remove a card from the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        void RemoveCard(
            Token cardToken);
        #endregion
    }
}
