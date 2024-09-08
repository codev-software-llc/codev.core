//-----------------------------------------------------------------------------
// <copyright file="PaymentReceipt.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the receipt returned from payment processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Payment
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Payment()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------     
        /// <summary>
        /// Get or set the type of transaction for the receipt.
        /// </summary>
        ///--------------------------------------------------------------------
        public TransactionType TransactionType { get; set; }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Get or set the payment method token.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token PaymentMethodToken { get; set; }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Get or set the payment token.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token PaymentToken { get; set; }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Get or set the order number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String OrderNumber { get; set; }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Get or set the payment amount.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount PaymentAmount { get; set; }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Get or set the error code if one occurred.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreErrorCode ErrorCode { get; set; }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Get or set the error message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ErrorMessage { get; set; }
        #endregion
    }
}