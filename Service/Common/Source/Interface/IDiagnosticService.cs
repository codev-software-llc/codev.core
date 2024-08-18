//-----------------------------------------------------------------------------
// <copyright file="IDiagnosticService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the diagnostics support.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IDiagnosticService : IService
    {
        #region Methods       
        ///--------------------------------------------------------------------
        /// <summary>
        /// Log information.
        /// </summary>
        ///--------------------------------------------------------------------
        void LogInformation(
                   Reference<Identity>     identityReference,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   String                  formatMessage,
            params Object[]                arguments);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Log an error.
        /// </summary>
        ///--------------------------------------------------------------------
        void LogError(
                   Reference<Identity>     identityReference,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   String                  formatMessage,
            params Object[]                arguments);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Log an exception.
        /// </summary>
        ///--------------------------------------------------------------------
        void LogException(
                   Reference<Identity>     identityReference,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   Exception               exception);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Begin a profile session.
        /// </summary>
        ///--------------------------------------------------------------------
        void BeginProfiling();

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will end the profiling session.
        /// </summary>
        ///--------------------------------------------------------------------
        void EndProfiling();

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will render the profile results to the UI.
        /// </summary>
        ///--------------------------------------------------------------------
        String RenderProfileResults();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Profile a connection request.
        /// </summary>
        ///--------------------------------------------------------------------
        IDbConnection ProfileConnection(
            String connectionString);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Profile a reader request.
        /// </summary>
        ///--------------------------------------------------------------------
        DbDataReader ProfileReader(
            IDbConnection connection,
            DbDataReader  reader);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve errors by a date-range.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ErrorLog> GetErrorsByDateRange(
            Reference<Identity> identityReference,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the error breakdown.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ErrorLog> GetStatistics(
            Reference<Identity> identityReference,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Purge all errors in the diagnostic service.
        /// </summary>
        ///--------------------------------------------------------------------
        void PurgeErrors();
        #endregion
    }
}
