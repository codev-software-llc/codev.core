//-----------------------------------------------------------------------------
// <copyright file="LicenseFeature.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides an object for a Licensing feature item.
    /// </summary>
    ///------------------------------------------------------------------------
    public class LicenseFeature
    {
        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// -------------------------------------------------------------------
        public LicenseFeature()
        {
        }
        #endregion

        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the feature name.
        /// </summary>
        /// -------------------------------------------------------------------
        public String Name { get; set; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the feature is enabled.
        /// </summary>
        /// -------------------------------------------------------------------
        public Boolean IsEnabled { get; set; }
        #endregion
    }
}
