//-----------------------------------------------------------------------------
// <copyright file="ServiceLink.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the ServiceLink model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ServiceJob : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceJob() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date created.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateCreated { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date modified.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateModified { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the tiny URL.
        /// </summary>
        ///--------------------------------------------------------------------
        public String TinyUrl { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the detail type.
        /// </summary>
        ///--------------------------------------------------------------------
        public String DetailType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the detail.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Detail { get; set; }
        #endregion
    }
}
