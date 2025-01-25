//-----------------------------------------------------------------------------
// <copyright file="GetCustomerRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to retrieve the customer information.
    /// </summary>
    ///------------------------------------------------------------------------
    public class GetCustomerRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }
        #endregion
    }
}