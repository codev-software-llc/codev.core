//-----------------------------------------------------------------------------
// <copyright file="TwilioSmsProvider.cs" company="Codev Software, LLC">
// Copyright © 2025
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
    /// This implements the ISmsClientProvider interface for SMS processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class TwilioSmsProvider : ISmsClientProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the server object.
        /// </summary>
        ///--------------------------------------------------------------------
        public TwilioSmsProvider(
            String accountId,
            String authToken,
            String serviceId,
            String phoneFrom)
        {
            this.AccountSid = Environment.ExpandEnvironmentVariables(accountId);
            this.AuthToken  = Environment.ExpandEnvironmentVariables(authToken);
            this.ServiceId  = Environment.ExpandEnvironmentVariables(serviceId);
            this.PhoneFrom  = Environment.ExpandEnvironmentVariables(phoneFrom);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the Message service identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ServiceId { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the Account identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String AccountSid { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the authentication token.
        /// </summary>
        ///--------------------------------------------------------------------
        public String AuthToken { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the the phone which the SMS is from.
        /// </summary>
        ///--------------------------------------------------------------------
        public String PhoneFrom { get; private set; }
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
            TwilioClient.Init(this.AccountSid, this.AuthToken);

            PhoneNumber phoneTo = new PhoneNumber(message.PhoneTo);

            CreateMessageOptions options = new CreateMessageOptions(phoneTo)
                {
                    From                = this.PhoneFrom,
                    MessagingServiceSid = this.ServiceId,
                    Body                = message.Body
                };

            MessageResource smsMessage = MessageResource.Create(options);
        }
        #endregion
    }
}
