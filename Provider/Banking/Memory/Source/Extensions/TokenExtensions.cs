//-----------------------------------------------------------------------------
// <copyright file="TokenExtensions.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
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
        /// Return a token for a subscription plan.
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
        #endregion
    }
}
