//-----------------------------------------------------------------------------
// <copyright file="SmsMessage.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the message used for sending SMS messages.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class SmsMessage : BaseMessage
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public SmsMessage() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the phone number to send to.
        /// </summary>
        ///--------------------------------------------------------------------
        public String PhoneTo { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set a url to contain a media item.
        /// </summary>
        ///--------------------------------------------------------------------
        public String MediaUrl { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the body of the sms message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Body { get; set; }
        #endregion
    }
}