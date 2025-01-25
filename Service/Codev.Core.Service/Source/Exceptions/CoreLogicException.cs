//-----------------------------------------------------------------------------
// <copyright file="CoreLogicException.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This exception is thrown for logic exceptions.
    /// </summary>
    ///------------------------------------------------------------------------
    [Serializable]
    public class CoreLogicException : Exception
    {
        #region Constructors
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Construct the exception.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreLogicException(
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
        public CoreLogicException(
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
