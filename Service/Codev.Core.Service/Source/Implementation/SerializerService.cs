//-----------------------------------------------------------------------------
// <copyright file="SerializerService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Serializer
{
    using System;
    using System.Text.Json;
    using Codev.Core.Service.Serializer;
    using NodaTime;
    using NodaTime.Serialization.SystemTextJson;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ISerializerService interface for commmon Json
    /// interactions.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class SerializerService : ISerializerService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public SerializerService()
        {
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Serialize the object into a Json string.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Serialize<T>(
            T item)
        {
            JsonSerializerOptions options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            options.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Serialize<T>(item, options);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Deserialize the string into an object.
        /// </summary>
        ///--------------------------------------------------------------------
        public T Deserialize<T>(
            String serializedObject)
        {
            JsonSerializerOptions options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

            options.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Deserialize<T>(serializedObject, options);
        }
        #endregion
    }
}
