//-----------------------------------------------------------------------------
// <copyright file="Customer.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the local object for a customer.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class Customer
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Customer()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier for the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer email address.
        /// </summary>
        ///--------------------------------------------------------------------
        public String EmailAddress { get; set; }
        #endregion
    }
}