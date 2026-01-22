//-----------------------------------------------------------------------------
// <copyright file="DeleteCustomerRequest.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to delete a customer.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DeleteCustomerRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the customer to remove.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }
        #endregion
    }
}