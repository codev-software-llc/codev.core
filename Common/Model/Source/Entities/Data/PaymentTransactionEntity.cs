//-----------------------------------------------------------------------------
// <copyright file="PaymentTransactionEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
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
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentTransactionEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
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
        public PaymentEntity Payment { get; set; }
        #endregion
    }
}