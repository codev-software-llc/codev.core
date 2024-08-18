//-----------------------------------------------------------------------------
// <copyright file="CreateSubscriptionResponse.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response to the subscription creation.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CreateSubscriptionResponse
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set ths subscription identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("id")]
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the object (subscription).
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("object")]
        public String Object { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether we're in live mode.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("livemode")]
        public Boolean IsLiveMode { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("customer")]
        public String CustomerId { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the subscription status.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("status")]
        public String Status { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the start date (EPOCH).
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("start")]
        public Int32 Start { get; set; }
        #endregion
    }
}