//-----------------------------------------------------------------------------
// <copyright file="PaymentCreditCard.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This represents a credit card payment type.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentCreditCard : BasePayment
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentCreditCard()
        {
            this.BillingAddress = new AddressInfo();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card type.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Type { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Number { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card CVC code.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Code { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card expiration date.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDate DateExpriation { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the billing address (optional).
        /// </summary>
        ///--------------------------------------------------------------------
        public AddressInfo BillingAddress { get; set; }
        #endregion
    }
}