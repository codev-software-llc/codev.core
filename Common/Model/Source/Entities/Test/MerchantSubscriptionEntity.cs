//-----------------------------------------------------------------------------
// <copyright file="MerchantSubscriptionEntity.cs" company="Codev Software, LLC">
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
    /// This represents an entity with tokenized information.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MerchantSubscriptionEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantSubscriptionEntity(
            Instant instantNow) : base(instantNow)
        {
            this.Initialize();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantSubscriptionEntity(
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
        public MerchantSubscriptionFlags Flags { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual MerchantPlanEntity Plan
        {
            get
            {
                return this.LazyPlan.Value;
            }

            set
            {
                this.LazyPlan = new Lazy<MerchantPlanEntity>(() => value);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual MerchantCustomerEntity Customer
        {
            get
            {
                return this.LazyCustomer.Value;
            }

            set
            {
                this.LazyCustomer = new Lazy<MerchantCustomerEntity>(() => value);
            }
        }
        #endregion

        #region Properties (Lazy Loading)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy loading reference for the identity entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<MerchantPlanEntity> LazyPlan { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy loading reference for the identity entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<MerchantCustomerEntity> LazyCustomer { get; set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Initialize()
        {
            this.LazyPlan     = new Lazy<MerchantPlanEntity>();
            this.LazyCustomer = new Lazy<MerchantCustomerEntity>();
        }
        #endregion
    }
}
