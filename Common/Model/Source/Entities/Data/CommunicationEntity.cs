//-----------------------------------------------------------------------------
// <copyright file="CommunicationEntity.cs" company="Codev Software, LLC">
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
    /// This is the Communication entity for contact and support.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CommunicationEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public CommunicationEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public CommunicationEntity(
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
        public CommunicationFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the application name (timecog, velocitix, ...)
        /// </summary>
        ///--------------------------------------------------------------------
        public String Application { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the category type (contact, support, ...).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Category { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the subcategory (website, phone, tray, ...).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Subcategory { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set email address.
        /// </summary>
        ///--------------------------------------------------------------------
        public String EmailAddress { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the application comments.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Comments { get; set; }
        #endregion
    }
}
