//-----------------------------------------------------------------------------
// <copyright file="PaymentEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;
    using Codev.Core.Common.Base;
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
            this.Initialize();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
            this.Initialize();
        }
        #endregion

        #region Properties (Base)
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
        public PaymentMethodEntity PaymentMethod
        {
            get
            {
                return this.LazyPaymentMethod.Value;
            }

            set
            {
                this.LazyPaymentMethod = new Lazy<PaymentMethodEntity>(() => value);
            }
        }
        #endregion

        #region Properties (Lazy)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy-load property for payment reference.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<PaymentMethodEntity> LazyPaymentMethod { private get; set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the entity references.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Initialize()
        {
            this.LazyPaymentMethod = new Lazy<PaymentMethodEntity>();
        }
        #endregion
    }
}