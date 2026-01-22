//-----------------------------------------------------------------------------
// <copyright file="PaymentMethod.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Account model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentMethod : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the model.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethod() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the payment method is the default.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsPrimary { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the type of payment method (visa, mastercard).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Type { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the offuscated number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String OffuscatedNumber { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the method expiration.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Expiration { get; set; }
        #endregion
    }
}