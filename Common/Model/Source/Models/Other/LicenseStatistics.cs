//-----------------------------------------------------------------------------
// <copyright file="LicenseStatistics.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides statistical information on a license.
    /// </summary>
    ///------------------------------------------------------------------------
    public class LicenseStatistics
    {
        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        /// -------------------------------------------------------------------
        public LicenseStatistics()
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
        /// Get the number of users subscribed to the license.
        /// </summary>
        /// -------------------------------------------------------------------
        public Int32 SubscribedCount { get; set; }
        #endregion
    }
}
