//-----------------------------------------------------------------------------
// <copyright file="CacheService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cache
{
    using System;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ICacheService interface for the distributed 
    /// caching.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class CacheService : ICacheService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the cache client.
        /// </summary>
        ///---------------------------------------------------------------
        public CacheService(
            ICacheProvider cacheProvider)
        {
            Validation.ValidateParameter<ICacheProvider>("cacheProvider", cacheProvider);

            this.CacheProvider = cacheProvider;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the provider we will user for our cache.
        /// </summary>
        ///---------------------------------------------------------------
        private ICacheProvider CacheProvider { get; set; }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Add item to the cache.  This takes an expiration time
        /// </summary>
        ///---------------------------------------------------------------
        public void Add<T>(
            String   key,
            T        item,
            Duration expiration)
        {
            this.CacheProvider.Add<T>(key, item, expiration);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Remove item from cache.
        /// </summary>
        ///---------------------------------------------------------------
        public void Delete<T>(
            String key)
        {
            this.CacheProvider.Delete<T>(key);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Flush the entire cache pool.
        /// </summary>
        ///---------------------------------------------------------------
        public void Flush()
        {
            this.CacheProvider.Flush();
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Retrieve the object from the cache.
        /// </summary>
        ///---------------------------------------------------------------
        public T Get<T>(
            String key)
        {
            return this.CacheProvider.Get<T>(key);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Update the item in the cache.
        /// </summary>
        ///---------------------------------------------------------------
        public void Update<T>(
            String key,
            T      item)
        {
            this.CacheProvider.Update<T>(key, item);
        }
        #endregion
    }
}
