//-----------------------------------------------------------------------------
// <copyright file="CurrencySymbol.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the supported currency symbols.
    /// </summary>
    ///------------------------------------------------------------------------
    public struct CurrencySymbol
    {
        ///--------------------------------------------------------------------
        /// <summary>
        /// Currency symbol for US dollar.
        /// </summary>
        ///--------------------------------------------------------------------
        public const String Usd = "$";

        ///--------------------------------------------------------------------
        /// <summary>
        /// Currency symbol for canadian dollar.
        /// </summary>
        ///--------------------------------------------------------------------
        public const String Cad = "$";

        ///--------------------------------------------------------------------
        /// <summary>
        /// Currency symbol for the euro.
        /// </summary>
        ///--------------------------------------------------------------------
        public const String Eur = "€";
    }
}
