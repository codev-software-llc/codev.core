//-----------------------------------------------------------------------------
// <copyright file="ValidateRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract validator for the base repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ValidateRepository<T>
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Validate the repository method.
        /// </summary>
        ///---------------------------------------------------------------
        public static void Add(
            T entity)
        {
            if (entity == null)
            {
                throw new CoreDataException(CoreErrorCode.InvalidParameter, new ArgumentNullException("entity"));
            }

            BaseEntity baseEntity = entity as BaseEntity;

            if (baseEntity.Id != 0)
            {
                throw new CoreDataException(CoreErrorCode.InvalidParameter, new ArgumentException("entity.Id"));
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Validate the repository method.
        /// </summary>
        ///---------------------------------------------------------------
        public static void Delete(
            T entity)
        {
            if (entity == null)
            {
                throw new CoreDataException(CoreErrorCode.InvalidParameter, new ArgumentNullException("entity"));
            }

            BaseEntity baseEntity = entity as BaseEntity;

            if (baseEntity.Id == 0)
            {
                throw new CoreDataException(CoreErrorCode.InvalidParameter, new ArgumentException("entity.Id"));
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Validate the repository method.
        /// </summary>
        ///---------------------------------------------------------------
        public static void GetAll()
        {
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Validate the repository method.
        /// </summary>
        ///---------------------------------------------------------------
        public static void GetById(
            Int32 entityId)
        {
            if (entityId == 0)
            {
                throw new CoreDataException(CoreErrorCode.InvalidParameter, new ArgumentException("entityId"));
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Validate the repository method.
        /// </summary>
        ///---------------------------------------------------------------
        public static void Purge(
            T entity)
        {
            if (entity == null)
            {
                throw new CoreDataException(CoreErrorCode.InvalidParameter, new ArgumentNullException("entity"));
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Validate the repository method.
        /// </summary>
        ///---------------------------------------------------------------
        public static void Update(
            T entity)
        {
            if (entity == null)
            {
                throw new CoreDataException(CoreErrorCode.InvalidParameter, new ArgumentNullException("entity"));
            }
        }
        #endregion
    }
}
