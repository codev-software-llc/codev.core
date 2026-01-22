//-----------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the generic repository interface.  This supports the
    /// general CRUD operations.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IRepository<T>
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the data source for the repository.
        /// </summary>
        ///--------------------------------------------------------------------
        IDataSource DataSource { get; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add entity to the repository.
        /// </summary>
        ///--------------------------------------------------------------------
        void Add(
            T entity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will mark entities as deleted.  This is accomplished by 
        /// setting the IsActive bit to zero.
        /// </summary>
        ///--------------------------------------------------------------------
        void Delete(
            T entity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will permanently delete an entity from the repository.
        /// </summary>
        ///--------------------------------------------------------------------
        void Purge(
            T entity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        void Update(
            T entity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate the object by the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        T GetById(
            Int32 entityId);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all entities of the repository type.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<T> GetAll();
        #endregion
    }
}
