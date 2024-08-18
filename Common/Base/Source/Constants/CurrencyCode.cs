//-----------------------------------------------------------------------------
// <copyright file="CurrencyCode.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the supported currency codes.
    /// </summary>
    ///------------------------------------------------------------------------
    public struct CurrencyCode
    {
        ///--------------------------------------------------------------------
        /// <summary>
        /// Currency code for United States dollar.
        /// </summary>
        ///--------------------------------------------------------------------
        public const String Usd = "USD";

        ///--------------------------------------------------------------------
        /// <summary>
        /// Currency code for Canada dollar.
        /// </summary>
        ///--------------------------------------------------------------------
        public const String Cad = "CAD";

        ///--------------------------------------------------------------------
        /// <summary>
        /// Currency code for Euro.
        /// </summary>
        ///--------------------------------------------------------------------
        public const String Eur = "EUR";
    }
}
