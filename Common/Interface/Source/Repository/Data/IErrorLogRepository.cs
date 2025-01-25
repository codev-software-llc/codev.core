//-----------------------------------------------------------------------------
// <copyright file="IErrorLogRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface provides the means to log error diagnostics.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IErrorLogRepository : IRepository<ErrorLogEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return errors that are within a date range.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<ErrorLogEntity> GetAllByDateRange(
            IdentityEntity      identity,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the count of errors that are the same.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<ErrorLogEntity> GetStatistics(
            IdentityEntity      identity,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Purge all entries in the error log table.
        /// </summary>
        ///--------------------------------------------------------------------
        void PurgeAll();
        #endregion
    }
}