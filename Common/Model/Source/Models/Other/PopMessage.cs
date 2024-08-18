//-----------------------------------------------------------------------------
// <copyright file="PopMessage.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;
    using System.Collections.Generic;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the message for pulling POP3 messages.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class PopMessage
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public PopMessage(
            List<String> headers)
        {
            this.Headers = headers;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the headers for the message.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<String> Headers { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the subject for the message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Subject { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Fet or set the body of the message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Body { get; set; }
        #endregion
    }
}