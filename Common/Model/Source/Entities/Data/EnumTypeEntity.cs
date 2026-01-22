//-----------------------------------------------------------------------------
// <copyright file="EnumTypeEntity.cs" company="Codev Software, LLC">
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
    /// This represents the information needed to generate enumerations
    /// in our code eco-system.
    /// </summary>
    ///------------------------------------------------------------------------
    public class EnumTypeEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EnumTypeEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EnumTypeEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the flags for the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EnumTypeFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the enumeration is a Flag type.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsFlag { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether its a size of 64 bits.  Otherwise, it's a
        /// size of 32 bits.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsBig { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the application this enumeration belongs.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Application { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the enumeration.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the key part of the value pair.
        /// </summary>
        ///--------------------------------------------------------------------
        public String EnumKey { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set enumeration value.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int64 Value { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the comment to be used for the enumeration.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Comment { get; set; }
        #endregion
    }
}