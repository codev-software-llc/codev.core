//-----------------------------------------------------------------------------
// <copyright file="NoCacheProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider
{
    using System;
    using Codev.Core.Interface;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ICacheService interface for a default cache.  This
    /// is essentially a "No" cache implementation.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class NoCacheProvider : ICacheProvider
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the cache client.
        /// </summary>
        ///---------------------------------------------------------------
        public NoCacheProvider()
        {
        }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Add item to the cache.  This takes an expiration date for
        /// the item.
        /// </summary>
        ///---------------------------------------------------------------
        public void Add<T>(
            String   keyId,
            T        item,
            Duration expiration)
        {
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Remove item from cache.
        /// </summary>
        ///---------------------------------------------------------------
        public void Delete<T>(
            String keyId)
        {
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Flush the entire cache pool.
        /// </summary>
        ///---------------------------------------------------------------
        public void Flush()
        {
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Retrieve the object from the cache.
        /// </summary>
        ///---------------------------------------------------------------
        public T Get<T>(
            String keyId)
        {
            return default(T);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Update the item in the cache.
        /// </summary>
        ///---------------------------------------------------------------
        public void Update<T>(
            String keyId,
            T      item)
        {
        }
        #endregion
    }
}
