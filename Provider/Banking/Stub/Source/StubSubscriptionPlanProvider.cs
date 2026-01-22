//-----------------------------------------------------------------------------
// <copyright file="StubSubscriptionPlanProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stub
{
    using System;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the provider for subscriptions.
    /// </summary>
    ///------------------------------------------------------------------------
    public class StubSubscriptionPlanProvider : ISubscriptionPlanProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public StubSubscriptionPlanProvider()
        {
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Creeate a new plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Create(
            String               source,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays)
        {
            return new Token()
                {
                    Id          = "1234567890ABCDEF",
                    Title       = "Stub Token",
                    Description = "Stub Token",
                    IsSuccess   = true,
                    Type        = "Subscription Plan Stub"
                };
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the subscription plan.  This inherently will remove all
        /// subscriptions that are associate to the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public void  Delete(
            Token token)
        {
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Update the subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Update(
            Token  token,
            String name)
        {
            return new Token()
                {
                    Id          = "1234567890ABCDEF",
                    Title       = "Stub Token",
                    Description = "Stub Token",
                    IsSuccess   = true,
                    Type        =  "Subscription Plan Stub"
                };
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Subscribe a customer to a subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Subscribe(
            Token planToken,
            Token customerToken)
        {
            return new Token()
                {
                    Id          = "1234567890ABCDEF",
                    Title       = "Stub Token",
                    Description = "Stub Token",
                    IsSuccess   = true,
                    Type        =  "Customer Subscription Stub"
                };
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Remove a customer from the subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Unsubscribe(
            Token token)
        {
        }
        #endregion
    }
}