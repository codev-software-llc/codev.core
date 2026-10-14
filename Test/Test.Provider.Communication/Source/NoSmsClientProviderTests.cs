//-----------------------------------------------------------------------------
// <copyright file="NoSmsClientProviderTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Communication
{
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Codev.Core.Provider.Communication;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This impolements tests for the clock service Reset method.
    /// </summary>
    ///------------------------------------------------------------------------
    public class NoSmsClientProviderTests
    {
        #region Setup
        /// -------------------------------------------------------------------
        /// <summary>
        /// This will setup the test.
        /// </summary>
        /// -------------------------------------------------------------------
        [SetUp]
        public void Setup()
        {
            this.SmsClientProvider = new NoSmsProvider();
        }
        #endregion

        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the provider.
        /// </summary>
        /// -------------------------------------------------------------------
        private ISmsClientProvider SmsClientProvider { get; set; }
        #endregion

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test the reset of the clock service.
        /// </summary>
        /// -------------------------------------------------------------------
        [Test]
        public void SendAsync_Reset()
        {
            try
            {
                String? host     = Environment.GetEnvironmentVariable("%CORE_SMTP_HOST");
                String? port     = Environment.GetEnvironmentVariable("%CORE_SMTP_Port");
                String? username = Environment.GetEnvironmentVariable("%CORE_SMTP_USERNAME");
                String? password = Environment.GetEnvironmentVariable("%CORE_SMTP_PASSWORD");

                MailJetSmtpProvider provider = new MailJetSmtpProvider(
                    host,
                    port,
                    username,
                    password,
                    "test@test.net",
                    "Codev"
                   );

                SmtpMessage message = new SmtpMessage()
                {
                    Bcc = "",
                    BccName = "",
                    From = "",
                    To = "<some address>",
                    ToName = "<some name>",
                    FromName = "test",
                    Body = "this is a test body",
                    Subject = "this is a test subject"
                };

                provider.SendAsync(message).Wait();
            }
            catch (CoreProviderException e)
            {
                Assert.AreEqual(CoreErrorCode.InternalFailure, e.ErrorCode);
            }
        }
    }
}