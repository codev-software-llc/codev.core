//-----------------------------------------------------------------------------
// <copyright file="DeserializeTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Base
{
    using System;
    using System.Text.Json;
    using Codev.Core.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NodaTime;
    using NodaTime.Serialization.SystemTextJson;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the test for password helper utility.
    /// </summary>
    ///------------------------------------------------------------------------
    [TestClass]
    public class DeserializeTests
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

            Byte[] interationArray = { 0, 0, 0, 0 };

            Buffer.BlockCopy(salt, 0, interationArray, 0, 4);

            Int32 iterations = BitConverter.ToInt32(interationArray);

            Assert.AreEqual(salt.Length, 36);
            Assert.AreEqual(iterations, 10);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with a iteration of ten.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void Deserialize_Nested()
        {
            String payload = "{\"statusCode\":200,\"errorMessage\":\"\",\"value\":{\"DestinationAddress\":\"chriswil @codev.net\",\"durationValue\":\"0:30:00\"}}";

            JsonSerializerOptions options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
           
            options.PropertyNameCaseInsensitive = true;

            WebApiResponse<TestStruct>? response = JsonSerializer.Deserialize<WebApiResponse<TestStruct>>(payload, options);

            Assert.IsTrue(true);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with a iteration of ten.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void Deserialize_Nested1()
        {
            String payload = "{\"destinationAddress\":\"chriswil @codev.net\",\"durationValue\":\"0:30:00\"}";

            JsonSerializerOptions options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            options.PropertyNameCaseInsensitive = true;

            TestStruct? response = JsonSerializer.Deserialize<TestStruct>(payload, options);

            Assert.IsTrue(true);
        }
        #endregion
    }
}