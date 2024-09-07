//-----------------------------------------------------------------------------
// <copyright file="CacheService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cache
{
    using System;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
    using Codev.Core.Repository.Ado;
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
            try
            {
                Validation.ValidateParameter<String>  ("key"           , key       );
                Validation.ValidateParameter<T>       ("item"          , item      );
                Validation.ValidateParameter<Duration>("expirationDate", expiration);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Add<T>(key, item, expiration);

                    work.Commit();
                }
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
            try
            {
                Validation.ValidateParameter<String>("key", key);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Delete<T>(key);

                    work.Commit();
                }
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
                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Flush();

                    work.Commit();
                }
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
            try
            {
                Validation.ValidateParameter<String>("key", key);

                T value = default(T);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    value = this.Get<T>(key);

                    work.Commit();
                }

                return value;
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
            try
            {
                Validation.ValidateParameter<String>("key" , key );
                Validation.ValidateParameter<T>     ("item", item);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Update<T>(key, item);

                    work.Commit();
                }
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
