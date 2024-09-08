//-----------------------------------------------------------------------------
// <copyright file="CoreProviderException.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This exception is thrown if there's an error encountered with the
    /// provider.
    /// </summary>
    ///------------------------------------------------------------------------
    [Serializable]
    public class CoreProviderException : Exception
    {
        #region Constructors
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Construct the exception.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreProviderException(
            CoreErrorCode errorCode,
            String        message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Construct the exception.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreProviderException(
            CoreErrorCode errorCode,
            Exception     innerException) : base(innerException.Message, innerException)
        {
            this.ErrorCode = errorCode;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the exception code.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreErrorCode ErrorCode { get; private set; }
        #endregion
    }
}
