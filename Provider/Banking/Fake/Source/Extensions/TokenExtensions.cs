//-----------------------------------------------------------------------------
// <copyright file="TokenExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Fake
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This class contains extensions for converting entities to models.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class TokenExtensions
    {
        #region Methods
        ///------------------------------------------------------------------------
        /// <summary>
        /// Return the token primary key.
        /// </summary>
        ///------------------------------------------------------------------------
        public static Int32 ToId(
            this Token token)
        {
            return Convert.ToInt32(token.Id);
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Return a token for a subscription plan.
        /// </summary>
        ///------------------------------------------------------------------------
        public static Token ToToken(
            this MerchantPlanEntity entity)
        {
            return new Token()
                {
                    Id          = entity.Id.ToString(),
                    IsSuccess   = true,
                    Title       = entity.Name,
                    Description = entity.Name,
                    Type        = "Plan"

                };
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Return a token for a subscription plan.
        /// </summary>
        ///------------------------------------------------------------------------
        public static Token ToToken(
            this MerchantCustomerEntity entity)
        {
            return new Token()
                {
                    Id          = entity.Id.ToString(),
                    IsSuccess   = true,
                    Title       = entity.Name,
                    Description = entity.EmailAddress,
                    Type        = "Customer"
                };
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Return a token for a subscription plan.
        /// </summary>
        ///------------------------------------------------------------------------
        public static Token ToToken(
            this MerchantSubscriptionEntity entity)
        {
            return new Token()
                {
                    Id          = entity.Id.ToString(),
                    IsSuccess   = true,
                    Title       = entity.Customer.EmailAddress,
                    Description = String.Format("{0} : {1}", entity.Plan.Name, entity.Customer.EmailAddress),
                    Type        = "Subscription"
                };
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Return a token for a card.
        /// </summary>
        ///------------------------------------------------------------------------
        public static Token ToToken(
            this MerchantCardEntity entity)
        {
            return new Token()
                {
                    Id          = entity.Id.ToString(),
                    IsSuccess   = true,
                    Title       = entity.Type,
                    Description = String.Format("{0}  (Expires {1:00}/{2:0000})", entity.Number.OffuscatedCardNumber(), entity.DateExpiration.Month, entity.DateExpiration.Year),
                    Type        = "Card"
                };
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Return a token for a transaction.
        /// </summary>
        ///------------------------------------------------------------------------
        public static Token ToToken(
            this MerchantTransactionEntity entity)
        {
            return new Token()
                {
                    Id          = entity.Id.ToString(),
                    IsSuccess   = entity.Flags == MerchantTransactionFlags.Success,
                    Title       = entity.TransactionType.ToString(),
                    Description = entity.OrderNumber,
                    Type        = "Transaction"
                };
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to a credit card.
        /// </summary>
        ///------------------------------------------------------------------------
        public static PaymentCreditCard ToCreditCard(
            this MerchantCardEntity entity)
        {
            return new PaymentCreditCard()
                {
                    Name           = entity.Name,
                    Number         = entity.Number,
                    Code           = entity.Code,
                    DateExpriation = entity.DateExpiration,

                    BillingAddress = new AddressInfo()
                        {
                           Street1     = String.Empty,
                           Street2     = String.Empty,
                           Street3     = String.Empty,
                           City        = String.Empty,
                           State       = String.Empty,
                           PostalCode  = String.Empty,
                           CountryCode = String.Empty
                        }
                };
        }
        #endregion
    }
}
