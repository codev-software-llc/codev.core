//-----------------------------------------------------------------------------
// <copyright file="CancelSubscriptionResponse.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response to the subscription cancellation.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CancelSubscriptionResponse
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("id")]
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set status of subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("status")]
        public String Status { get; set; }
        #endregion
    }
}