//-----------------------------------------------------------------------------
// <copyright file="CipherAesTests.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Cipher
{
    using System;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Provider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the test for Aes provider.
    /// </summary>
    ///------------------------------------------------------------------------
    [TestClass]
    public class CipherAesTests
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with empty string.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void EncryptString_GuidHash()
        {
            String testString = "I am a TEST String";

            ICipherProvider provider = new AesCipherProvider(Guid.NewGuid());

            String encrypted = provider.EncryptString(testString);

            String decrypted = provider.DecryptString(encrypted);

            Assert.AreEqual(testString, decrypted);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with valid string with a string hash.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void EncrytString_StringHash()
        {
            String testString = "I am a TEST String";
            String testHash   = "AbCdE";

            ICipherProvider provider = new AesCipherProvider(testHash);

            String encrypted = provider.EncryptString(testString);

            String decrypted = provider.DecryptString(encrypted);

            Assert.AreEqual(testString, decrypted);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with valid string with a string hash.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void Test_Aes_NullHash()
        {
            try
            {
                ICipherProvider provider = new AesCipherProvider(null);
            }
            catch (CoreProviderException e)
            {
                Assert.AreEqual(e.ErrorCode, CoreErrorCode.InternalFailure);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with valid string with a string hash.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void EncrytString_StringEmptyHash()
        {
            String testString = "I am a TEST String";

            ICipherProvider provider = new AesCipherProvider(String.Empty);

            String encrypted = provider.EncryptString(testString);

            String decrypted = provider.DecryptString(encrypted);

            Assert.AreEqual(testString, decrypted);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with string.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void EncryptString_CredentialHash()
        {
            String testString = "I am a TEST String";
            String userName = "TestUser";
            String password = "TestPass";

            ICipherProvider provider = new AesCipherProvider(userName, password);

            String encrypted = provider.EncryptString(testString);

            String decrypted = provider.DecryptString(encrypted);

            Assert.AreEqual(testString, decrypted);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with string.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void EncryptString_CredentialNullHash()
        {
            String testString = "I am a TEST String";

            ICipherProvider provider = new AesCipherProvider(null, null);

            String encrypted = provider.EncryptString(testString);

            String decrypted = provider.DecryptString(encrypted);

            Assert.AreEqual(testString, decrypted);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with valid string with a string hash.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void EncryptString_EmptyString()
        {
            String testHash = "AbCdE";

            ICipherProvider provider = new AesCipherProvider(testHash);

            String encrypted = provider.EncryptString(String.Empty);

            String decrypted = provider.DecryptString(encrypted);

            Assert.AreEqual(String.Empty, decrypted);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with valid string with a string hash.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void EncryptString_NullString()
        {
            String testHash = "AbCdE";

            ICipherProvider provider = new AesCipherProvider(testHash);

            try
            {
                String encrypted = provider.EncryptString(null);
            }
            catch (CoreProviderException e)
            {
                Assert.AreEqual(e.ErrorCode, CoreErrorCode.InternalFailure);
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with valid string with a string hash.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void DecryptString_NullString()
        {
            String testHash = "AbCdE";

            ICipherProvider provider = new AesCipherProvider(testHash);

            try
            {
                String encrypted = provider.DecryptString(null);
            }
            catch (CoreProviderException e)
            {
                Assert.AreEqual(e.ErrorCode, CoreErrorCode.InternalFailure);
            }
        }
        #endregion
    }
}