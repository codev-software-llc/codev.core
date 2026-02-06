//-----------------------------------------------------------------------------
// <copyright file="IdentityEntity.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is a core identity entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class IdentityEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity(
            Instant instantNow) : base(instantNow)
        {
            this.SerializedData = String.Empty;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
            this.SerializedData = String.Empty;
        }       
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the count of confirmation attempts for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 ConfirmationAttempts { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the time zone for the user.
        /// </summary>
        ///--------------------------------------------------------------------
        public DateTimeZone TimeZone { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the serialized data that is specific to the application
        /// that consumes this entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public String SerializedData { get; set; }
        #endregion
    }
}
