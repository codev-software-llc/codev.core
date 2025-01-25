//-----------------------------------------------------------------------------
// <copyright file="CreateCustomerResponse.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response to the cstommer create method.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CreateCustomerResponse
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
        /// Get or set the default source (card).
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("default_source")]
        public String DefaultCard { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the sustomer.
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
        #endregion
    }
}