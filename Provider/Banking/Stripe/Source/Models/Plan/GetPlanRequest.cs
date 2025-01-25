//-----------------------------------------------------------------------------
// <copyright file="GetPlanRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to retrieve the plan information.
    /// </summary>
    ///------------------------------------------------------------------------
    public class GetPlanRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }
        #endregion
    }
}