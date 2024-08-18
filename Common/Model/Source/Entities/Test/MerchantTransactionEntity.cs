//-----------------------------------------------------------------------------
// <copyright file="MerchantTransactionEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;
    using Codev.Core.Common.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Transaction Entity used by our merchant provider for
    /// testing purposes.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MerchantTransactionEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantTransactionEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
            this.Initialize();
        }
        #endregion

        #region Properties (Base)
        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>   
        ///--------------------------------------------------------------------        
        public MerchantTransactionFlags Flags { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the transaction group.
        /// </summary>   
        ///--------------------------------------------------------------------        
        public Guid TransactionGroup { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the transaction type.
        /// </summary>   
        ///--------------------------------------------------------------------     
        public TransactionType TransactionType { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the payment.
        /// </summary>   
        ///--------------------------------------------------------------------     
        public PaymentAmount Payment { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the order number.
        /// </summary>   
        ///--------------------------------------------------------------------     
        public String OrderNumber { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual MerchantCardEntity Card
        {
            get
            {
                return this.LazyCard.Value;
            }

            set
            {
                this.LazyCard = new Lazy<MerchantCardEntity>(() => value);
            }
        }
        #endregion

        #region Properties (Lazy Loading)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy loading reference for the identity entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<MerchantCardEntity> LazyCard { get; set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Initialize()
        {
            this.LazyCard = new Lazy<MerchantCardEntity>();
        }
        #endregion
    }
}
