//-----------------------------------------------------------------------------
// <copyright file="CreateCardRequest.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to create a new card.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CreateCardRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String CustomerId { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Number { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card CVV code.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Code { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card postal code.
        /// </summary>
        ///--------------------------------------------------------------------
        public String PostalCode { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the expiration date.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDate DateExpiration { get; set; }
        #endregion
    }
}