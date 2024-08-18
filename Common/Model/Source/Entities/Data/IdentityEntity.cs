//-----------------------------------------------------------------------------
// <copyright file="IdentityEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;
    using Codev.Core.Common.Base;
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
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity(
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
        #endregion
    }
}
