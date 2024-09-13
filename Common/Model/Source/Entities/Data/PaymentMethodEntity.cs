//-----------------------------------------------------------------------------
// <copyright file="PaymentMethodEntity.cs" company="Codev Software, LLC">
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
    /// Account entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentMethodEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethodEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethodEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethodFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the expiration format (MM/YYYY) for the expiration.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Expiration { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the offuscated number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String OffuscatedNumber { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set payment method specific information.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Token { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity reference.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity Identity { get; set; }
        #endregion
    }
}