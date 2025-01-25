//-----------------------------------------------------------------------------
// <copyright file="SmtpMessage.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the message used for sending SMTP email.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class SmtpMessage : BaseMessage
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public SmtpMessage() : base()
        {
            this.Headers = new List<MailHeader>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Defines the properties for the message.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<MailHeader> Headers  { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the to email name (list).
        /// </summary>
        ///--------------------------------------------------------------------
        public String To { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the to name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ToName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email bcc email list.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Bcc { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set a bcc name string.
        /// </summary>
        ///--------------------------------------------------------------------
        public String BccName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the from email address.
        /// </summary>
        ///--------------------------------------------------------------------
        public String From { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the from name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String FromName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email subject text.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Subject { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the email body.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Body { get; set; }
        #endregion
    }
}