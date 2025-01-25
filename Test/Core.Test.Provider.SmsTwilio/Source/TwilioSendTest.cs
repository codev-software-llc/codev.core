//-----------------------------------------------------------------------------
// <copyright file="CipherAesTests.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Cipher
{
    using System;
    using Codev.Core.Model;
    using Codev.Core.Provider.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the test for Des provider.
    /// </summary>
    ///------------------------------------------------------------------------
    [TestClass]
    public class TwilioSendTests
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with empty string.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void Sms_Send()
        {
            String accountId = Environment.ExpandEnvironmentVariables("%TIMECOG_TWILIO_ACCOUNTID%");
            String authToken = Environment.ExpandEnvironmentVariables("%TIMECOG_TWILIO_AUTHTOKEN%");
            String serviceId = Environment.ExpandEnvironmentVariables("%TIMECOG_TWILIO_SERVICEID%");
            //String phoneFrom = Environment.ExpandEnvironmentVariables("%TIMECOG_TWILIO_PHONEFROM%");
            String phoneFrom = "+18886985043";

            TwilioSmsProvider provider = new TwilioSmsProvider(accountId, authToken, serviceId, phoneFrom);

            SmsMessage message = new SmsMessage()
                {
                    Body      = "Test Message from TimeCog\nConfirmationCode (1234)",
                    PhoneTo   = "+12088632633"
                };

            provider.Send(message);
        }
        #endregion
    }
}