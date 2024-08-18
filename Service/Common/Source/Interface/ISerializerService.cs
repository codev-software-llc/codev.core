//-----------------------------------------------------------------------------
// <copyright file="ISerializerService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using Codev.Core.Common.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This service provids json serialization.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISerializerService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Serialize an object into Json.
        /// </summary>
        ///--------------------------------------------------------------------
        String Serialize<T>(
            T item);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Deserialize the string into a json object.
        /// </summary>
        ///--------------------------------------------------------------------
        T Deserialize<T>(
            String serializedString);
        #endregion
    }
}
