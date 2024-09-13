//-----------------------------------------------------------------------------
// <copyright file="SmtpProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IEMailClient interface for POP3 email processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SmtpProvider : IEmailClientProvider
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Header name for delivery intent.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String BounceRecoveryHeaderName = "CodevDeliveryIntent";

        ///--------------------------------------------------------------------
        /// <summary>
        /// Separator in the header.
        /// </summary>
        ///--------------------------------------------------------------------
        private const Char BounceRecoveryHeaderSeparator = '|';
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the server object.
        /// </summary>
        ///--------------------------------------------------------------------
        public SmtpProvider(
            String host,
            String userName,
            String userPassword,
            String useSSL,
            String useDefaultCredentials,
            String port,
            String fromAddress,
            String fromAddressName,
            String failingThreshold)
        {

            this.Host                  = Environment.ExpandEnvironmentVariables(host);
            this.UserName              = Environment.ExpandEnvironmentVariables(userName);
            this.UserPassword          = Environment.ExpandEnvironmentVariables(userPassword);
            this.UseSSL                = Convert.ToBoolean(useSSL);
            this.UseDefaultCredentials = Convert.ToBoolean(useDefaultCredentials);
            this.Port                  = Convert.ToInt32(port);
            this.SystemEmailAddress    = fromAddress;
            this.SystemEmailName       = fromAddressName;
            this.EmailFailingThreshold = Convert.ToInt32(failingThreshold);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Email settings for SMTP client.
        /// </summary>
        ///--------------------------------------------------------------------
        public  String  SystemEmailAddress    { get; private set; }
        public  String  SystemEmailName       { get; private set; }
        public  Int32   EmailFailingThreshold { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the host for the provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private String Host { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether this provider uses SSL.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean UseSSL { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether this provider uses default credentials.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean UseDefaultCredentials { get; set; }

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
            // Convert the SmtpMessage into the message format for the
            // SmtpClient.
            //
            MailAddress fromAddress = new MailAddress(this.SystemEmailAddress, this.SystemEmailName);
            MailAddress toAddress   = new MailAddress(message.To, message.ToName);

            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);

            if (!String.IsNullOrWhiteSpace(message.Bcc))
            {
                MailAddress bcc = new MailAddress(message.Bcc, message.BccName);
                mailMessage.Bcc.Add(bcc);
            }

            mailMessage.Subject    = message.Subject;
            mailMessage.Body       = message.Body;
            mailMessage.IsBodyHtml = true;

            foreach (MailHeader header in message.Headers)
            {
                mailMessage.Headers.Add(header.Name, header.Value);
            }

            // Send the email.
            //
            this.SendMailSynchronously(mailMessage);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Main routine for transmitting email asynchronously.
        /// </summary>
        ///--------------------------------------------------------------------
        private void SendMailSynchronously(
            MailMessage message)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host      = this.Host;
                smtp.EnableSsl = this.UseSSL;

                if (this.Port != 0)
                {
                    smtp.Port = this.Port;
                }

                smtp.DeliveryMethod        = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = this.UseDefaultCredentials;

                if (this.UseDefaultCredentials == false)
                {
                    smtp.Credentials = new NetworkCredential(this.UserName, this.UserPassword);
                }

                List<String> headers;

                String bounceHeader = message.Headers[BounceRecoveryHeaderName];

                if (String.IsNullOrWhiteSpace(bounceHeader) == false)
                {
                    String[] headerSplit = bounceHeader.Split(BounceRecoveryHeaderSeparator);

                    headers = new List<String>(headerSplit);
                }
                else
                {
                    headers = new List<String>();
                }
                
                String subject = message.Subject;
                String body    = message.Body;

                try
                {
                    var sendRecoveryToken = new Tuple<SmtpProvider, List<String>, String, String>(this, headers, subject, body);

                    smtp.Send(message);
                }
                catch (SmtpFailedRecipientsException sfre)
                {
                    throw new CoreProviderException(this.MapSmtpCodeToErrorCode(sfre.StatusCode), sfre);
                }
                catch (SmtpException se)
                {
                    throw new CoreProviderException(this.MapSmtpCodeToErrorCode(se.StatusCode), se);
                }
                catch (Exception e)
                {
                    throw new CoreProviderException(CoreErrorCode.InternalFailure, e);
                }
            }
        }

        private CoreErrorCode MapSmtpCodeToErrorCode(
            SmtpStatusCode code)
        {
            CoreErrorCode errorCode = CoreErrorCode.InternalFailure;

            switch (code)
            {
                case SmtpStatusCode.ClientNotPermitted:
                    errorCode = CoreErrorCode.AccessDenied;
                    break;

                case SmtpStatusCode.BadCommandSequence:
                    errorCode = CoreErrorCode.InvalidParameter;
                    break;

                case SmtpStatusCode.TransactionFailed:
                    errorCode = CoreErrorCode.InternalFailure;
                    break;
            }

            return errorCode;
        }
        #endregion
    }
}
