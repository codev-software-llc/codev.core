using System;
using Codev.Core.Common.Interface;
using Codev.Core.Provider;
using Codev.Core.Repository.Ado;
using Codev.Core.Service.Cipher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests
{
    [TestClass]
    public class CipherTests
    {
        [TestMethod]
        public void Test_Aes()
        {
            ICoreDataSource ds = new CoreDataSource("local", "Codev.Core", "CodevCoreUser", "C0d3vC0r3", "core", "core", 10, 30, 30, false, false);

            ICipherProvider provider = new AesCipherProvider("test");

            ICipherService service = new CipherService(ds, provider);

            String encrypted = service.EncryptString("I Am A String");

            String decrypted = service.DecryptString(encrypted);
        }

        [TestMethod]
        public void Test_Des()
        {
            ICoreDataSource ds = new CoreDataSource("local", "Codev.Core", "CodevCoreUser", "C0d3vC0r3", "core", "core", 10, 30, 30, false, false);

            ICipherProvider provider = new DesCipherProvider("test");

            ICipherService service = new CipherService(ds, provider);

            String encrypted = service.EncryptString("I Am A String");

            String decrypted = service.DecryptString(encrypted);
        }
    }
}