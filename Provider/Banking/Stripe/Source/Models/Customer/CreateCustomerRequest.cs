//-----------------------------------------------------------------------------
// <copyright file="CreateCustomerRequest.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to create a new customer.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CreateCustomerRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Email { get; set; }
        #endregion
    }
}