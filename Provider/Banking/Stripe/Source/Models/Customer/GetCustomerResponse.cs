//-----------------------------------------------------------------------------
// <copyright file="GetCustomerResponse.cs" company="Codev Software, LLC">
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
    public class GetCustomerResponse
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("id")]
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set name of the object (Customer).
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
        /// Get or set whether the customer is delinquent.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("delinquent")]
        public Boolean IsDelinquent { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("description")]
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("email")]
        public String Email { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the account balance.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("account_balance")]
        public Int32 AccountBalenaceInCents { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the currency (usd, cad, ...).
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("currency")]
        public String CurrencyCode { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the default source (card).
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("default_source")]
        public String DefaultCard { get; set; }
        #endregion
    }
}