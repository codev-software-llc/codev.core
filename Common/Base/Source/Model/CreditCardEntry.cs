//-----------------------------------------------------------------------------
// <copyright file="CreditCardEntry.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides the information to capture for a lookup entry when
    /// validating cards.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CreditCardEntry
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the entry object for credit card information.
        /// </summary>
        ///--------------------------------------------------------------------
        public CreditCardEntry(
            String              prefixNumber,
            Int32               cardLength,
            CreditCardCheckType algorithmCheck,
            CreditCardType      cardType)
        {
            this.PrefixNumber = prefixNumber;
            this.CardLength   = cardLength;
            this.Algorthm     = algorithmCheck;
            this.CardType     = cardType;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Gets the prefix number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String PrefixNumber { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Gets the length of the card numbers.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 CardLength { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Gets the card algorithm.
        /// </summary>
        ///--------------------------------------------------------------------
        public CreditCardCheckType Algorthm { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Gets the card type.
        /// </summary>
        ///--------------------------------------------------------------------
        public CreditCardType CardType { get; private set; }
        #endregion
    }
}
