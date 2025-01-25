//-----------------------------------------------------------------------------
// <copyright file="CreditCardCheckType.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the type of algorithm for checking credit card validity.
    /// </summary>
    ///------------------------------------------------------------------------
    public enum CreditCardCheckType : int
    {
        Any = 0,
        Mod10
    }
}
