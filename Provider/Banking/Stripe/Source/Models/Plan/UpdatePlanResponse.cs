//-----------------------------------------------------------------------------
// <copyright file="UpdatePlanResponse.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response to the plan create method.
    /// </summary>
    ///------------------------------------------------------------------------
    public class UpdatePlanResponse
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("id")]
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("object")]
        public String Object { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("name")]
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether we're in live mode.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("livemode")]
        public Boolean IsLiveMode { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the currency code of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("currency")]
        public String CurrencyCode { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the amount of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("amount")]
        public String Amount { get; set; }
        #endregion
    }
}