//-----------------------------------------------------------------------------
// <copyright file="MerchantCustomerEntity.cs" company="Codev Software, LLC">
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
    public class MerchantCustomerEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCustomerEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCustomerEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties (Base)
        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>   
        ///--------------------------------------------------------------------        
        public MerchantCustomerFlags Flags { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the name of the customer.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public String Name { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the email address.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public String EmailAddress { get; set; }
        #endregion
    }
}
