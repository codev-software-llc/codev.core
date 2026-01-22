//-----------------------------------------------------------------------------
// <copyright file="EnumType.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the EnumType model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class EnumType : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EnumType() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the enum type is a flag.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsFlag { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the enum type is a big size.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsBig { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the application name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Application { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the enum name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the application enum key.
        /// </summary>
        ///--------------------------------------------------------------------
        public String EnumKey { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the application value.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int64 Value { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the comment.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Comment { get; set; }
        #endregion
    }
}
