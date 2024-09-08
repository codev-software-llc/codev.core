//-----------------------------------------------------------------------------
// <copyright file="MerchantCardEntity.cs" company="Codev Software, LLC">
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
    /// This represents an entity for cards.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MerchantCardEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCardEntity(
            Instant instantNow) : base(instantNow)
        {
            this.Initialize();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCardEntity(
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
        public MerchantCardFlags Flags { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the type of card (Visa, Mastercard, ...)
        /// </summary>   
        ///--------------------------------------------------------------------  
        public String Type { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the card holder name.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public String Name { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the card number.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public String Number { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the card code.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public String Code { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the expiration year.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public LocalDate DateExpiration { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer.
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
            this.LazyCustomer = new Lazy<MerchantCustomerEntity>();
        }
        #endregion
    }
}
