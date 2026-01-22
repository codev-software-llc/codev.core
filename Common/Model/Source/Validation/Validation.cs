//-----------------------------------------------------------------------------
// <copyright file="Validation.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a validation utilities.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Validation
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will validate the payment information.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void Payment(
            LocalDate         currentDate,
            PaymentCreditCard paymentCard)
        {

            if (paymentCard != null)
            {
                CoreErrorCode errorCode;

                CreditCardType cardType = ValidateCreditCard.GetCardType(currentDate, paymentCard.Number, paymentCard.DateExpriation, out errorCode);

                if (cardType == CreditCardType.Unknown)
                {
                    throw new ArgumentException("Failed to validate the card");
                }
            }
            else
            {
                throw new ArgumentException("No payment information was provided.");
            }

            // Validate the CVC code.
            //
            if (String.IsNullOrWhiteSpace(paymentCard.Code))
            {
                throw new ArgumentException("Invalid CVC code.");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will validate the token information.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void Token(
            Token paymentToken)
        {
            if (String.IsNullOrWhiteSpace(paymentToken.Id) || String.IsNullOrWhiteSpace(paymentToken.Title))
            {
                throw new ArgumentException("Token has no valid information");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will validate the payment amount.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void Amount(
            PaymentAmount paymentAmount)
        {
            if (paymentAmount.Amount <= 0.00m)
            {
                throw new ArgumentException("Payment cannot be zero");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will validate the payment receipt information.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void Receipt(
            Payment  paymentReceipt,
            TransactionType transactionType)
        {
            Validation.ValidateParameter<Payment>("paymentReceipt", paymentReceipt);

            Validation.Token(paymentReceipt.PaymentToken);

            if (paymentReceipt.TransactionType != transactionType)
            {
                throw new ArgumentException("Invalid transaction type for receipt");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Validate the parameter.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public static void ValidateParameter<T>(
            String name,
            T      value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }

            if (typeof(T) == typeof(String))
            {
                if (String.IsNullOrWhiteSpace(value as String))
                {
                    throw new ArgumentException(name);
                }
            }
            else if (typeof(T) == typeof(Guid))
            {
                Guid? guidInfo = value as Guid?;

                if (guidInfo == Guid.Empty)
                {
                    throw new ArgumentException("Guid");
                }
            }
            else if (typeof(T) == typeof(AddressInfo))
            {
                AddressInfo addressInfo = value as AddressInfo;

                if (String.IsNullOrWhiteSpace(addressInfo.Street1))
                {
                    throw new ArgumentException(String.Format("{0}.Street1", name));
                }

                if (String.IsNullOrWhiteSpace(addressInfo.City))
                {
                    throw new ArgumentException(String.Format("{0}.City", name));
                }

                if (String.IsNullOrWhiteSpace(addressInfo.State))
                {
                    throw new ArgumentException(String.Format("{0}.State", name));
                }

                if (String.IsNullOrWhiteSpace(addressInfo.PostalCode))
                {
                    throw new ArgumentException(String.Format("{0}.PostalCode", name));
                }
            }
            else if (typeof(T) == typeof(SmtpMessage))
            {
                SmtpMessage emailMessage = value as SmtpMessage;

                if (String.IsNullOrWhiteSpace(emailMessage.To))
                {
                    throw new ArgumentException(String.Format("{0}.To", name));
                }

                if (String.IsNullOrWhiteSpace(emailMessage.From))
                {
                    throw new ArgumentException(String.Format("{0}.From", name));
                }

                if (String.IsNullOrWhiteSpace(emailMessage.Subject))
                { 
                    throw new ArgumentException(String.Format("{0}.Subject", name));
                }

                if (String.IsNullOrWhiteSpace(emailMessage.Body))
                {
                    throw new ArgumentException(String.Format("{0}.Body", name));
                }
            } 
            else if (typeof(T) == typeof(Payment))
            {
                Payment receipt = value as Payment;

                if (receipt.PaymentMethodToken == null)
                {
                    throw new ArgumentException(String.Format("{0}.PaymentMethodToken", name));
                }

                if (receipt.PaymentToken == null)
                {
                    throw new ArgumentException(String.Format("{0}.PaymentToken", name));
                }

                if (receipt.PaymentAmount.Amount <= 0.00m)
                {
                    throw new ArgumentException(String.Format("{0}.PaymentAmount", name));
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Validate the parameter and return the default value if it fails
        /// </summary>
        ///-------------------------------------------------------------------- 
        public static T ValidateDefault<T>(
            String name,
            T      value,
            T      defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }

            if ((typeof(T) == typeof(String)) && String.IsNullOrWhiteSpace(value as String))
            {
                return defaultValue;
            }

            return value;
        }
        #endregion
    }
}
