//-----------------------------------------------------------------------------
// <copyright file="SessionEntity.cs" company="Codev Software, LLC">
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
    /// This is a core user session token.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SessionEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public SessionEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public SessionEntity(
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
        public SessionFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the expiration date for the session.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant DateExpiration { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the session secret.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Secret { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Foreign key references initialized by the lazy-loading process.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual IdentityEntity Identity { get; set; }
        #endregion
    }
}
