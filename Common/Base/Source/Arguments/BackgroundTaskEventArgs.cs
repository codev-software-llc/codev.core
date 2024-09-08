//-----------------------------------------------------------------------------
// <copyright file="BackgroundTaskEventArgs.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This argument is used for notifications on the BackgroundTask object.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BackgroundTaskEventArgs : EventArgs
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public BackgroundTaskEventArgs()
        {
            this.IsSuccess = true;
            this.Message   = String.Empty;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public BackgroundTaskEventArgs(
            Exception exception)
        {
            this.Exception = exception;

            if (this.Exception != null)
            {
                this.Message   = exception.Message;
                this.IsSuccess = false;
            }
            else
            {
                this.Message   = String.Empty;
                this.IsSuccess = true;
            }
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the argument is successful.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsSuccess { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the top level message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Message { get; private set; }

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Get or set the exception record if there is one.
        /// </summary>
        ///--------------------------------------------------------------------
        public Exception Exception { get; private set; }
        #endregion
    }
}