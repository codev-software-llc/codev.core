//-----------------------------------------------------------------------------
// <copyright file="ISubscriptionPlanProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using System;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines a subs subsription plan.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISubscriptionPlanProvider : IProvider
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Creeate a new subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        Token Create(
            String               source,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays);

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the subscription plan.  This will remove all subscriptions
        /// to the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        void Delete(
            Token plan);

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Update the subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        Token Update(
            Token  token,
            String name);

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Subscribe a customer and plan.
        /// </summary>
        ///--------------------------------------------------------------------
        Token Subscribe(
            Token planToken,
            Token customerToken);

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Unsubscribe a subscription plan.  The type of token should be
        /// a subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        void Unsubscribe(
            Token subscriptionToken);
        #endregion
    }
}
