//-----------------------------------------------------------------------------
// <copyright file="CacheService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cache
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Cache Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class CacheService : ICacheService
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Add item to the cache.  This takes an expiration date for
        /// the item.
        /// </summary>
        ///---------------------------------------------------------------
        void ICacheService.Add<T>(
            String   key,
            T        item,
            Duration expiration)
        {
            Validation.ValidateParameter<String>  ("key"           , key       );
            Validation.ValidateParameter<T>       ("item"          , item      );
            Validation.ValidateParameter<Duration>("expirationDate", expiration);

            try
            {
                this.Add<T>(key, item, expiration);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Remove item from cache.
        /// </summary>
        ///---------------------------------------------------------------
        void ICacheService.Delete<T>(
            String key)
        {
            Validation.ValidateParameter<String>("key", key);

            try
            {
                this.Delete<T>(key);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Flush the entire cache.
        /// </summary>
        ///---------------------------------------------------------------
        void ICacheService.Flush()
        {
            try
            {
                this.Flush();
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Retrieve the object from the cache.
        /// </summary>
        ///---------------------------------------------------------------
        T ICacheService.Get<T>(
            String key)
        {
            Validation.ValidateParameter<String>("key", key);

            try
            {
                return this.Get<T>(key);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Update the item in the cache.
        /// </summary>
        ///---------------------------------------------------------------
        void ICacheService.Update<T>(
            String key,
            T      item)
        {
            Validation.ValidateParameter<String>("key" , key );
            Validation.ValidateParameter<T>     ("item", item);

            try
            {
                this.Update<T>(key, item);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
