//-----------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    ///------------------------------------------------------------------------
    /// <summary>
    /// This is a base class for the service.
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
            IDataSource dataSource)
        {
            this.DataSource = dataSource;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the data source for the service.
        /// </summary>
        ///--------------------------------------------------------------------
        protected IDataSource DataSource { get; private set; }
        #endregion
    }
}
