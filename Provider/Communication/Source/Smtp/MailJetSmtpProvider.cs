//-----------------------------------------------------------------------------
// <copyright file="MailJetSmtpProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IEMailClient interface for POP3 email processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MailJetSmtpProvider : IEmailClientProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the server object.
        /// </summary>
        ///--------------------------------------------------------------------
        public MailJetSmtpProvider(
            String host,
            String port,
            String userName,
            String userPassword,
            String fromAddress,
            String fromAddressName)
        {
            this.Host         = Environment.ExpandEnvironmentVariables(host);
            this.UserName     = Environment.ExpandEnvironmentVariables(userName);
            this.UserPassword = Environment.ExpandEnvironmentVariables(userPassword);
            this.Port         = Convert.ToInt32(Environment.ExpandEnvironmentVariables(port));
            this.FromAddress  = fromAddress;
            this.FromName     = fromAddressName;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the from address.
        /// </summary>
        ///--------------------------------------------------------------------
        private String FromAddress { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the from name.
        /// </summary>
        ///--------------------------------------------------------------------
        private String FromName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the host for the provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private String Host { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the port for the SMTP gateway.
        /// </summary>
        ///--------------------------------------------------------------------
        private Int32 Port { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the username for the SMTP gateway.
        /// </summary>
        ///--------------------------------------------------------------------
        private String UserName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the username for the SMTP gateway.
        /// </summary>
        ///--------------------------------------------------------------------
        private String UserPassword { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Send email.  This is a synchronous operation and as such is 
        /// blocking.  It is recommended that email is asynchronously 
        /// controlled from the outside caller.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Send(
            SmtpMessage message)
        {
            //
            // Don't Use.
            //
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Send email.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task SendAsync(
            SmtpMessage content)
        {
            await Task.CompletedTask;

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(this.FromName, this.FromAddress));
            message.To.Add(MailboxAddress.Parse(content.To));

            message.Subject = content.Subject;
            message.Body    = new TextPart("plain") { Text = content.Body };

            using var client = new SmtpClient();

            await client.ConnectAsync(this.Host, this.Port, SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(this.UserName, this.UserPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        #endregion
    }
}
