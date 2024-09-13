//-----------------------------------------------------------------------------
// <copyright file="SubscriptionEntity.cs" company="Codev Software, LLC">
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
    /// This is the license subscription entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SubscriptionEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public SubscriptionEntity(
            Instant instantNow) : base(instantNow)
        {
            this.Initialize();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public SubscriptionEntity(
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
        public SubscriptionFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date the subscription expires.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant DateExpiration { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set subscription specific information.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Token { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual LicenseEntity License
        {
            get
            {
                return this.LazyLicense.Value;
            }

            set
            {
                this.LazyLicense = new Lazy<LicenseEntity>(() => value);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual IdentityEntity Identity
        {
            get
            {
                return this.LazyIdentity.Value;
            }

            set
            {
                this.LazyIdentity = new Lazy<IdentityEntity>(() => value);
            }
        }
        #endregion

        #region Properties (Lazy Loading)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy loading reference for the license entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<LicenseEntity> LazyLicense { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy loading reference for the identity entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<IdentityEntity> LazyIdentity { get; set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Initialize()
        {
            this.LazyLicense  = new Lazy<LicenseEntity>();
            this.LazyIdentity = new Lazy<IdentityEntity>();
        }
        #endregion
    }
}
