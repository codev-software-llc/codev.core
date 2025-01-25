//-----------------------------------------------------------------------------
// <copyright file="UpdateCardRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to update the details of a card.
    /// </summary>
    ///------------------------------------------------------------------------
    public class UpdateCardRequest
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
        /// Get or set the card number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

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