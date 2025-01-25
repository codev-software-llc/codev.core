//-----------------------------------------------------------------------------
// <copyright file="GetPlanResponse.cs" company="Codev Software, LLC">
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
    public class GetPlanResponse
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
        /// Get or set name of the plan type.
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
        /// Get or set the name of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("name")]
        public String Name { get; set; }
        #endregion
    }
}