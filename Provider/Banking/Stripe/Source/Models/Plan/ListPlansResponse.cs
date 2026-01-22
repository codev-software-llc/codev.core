//-----------------------------------------------------------------------------
// <copyright file="ListPlansResponse.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response to retreieve all plans.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ListPlansResponse
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set name of the object (list).
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("object")]
        public String Object { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the data.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("data")]
        public List<GetPlanResponse> Data { get; set; }
        #endregion
    }
}