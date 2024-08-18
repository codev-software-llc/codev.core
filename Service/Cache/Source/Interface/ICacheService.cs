//-----------------------------------------------------------------------------
// <copyright file="ICacheService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cache
{
    using System;
    using Codev.Core.Common.Interface;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This service provides for caching of data.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ICacheService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add item to the cache.  This takes an expiration date for
        /// the item.
        /// </summary>
        ///--------------------------------------------------------------------
        void Add<T>(
            String   key,
            T        item,
            Duration expiration);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove item from cache.
        /// </summary>
        ///--------------------------------------------------------------------
        void Delete<T>(
            String key);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Flush the entire cache.
        /// </summary>
        ///--------------------------------------------------------------------
        void Flush();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the object from the cache.
        /// </summary>
        ///--------------------------------------------------------------------
        T Get<T>(
            String key);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the item in the cache.
        /// </summary>
        ///--------------------------------------------------------------------
        void Update<T>(
            String key,
            T      item);
        #endregion
    }
}
