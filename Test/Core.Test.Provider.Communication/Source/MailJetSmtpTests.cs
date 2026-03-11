//-----------------------------------------------------------------------------
// <copyright file="MailJetSmtpTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Communication
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using Codev.Core.Provider.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Org.BouncyCastle.Bcpg;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains tests for the MailJet SMTP provider.
    /// </summary>
    ///------------------------------------------------------------------------
    [TestClass]
    public class MailJetSmtpTests
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with valid string with a string hash.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void Test_MailJet_Send()
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
                        Bcc      = "",
                        BccName  = "",
                        From     = "",
                        To       = "<some address>",
                        ToName   = "<some name>",
                        FromName = "test",
                        Body    = "this is a test body",
                        Subject = "this is a test subject"
                    };

                provider.SendAsync(message).Wait();
            }
            catch (CoreProviderException e)
            {
                Assert.AreEqual(CoreErrorCode.InternalFailure, e.ErrorCode);
            }
        }

        #endregion
    }
}