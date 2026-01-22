//-----------------------------------------------------------------------------
// <copyright file="DeletePlanResponse.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response to the plan deletion.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DeletePlanResponse
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
        /// Get or set whether it was deleted.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonPropertyName("deleted")]
        public Boolean IsDeleted { get; set; }
        #endregion
    }
}