//-----------------------------------------------------------------------------
// <copyright file="DecimalExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// These are model extension converters.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class DecimalExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return limited number of decimal places.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String To2Places(
            this Decimal value)
        {
            return String.Format("{0:0.00}", value);
        }
        #endregion
    }
}