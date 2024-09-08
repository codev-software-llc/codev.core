//-----------------------------------------------------------------------------
// <copyright file="CoreErrorCode.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    ///------------------------------------------------------------------------
    /// <summary>
    /// This is a generated file for the CoreErrorCode enumeration.
    /// </summary>
    ///------------------------------------------------------------------------
    public enum CoreErrorCode : int
    {
        Success            =  0, // Success
        InternalFailure    =  1, // Internal Failure
        InvalidOperation   =  2, // Invalid Operation
        DoesNotExist       =  3, // Does not exist
        Duplicate          =  4, // Duplicate
        AccessDenied       =  5, // Access Denied
        InvalidParameter   =  6, // Invalid Argument
        ValidationFailure  =  7, // Failed Validation
        DeclinedCvv        =  8, // Declined CVV Validation
        DeclinedExpiration =  9, // Declined Card Expiration
        DeclinedNumber     = 10, // Declined Card Number
        DeclinedFunds      = 11, // Declined Insufficient Funds
        DeclinedCardType   = 12, // Card is not supported
        ServiceFailure     = 13  // Service Communication Failure
    }
}
