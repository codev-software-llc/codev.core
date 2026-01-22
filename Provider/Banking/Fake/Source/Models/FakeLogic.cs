//-----------------------------------------------------------------------------
// <copyright file="FakeLogic.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Fake
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides testing criteria for fake card processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class FakeLogic
    {
        #region Constants (Fake Cards)
        public const String FakeCardAmex           = "378282246310005";    ////    1.00 Limit
        public const String FakeCardDiscover       = "6011111111111117";   //// 1000.00 Limit
        public const String FakeCardMastercard     = "5555555555554444";   ////  100.00 Limit
        public const String FakeCardVisaUS         = "4012888888881881";   //// 5000.00 Limit
        public const String FakeCardVisaBadCode    = "4111111111111111";   //// Merchant Bad CVV
        public const String FakeCardVisaBadExpired = "4222222222222";      //// Merchant Bad Date
        public const String FakeCardVisaBadNumber  = "4012000077777777";   //// Merchant Bad Number       
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the fake data.
        /// </summary>
        ///--------------------------------------------------------------------
        static FakeLogic()
        {
            FakeLogic.InitializeFakeCards();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Fake data tables and properties.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Dictionary<String, FakeCard> FakeCards { get; private set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Validate the card criteria.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Boolean Validate(
                LocalDate         currentDate,
                PaymentCreditCard paymentCard,
                PaymentAmount     paymentAmount,
            out CoreErrorCode     errorCode)
        {
            Boolean isValid = true;

            if (String.IsNullOrEmpty(paymentCard.Number) == false)
            {
                FakeCard fakeCard;

                if (FakeCards.TryGetValue(paymentCard.Number, out fakeCard))
                {
                    isValid = false;

                    // Validate the card expected failures.
                    //
                    switch (fakeCard.ExpectedFailure)
                    {
                        case FakeFailureType.InvalidCardNumber:
                            errorCode = CoreErrorCode.DeclinedNumber;
                            break;

                        case FakeFailureType.InvalidCVV:
                            errorCode = CoreErrorCode.DeclinedCvv;
                            break;

                        case FakeFailureType.CardExpired:
                            errorCode = CoreErrorCode.DeclinedExpiration;
                            break;

                        case FakeFailureType.None:

                            if (paymentAmount.Amount > fakeCard.CardBalance)
                            {
                                errorCode = CoreErrorCode.DeclinedFunds;
                            }
                            else
                            {
                                isValid   = true;
                                errorCode = CoreErrorCode.Success;
                            }

                            break;

                        default:
                            isValid = true;
                            errorCode = CoreErrorCode.Success;
                            break;
                    }
                }
                else
                {
                    isValid = FakeLogic.ValidateLuhn(currentDate, paymentCard, out errorCode);
                }
            }
            else
            {
                isValid   = false;
                errorCode = CoreErrorCode.DeclinedNumber;
            }

            return isValid;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary> (private)
        /// Initialize a dictionary of Fake Cards with expected behaviors.
        /// </summary>
        ///--------------------------------------------------------------------
        private static void InitializeFakeCards()
        {
            FakeCards = new Dictionary<String, FakeCard>();

            FakeCards.Add(FakeCardAmex          , new FakeCard() { CardNumber = FakeCardAmex          , CardCode = "123", CardHolder = "John Doe"    , DateExpiration = new LocalDate(2018,  7, 1), CardBalance = 1.00m   , ExpectedFailure = FakeFailureType.None              });
            FakeCards.Add(FakeCardDiscover      , new FakeCard() { CardNumber = FakeCardDiscover      , CardCode = "456", CardHolder = "Sally May"   , DateExpiration = new LocalDate(2017,  4, 1), CardBalance = 1000.00m, ExpectedFailure = FakeFailureType.None              });
            FakeCards.Add(FakeCardMastercard    , new FakeCard() { CardNumber = FakeCardMastercard    , CardCode = "789", CardHolder = "Jimmy John"  , DateExpiration = new LocalDate(2017,  8, 1), CardBalance = 100.00m , ExpectedFailure = FakeFailureType.None              });
            FakeCards.Add(FakeCardVisaUS        , new FakeCard() { CardNumber = FakeCardVisaUS        , CardCode = "444", CardHolder = "Seymore Cash", DateExpiration = new LocalDate(2018, 12, 1), CardBalance = 5000.00m, ExpectedFailure = FakeFailureType.None              });
            FakeCards.Add(FakeCardVisaBadCode   , new FakeCard() { CardNumber = FakeCardVisaBadCode   , CardCode = "123", CardHolder = "Vance Victor", DateExpiration = new LocalDate(2018, 12, 1), CardBalance = 5000.00m, ExpectedFailure = FakeFailureType.InvalidCVV        });
            FakeCards.Add(FakeCardVisaBadExpired, new FakeCard() { CardNumber = FakeCardVisaBadExpired, CardCode = "456", CardHolder = "Ayn Rand"    , DateExpiration = new LocalDate(2017, 11, 1), CardBalance = 5000.00m, ExpectedFailure = FakeFailureType.CardExpired       });
            FakeCards.Add(FakeCardVisaBadNumber , new FakeCard() { CardNumber = FakeCardVisaBadNumber , CardCode = "789", CardHolder = "Oscar Meyer" , DateExpiration = new LocalDate(2018, 10, 1), CardBalance = 5000.00m, ExpectedFailure = FakeFailureType.InvalidCardNumber });
        }

        ///--------------------------------------------------------------------
        /// <summary> (private)
        /// Validate the LUHN validity of the card.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Boolean ValidateLuhn(
                LocalDate         currentDate,
                PaymentCreditCard paymentCard,
            out CoreErrorCode     errorCode)
        {
            CreditCardType cardType = ValidateCreditCard.GetCardType(currentDate, paymentCard.Number, paymentCard.DateExpriation, out errorCode);

            return (cardType != CreditCardType.Unknown);
        }
        #endregion
    }
}
