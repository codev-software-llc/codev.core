//-----------------------------------------------------------------------------
// <copyright file="DesCipherTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Cipher
{
    using Codev.Core.Interface;
    using Codev.Core.Provider.Cipher;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This impolements tests for the deserialization methods.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DesCipherTests
    {
        #region Setup
        ///---------------------------------------------------------------
        /// <summary>
        /// Setup the class.
        /// </summary>
        ///---------------------------------------------------------------
        [SetUp]
        public void Setup()
        {
        }
        #endregion

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with empty string.
        /// </summary>
        /// -------------------------------------------------------------------
        [Test]
        public void EncryptString_GuidHash()
        {
            String testString = "I am a TEST String";

            ICipherProvider provider = new DesCipherProvider(Guid.NewGuid());

            String encrypted = provider.EncryptString(testString);

            String decrypted = provider.DecryptString(encrypted);

            Assert.AreEqual(testString, decrypted);
        }
    }
}