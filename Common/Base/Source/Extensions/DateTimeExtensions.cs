//-----------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Text;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Extends the DateTime class.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class DateTimeExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert date time to bytes.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Byte[] ToBytes(
            this DateTime value)
        {
            Int64 encoded = value.ToBinary();

            return Encoding.UTF8.GetBytes(encoded.ToString());
        }
        #endregion
    }
}
