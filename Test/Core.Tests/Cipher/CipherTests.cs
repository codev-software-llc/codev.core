//-----------------------------------------------------------------------------
// <copyright file="CipherTests.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Tests
{
    using System;
    using Codev.Core.Interface;
    using Codev.Core.Provider.Cipher;
    using Codev.Core.Service.Cipher;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This tests the cipher service.
    /// </summary>
    ///------------------------------------------------------------------------
    [TestClass]
    public class CipherTests
    {
        [TestMethod]
        public void Test_Aes()
        {
            ICipherProvider provider = new AesCipherProvider("test");

            ICipherService service = new CipherService(provider);

            String encrypted = service.EncryptString("I Am A String");

            String decrypted = service.DecryptString(encrypted);
        }

        [TestMethod]
        public void Test_Des()
        {
            ICipherProvider provider = new DesCipherProvider("test");

            ICipherService service = new CipherService(provider);

            String encrypted = service.EncryptString("I Am A String");

            String decrypted = service.DecryptString(encrypted);
        }
    }
}