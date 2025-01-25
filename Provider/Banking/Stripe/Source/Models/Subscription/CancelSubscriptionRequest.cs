//-----------------------------------------------------------------------------
// <copyright file="CancelSubscriptionRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to cancel a subscription.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CancelSubscriptionRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the subscription identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }
        #endregion
    }
}