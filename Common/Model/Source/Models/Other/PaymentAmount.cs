//-----------------------------------------------------------------------------
// <copyright file="PaymentAmount.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines a payment amount using a currency.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentAmount
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount()
        {
            this.IsoCode   = CurrencyCode.Usd;
            this.IsoSymbol = CurrencySymbol.Usd;
            this.Amount    = 0.00m;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount(
            String  currencyCode,
            Decimal amount)
        {
            this.IsoCode = currencyCode;
            this.Amount  = amount;

            this.SetSymbol();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount(
            String  currencyCode,
            String  currencySymbol,
            Decimal amount)
        {
            this.IsoCode   = currencyCode;
            this.IsoSymbol = currencySymbol;
            this.Amount    = amount;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the currency symbol.
        /// </summary>
        ///--------------------------------------------------------------------
        public String IsoSymbol { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the ISO 3 character code for the payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public String IsoCode { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the amount of the payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public Decimal Amount { get; set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the symbol based on the IsoCode.
        /// </summary>
        ///--------------------------------------------------------------------
        private void SetSymbol()
        {
            switch (this.IsoCode)
            {
                case CurrencyCode.Eur:
                    this.IsoSymbol = CurrencySymbol.Eur;
                    break;

                case CurrencyCode.Cad:
                    this.IsoSymbol = CurrencySymbol.Cad;
                    break;

                default:
                    this.IsoSymbol = CurrencySymbol.Usd;
                    break;
            }
        }
        #endregion
    }
}