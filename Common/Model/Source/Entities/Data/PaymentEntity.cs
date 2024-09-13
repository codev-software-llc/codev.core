//-----------------------------------------------------------------------------
// <copyright file="PaymentEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Payment entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the order number for the payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public String OrderNumber { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the status of the payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentStatus PaymentStatus { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment amount.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount PaymentAmount { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set payment provider data.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Token { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethodEntity PaymentMethod {  get; set; }
        #endregion
    }
}