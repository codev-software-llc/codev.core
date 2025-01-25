//-----------------------------------------------------------------------------
// <copyright file="CreateSubscriptionRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to create a new subscription.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CreateSubscriptionRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String CustomerId { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the plan identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String PlanId { get; set; }
        #endregion
    }
}