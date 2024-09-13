//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkEntity.cs" company="Codev Software, LLC">
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
    /// This represents the site service link entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ServiceLinkEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceLinkEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceLinkEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceLinkFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set unique tiny url.
        /// </summary>
        ///--------------------------------------------------------------------
        public String TinyUrl { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set type of the detail parameter.  Usually this is the type
        /// name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String DetailType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the service link specific details.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Detail { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual IdentityEntity Identity { get; set; }
        #endregion
    }
}
