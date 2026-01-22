//-----------------------------------------------------------------------------
// <copyright file="FakeFailureType.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the type of failures we can get through fake card
    /// processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public enum FakeFailureType : int
    {
        None          = 0,
        CardExpired      ,
        InvalidCardNumber,
        InvalidCVV       ,
        InvalidAVS
    }
}