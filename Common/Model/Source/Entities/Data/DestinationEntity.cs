//-----------------------------------------------------------------------------
// <copyright file="DestinationEntity.cs" company="Codev Software, LLC">
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
    /// This is the Destination entity for authentication.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DestinationEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public DestinationEntity(
            Instant instantNow) : base(instantNow)
        {
            this.Initialize();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public DestinationEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
            this.Initialize();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public DestinationFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date the confirmation secret expires.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant DateConfirmationExpires { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the addres (email, twitter).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Address { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the type of destination.
        /// </summary>
        ///--------------------------------------------------------------------
        public DestinationType DestinationType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the confirmation secret.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ConfirmationSecret { get; set; }
        #endregion

        #region Properties (Reference)
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
            this.LazyIdentity = new Lazy<IdentityEntity>();
        }
        #endregion
    }
}
