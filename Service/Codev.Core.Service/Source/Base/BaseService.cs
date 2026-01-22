//-----------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Service.Clock;

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
            ICoreDataSource dataSource,
            IClockService   clockService)
        {
            this.DataSource   = dataSource;
            this.ClockService = clockService;;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        ///--------------------------------------------------------------------
        protected IClockService ClockService { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the data source for the service.
        /// </summary>
        ///--------------------------------------------------------------------
        protected IDataSource DataSource { get; private set; }
        #endregion
    }
}
