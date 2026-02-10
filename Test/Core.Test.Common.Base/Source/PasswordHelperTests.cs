//-----------------------------------------------------------------------------
// <copyright file="PasswordHelperTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Common.Cipher
{
    using System;
    using Codev.Core.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the test for password helper utility.
    /// </summary>
    ///------------------------------------------------------------------------
    [TestClass]
    public class PasswordHelperTests
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with a iteration of ten.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void PasswordHelper_GenerateRandomSalt_Interation10()
        {
            Byte[] salt = PasswordHelper.GenerateRandomSalt(10);

            Byte[] interationArray = [ 0, 0, 0, 0 ];

            Buffer.BlockCopy(salt, 0, interationArray, 0, 4);

            Int32 iterations = BitConverter.ToInt32(interationArray);

            Assert.HasCount(36, salt);
            Assert.AreEqual(10, iterations);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with a iteration of ten.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void PasswordHelper_GenerateSaltedPasswordHash_Interation10()
        {
            Byte[] salt = PasswordHelper.GenerateRandomSalt(10);

            Byte[] pw = PasswordHelper.GenerateSaltedPasswordHash("test", salt);

            Assert.HasCount(36, salt);
        }

        #endregion
    }
}