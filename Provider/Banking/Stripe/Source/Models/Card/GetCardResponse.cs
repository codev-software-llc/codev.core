//-----------------------------------------------------------------------------
// <copyright file="GetCardResponse.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response to the Get request.
    /// </summary>
    ///------------------------------------------------------------------------
    public class GetCardResponse
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the card.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("id")]
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("customer")]
        public String CustomerId { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the type of object (Card).
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
        /// Get or set the type of card (visa, mastercard, ...)
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("brand")]
        public String Brand { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the country.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("country")]
        public String Country { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the last four digits of the card.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("last4")]
        public String Last4Digits { get; set; }
        #endregion
    }
}