//-----------------------------------------------------------------------------
// <copyright file="ICacheProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the supported calls through the cache
    /// component.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ICacheProvider : IProvider
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Add item to the cache.  This takes an expiration date for
        /// the item.
        /// </summary>
        ///---------------------------------------------------------------
        void Add<T>(
            String   keyId,
            T        item,
            Duration duration);

        ///---------------------------------------------------------------
        /// <summary>
        /// Remove item from cache.
        /// </summary>
        ///---------------------------------------------------------------
        void Delete<T>(
            String keyId);

        ///---------------------------------------------------------------
        /// <summary>
        /// Flush the entire cache.
        /// </summary>
        ///---------------------------------------------------------------
        void Flush();

        ///---------------------------------------------------------------
        /// <summary>
        /// Retrieve the object from the cache.
        /// </summary>
        ///---------------------------------------------------------------
        T Get<T>(
            String keyId);

        ///---------------------------------------------------------------
        /// <summary>
        /// Update the item in the cache.
        /// </summary>
        ///---------------------------------------------------------------
        void Update<T>(
            String keyId,
            T      item);
        #endregion
    }
}
