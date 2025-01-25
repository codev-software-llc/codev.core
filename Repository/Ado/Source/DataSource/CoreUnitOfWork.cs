//-----------------------------------------------------------------------------
// <copyright file="CoreUnitOfWork.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using Codev.Core.Base;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements a unit of work on a repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CoreUnitOfWork : UnitOfWork, ICoreUnitOfWork
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreUnitOfWork(
            ICoreDataSource dataSource) : base(dataSource)
        {
        }
        #endregion
    }
}
