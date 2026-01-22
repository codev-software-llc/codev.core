//-----------------------------------------------------------------------------
// <copyright file="CoreDataException.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This exception is thrown if there's an error encountered with the
    /// Data component.
    /// </summary>
    ///------------------------------------------------------------------------
    [Serializable]
    public class CoreDataException : Exception
    {
        #region Constructors
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Construct the exception.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreDataException(
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
        public CoreDataException(
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
