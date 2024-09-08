//-----------------------------------------------------------------------------
// <copyright file="SettingEntity.cs" company="Codev Software, LLC">
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
    /// This is the common key-value pair setting.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SettingEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public SettingEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public SettingEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties (Base)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the key that can be used to bind entities.
        /// </summary>
        ///--------------------------------------------------------------------
        public SettingFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the key that can be used to bind entities.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the value of the setting.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Value { get; set; }
        #endregion
    }
}
