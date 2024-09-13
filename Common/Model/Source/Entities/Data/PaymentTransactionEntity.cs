//-----------------------------------------------------------------------------
// <copyright file="PaymentTransactionEntity.cs" company="Codev Software, LLC">
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
    /// This is a payment transaction associated to a Payment.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentTransactionEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentTransactionEntity(
            Instant instantNow) : base(instantNow)
        {
            this.Initialize();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentTransactionEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
            this.Initialize();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the flags for the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentTransactionFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the tranaction type.
        /// </summary>
        ///--------------------------------------------------------------------
        public TransactionType TransactionType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment amount.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount PaymentAmount { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment reference.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentEntity Payment
        {
            get
            {
                return this.LazyPayment.Value;
            }

            set
            {
                this.LazyPayment = new Lazy<PaymentEntity>(() => value);
            }
        }
        #endregion

        #region Properties (Lazy)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy-load property for the payment association.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<PaymentEntity> LazyPayment { get; set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the entity references.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Initialize()
        {
            this.LazyPayment = new Lazy<PaymentEntity>();
        }
        #endregion
    }
}