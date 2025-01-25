//-----------------------------------------------------------------------------
// <copyright file="VersionInfo.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the version information.
    /// </summary>
    ///------------------------------------------------------------------------
    public class VersionInfo
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public VersionInfo()
        {
        }
        #endregion

        #region Properties
        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the name of the application (Product Name).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the optional application key.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ApplicationKey { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the company name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String CompanyName { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the version string (Product Version).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Version { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the copyright string.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Copyright { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the build configuration.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Configuration { get; set; }
        #endregion
    }
}