//-----------------------------------------------------------------------------
// <copyright file="MailHeader.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the message used for an SMTP header.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class MailHeader
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public MailHeader()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Defines the properties for a payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name  { get; set; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the value of the mail header.
        /// </summary>
        /// -------------------------------------------------------------------
        public String Value { get; set; }
        #endregion
    }
}