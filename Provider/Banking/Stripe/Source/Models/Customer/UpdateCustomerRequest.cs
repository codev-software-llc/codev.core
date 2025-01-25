//-----------------------------------------------------------------------------
// <copyright file="UpdateCustomerRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to update the details of a customer.
    /// </summary>
    ///------------------------------------------------------------------------
    public class UpdateCustomerRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the customer
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email for the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Email { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the default card if provided.  If this is null or
        /// empty, then we won't change the default.
        /// </summary>
        ///--------------------------------------------------------------------
        public String DefaultCard { get; set; }
        #endregion
    }
}