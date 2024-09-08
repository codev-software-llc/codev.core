//-----------------------------------------------------------------------------
// <copyright file="ValidateCreditCard.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a means to lookup a card type based on the card number.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ValidateCreditCard
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the lookup object.
        /// </summary>
        ///--------------------------------------------------------------------
        static ValidateCreditCard()
        {
            ValidateCreditCard.CardEntries = new List<CreditCardEntry>();

            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("6011", 16, CreditCardCheckType.Mod10, CreditCardType.Discover));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("2014", 15, CreditCardCheckType.Any  , CreditCardType.Enroute));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("2149", 15, CreditCardCheckType.Any  , CreditCardType.Enroute));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("2131", 16, CreditCardCheckType.Mod10, CreditCardType.JCB));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("1800", 16, CreditCardCheckType.Mod10, CreditCardType.JCB));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("300" , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("301" , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("302" , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("303" , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("304" , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("305" , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("51"  , 16, CreditCardCheckType.Mod10, CreditCardType.MasterCard));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("52"  , 16, CreditCardCheckType.Mod10, CreditCardType.MasterCard));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("53"  , 16, CreditCardCheckType.Mod10, CreditCardType.MasterCard));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("54"  , 16, CreditCardCheckType.Mod10, CreditCardType.MasterCard));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("55"  , 16, CreditCardCheckType.Mod10, CreditCardType.MasterCard));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("34"  , 15, CreditCardCheckType.Mod10, CreditCardType.Amex));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("37"  , 15, CreditCardCheckType.Mod10, CreditCardType.Amex));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("36"  , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("38"  , 14, CreditCardCheckType.Mod10, CreditCardType.Diners));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("3"   , 16, CreditCardCheckType.Mod10, CreditCardType.JCB));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("4"   , 13, CreditCardCheckType.Mod10, CreditCardType.Visa));
            ValidateCreditCard.CardEntries.Add(new CreditCardEntry("4"   , 16, CreditCardCheckType.Mod10, CreditCardType.Visa));
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// This holds the card details for card lookups.
        /// </summary>
        ///--------------------------------------------------------------------
        private static List<CreditCardEntry> CardEntries { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will return the card type from the account number.
        /// </summary>
        ///--------------------------------------------------------------------
        public static CreditCardType GetCardType(
                LocalDate     currentDate,
                String        cardNumber,
                LocalDate     dateExpiration,
            out CoreErrorCode errorCode)
        {
            if (String.IsNullOrEmpty(cardNumber) == false)
            {
                // Normalize the card-number so that we have a consistent manner
                // in which to validate.
                //
                cardNumber = cardNumber.Replace(" ", String.Empty);
                cardNumber = cardNumber.Replace("-", String.Empty);
                cardNumber = cardNumber.Trim();

                if (Validate(currentDate, cardNumber, dateExpiration, out errorCode))
                {
                    foreach (CreditCardEntry item in ValidateCreditCard.CardEntries)
                    {
                        if (cardNumber.StartsWith(item.PrefixNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            if (CheckAgainstAlgorithm(cardNumber, item.Algorthm))
                            {
                                return item.CardType;
                            }
                        }
                    }

                    errorCode = CoreErrorCode.DeclinedNumber;
                }
            }
            else
            {
                errorCode = CoreErrorCode.DeclinedNumber;
            }

            return CreditCardType.Unknown;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will return the card type from the account number.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String GetOffuscatedNumber(
            String cardNumber)
        {
            return cardNumber.OffuscatedCardNumber();
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will validate the basic card details.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Boolean Validate(
                LocalDate     currentDate,
                String        cardNumber,
                LocalDate     dateExpiration,
            out CoreErrorCode errorCode)
        {
            Boolean isValid = false;

            if (String.IsNullOrEmpty(cardNumber) == false)
            {
                try
                {
                    LocalDateTime endOfMonth     = currentDate.ToEndOfMonth();
                    LocalDateTime expirationDate = new LocalDate(dateExpiration.Year, dateExpiration.Month, 1).ToEndOfMonth();

                    if (expirationDate >= endOfMonth)
                    {
                        isValid   = true;
                        errorCode = CoreErrorCode.Success;
                    }
                    else
                    {
                        errorCode = CoreErrorCode.DeclinedExpiration;
                    }
                }
                catch (Exception)
                {
                    errorCode = CoreErrorCode.DeclinedExpiration;
                }
            }
            else
            {
                errorCode = CoreErrorCode.DeclinedNumber;
            }

            return isValid;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Uses the LUHN formula for validating whether a card-number is 
        /// valid.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Boolean CheckAgainstAlgorithm(
            String              cardNumber,
            CreditCardCheckType algorithm)
        {
            // NOTE: info from http://www.beachnet.com/~hstiles/cardtype.html
            //      
            //       LUHN Formula (Mod 10) for Validation of Primary Account 
            //       Number.  The following steps are required to validate 
            //       the primary account number:
            //
            //         Step 1: Double the value of alternate digits of the 
            //                 primary account number beginning with the second
            //                 digit from the right (the first right--hand 
            //                 digit is the check digit.) 
            //
            //         Step 2: Add the individual digits comprising the
            //                 products obtained in Step 1 to each of the 
            //                 unaffected digits in the original number. 
            //
            //         Step 3: The total obtained in Step 2 must be a number
            //                 ending in zero (30, 40, 50, etc.) for the 
            //                 account number to be validated. 
            //
            //       For example, to validate the primary account number 
            //       49927398716: 
            //
            //         Step 1: 
            //
            //            4 9 9 2 7 3 9 8 7 1 6
            //             x2  x2  x2  x2  x2 
            //         ------------------------
            //             18   4   6  16   2
            //
            //         Step 2: 4 +(1+8)+ 9 + (4) + 7 + (6) + 9 +(1+6) + 7 + (2) + 6
            //
            //         Step 3: Sum = 70 : Card number is validated 
            //
            //         Note: Card is valid because the 70/10 yields no remainder
            //
            //       Make sure you have:
            //         Have started with the rightmost digit (including the 
            //         check digit) (figure odd and even based upon the 
            //         rightmost digit being odd, regardless of the length of
            //         the Credit Card.) ALWAYS work right to left.  The check
            //         digit counts as digit #1 (assuming that the rightmost
            //         digit is the check digit) and is not doubled Double
            //         every second digit (starting with digit # 2 from the 
            //         right).  Remember that when you double a number over 4,
            //         (6 for example) you don't add the result to your total,
            //         but rather the sum of the digits of the result (in the
            //         above example 6*2=12 so you would add 1+2 to your total
            //         (not 12).  Always include the Visa or M/C/ prefix. 
            //     

            // If the algorithm is any then just return true - we have no way
            // to verify.
            //
            if (algorithm == CreditCardCheckType.Any)
            {
                return true;
            }

            Int32 total = 0;

            // NOTE: This may be a better expression for validating the card.
            //
            total = cardNumber.Where(c => Char.IsDigit(c)).Reverse().SelectMany((c, i) => ((c - '0') << (i & 1)).ToString()).Sum(c => c - '0');

            // if the sum mod 10 = 0 then the # is valid.
            //
            return (total % 10) == 0;
        }
        #endregion
    }
}
