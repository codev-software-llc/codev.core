//-----------------------------------------------------------------------------
// <copyright file="CoreServiceException.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This exception is thrown if there's an error encountered with the
    /// service.
    /// </summary>
    ///------------------------------------------------------------------------
    [Serializable]
    public class CoreServiceException : Exception
    {
        #region Constructors
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Construct the exception.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreServiceException(
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
        public CoreServiceException(
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
