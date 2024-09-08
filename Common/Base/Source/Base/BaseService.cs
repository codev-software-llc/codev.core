//-----------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    ///------------------------------------------------------------------------
    /// <summary>
    /// This is a base class for all services.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BaseService
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the access.
        /// </summary>
        ///--------------------------------------------------------------------
        protected BaseService(
            IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the unit of work wrapper around the datasource.
        /// </summary>
        ///--------------------------------------------------------------------
        protected IUnitOfWork UnitOfWork { get; private set; }
        #endregion
    }
}
