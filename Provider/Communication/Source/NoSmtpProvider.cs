//-----------------------------------------------------------------------------
// <copyright file="NoSmtpProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider
{
    using System;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IEMailClient interface for POP3 email processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class NoSmtpProvider : IEmailClientProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the server object.
        /// </summary>
        ///--------------------------------------------------------------------
        public NoSmtpProvider()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Email settings for SMTP client.
        /// </summary>
        ///--------------------------------------------------------------------
        public String SystemEmailAddress { get; private set; }
        public String SystemEmailName { get; private set; }
        public Int32 EmailFailingThreshold { get; private set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Send email.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Send(
            SmtpMessage message)
        {
            //
            // DO NOTHING.
            //
        }
        #endregion
    }
}
