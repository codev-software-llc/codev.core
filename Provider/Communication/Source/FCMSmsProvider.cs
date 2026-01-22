//-----------------------------------------------------------------------------
// <copyright file="FCMSmsProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Communication
{
    using System;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Types;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ISmsClientProvider interface for SMS processing
    /// on Firebase Cloud Messaging (FCM).
    /// </summary>
    ///------------------------------------------------------------------------
    public class FCMSmsProvider : ISmsClientProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the provider.
        /// </summary>
        ///--------------------------------------------------------------------
        public FCMSmsProvider(
            String projectId,
            String projectNumber,
            String accessToken)
        {
            this.ProjectId     = Environment.ExpandEnvironmentVariables(projectId);
            this.ProjectNumber = Environment.ExpandEnvironmentVariables(projectNumber);
            this.AccessToken   = Environment.ExpandEnvironmentVariables(accessToken);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the Project number.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ProjectNumber { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the Project identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ProjectId { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the Access Token.
        /// </summary>
        ///--------------------------------------------------------------------
        public String AccessToken { get; private set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Send the SMS.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Send(
            SmsMessage message)
        {
        /*
            TwilioClient.Init(this.AccountSid, this.AuthToken);

            PhoneNumber phoneTo = new PhoneNumber(message.PhoneTo);

            CreateMessageOptions options = new CreateMessageOptions(phoneTo)
                {
                    From                = this.PhoneFrom,
                    MessagingServiceSid = this.ServiceId,
                    Body                = message.Body
                };

            MessageResource smsMessage = MessageResource.Create(options);
            */
        }
        #endregion
    }
}
