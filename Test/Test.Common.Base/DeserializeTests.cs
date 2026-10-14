//-----------------------------------------------------------------------------
// <copyright file="PasswordHelperTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Common.Base
{
    using System.Text.Json;
    using Codev.Core.Base;
    using Microsoft.Extensions.DependencyModel;
    using NodaTime;
    using NodaTime.Serialization.SystemTextJson;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This impolements tests for the deserialization methods.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DeserializeTests
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

        ///---------------------------------------------------------------
        /// <summary>
        /// Test hash uniqueness.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void DeserializedNested()
        {
            String payload = "{\"statusCode\":200,\"errorMessage\":\"\",\"value\":{\"DestinationAddress\":\"chriswil @codev.net\",\"durationValue\":\"0:30:00\"}}";

            JsonSerializerOptions options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            options.PropertyNameCaseInsensitive = true;

            WebApiResponse<TestStruct>? response = JsonSerializer.Deserialize<WebApiResponse<TestStruct>>(payload, options);

        }


        ///---------------------------------------------------------------
        /// <summary>
        /// Test hash uniqueness.
        /// </summary>
        ///---------------------------------------------------------------
        [Test]
        public void DeserializedNested1()
        {
            String payload = "{\"destinationAddress\":\"chriswil @codev.net\",\"durationValue\":\"0:30:00\"}";

            JsonSerializerOptions options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            options.PropertyNameCaseInsensitive = true;

            TestStruct? response = JsonSerializer.Deserialize<TestStruct>(payload, options);

        }

    }
}