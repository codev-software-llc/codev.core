//-----------------------------------------------------------------------------
// <copyright file="DiagnosticService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IDiagnosticService for logging and profiling.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class DiagnosticService : BaseService, IDiagnosticService
    {
        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the service.
        /// </summary>
        /// -------------------------------------------------------------------
        public DiagnosticService(
            ICoreUnitOfWork     unitOfWork,
            IIdentityRepository identityRepository,
            IErrorLogRepository errorLogRepository,
            IClockService       clockService) : base(unitOfWork)
        {
            Validation.ValidateParameter<IIdentityRepository>("identityRepository", identityRepository);
            Validation.ValidateParameter<IErrorLogRepository>("errorLogRepository", errorLogRepository);
            Validation.ValidateParameter<IClockService>      ("clockService"      , clockService      );

            this.IdentityRepository = identityRepository;
            this.ErrorLogRepository = errorLogRepository;
            this.ClockService       = clockService;
        }
        #endregion

        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for associating an other to the entry.
        /// </summary>
        /// -------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing the logged errors.
        /// </summary>
        /// -------------------------------------------------------------------
        private IErrorLogRepository ErrorLogRepository { get; set; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        /// -------------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Log information.
        /// </summary>
        /// -------------------------------------------------------------------
        public void LogInformation(
                   IdentityEntity          identityEntity,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   String                  formatMessage,
            params Object[]                arguments)
        {
            this.WriteLog(identityEntity, componentType, tag, DiagnosticSeverityType.Information, formatMessage, arguments);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Log an error.
        /// </summary>
        /// -------------------------------------------------------------------
        public void LogError(
                   IdentityEntity          identityEntity,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   String                  formatMessage,
            params Object[]                arguments)
        {
            this.WriteLog(identityEntity, componentType, tag, DiagnosticSeverityType.Error, formatMessage, arguments);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Log an exception.
        /// </summary>
        /// -------------------------------------------------------------------
        public void LogException(
            IdentityEntity          identityEntity,
            DiagnosticComponentType componentType,
            String                  tag,
            Exception               exception)
        {
            this.WriteLog(identityEntity, componentType, tag, DiagnosticSeverityType.Exception, exception);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Begin a profile session.
        /// </summary>
        ///--------------------------------------------------------------------
        public void BeginProfiling()
        {
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will end the profiling session.
        /// </summary>
        ///--------------------------------------------------------------------
        public void EndProfiling()
        {
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will render the profile results to the UI.
        /// </summary>
        ///--------------------------------------------------------------------
        public String RenderProfileResults()
        {
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Profile a connection request.
        /// </summary>
        ///--------------------------------------------------------------------
        public IDbConnection ProfileConnection(
            String connectionString)
        {
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Profile a reader request.
        /// </summary>
        ///--------------------------------------------------------------------
        public DbDataReader ProfileReader(
            IDbConnection connection,
            DbDataReader  reader)
        {
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve errors by a date-range.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ErrorLog> GetErrorsByDateRange(
            IdentityEntity      identityEntity,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd)
        {
            EntityCollection<ErrorLogEntity> entities = this.ErrorLogRepository.GetAllByDateRange(identityEntity, dateStart, dateEnd);

            return entities.Select(x => x.ToModel()).ToList();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve statistics breakdown of errors.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ErrorLog> GetStatistics(
            IdentityEntity      identityEntity,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd)
        {
            EntityCollection<ErrorLogEntity> entities = this.ErrorLogRepository.GetStatistics(identityEntity, dateStart, dateEnd);

            return entities.Select(x => x.ToModel()).ToList();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Purge all the diagnostics errors.
        /// </summary>
        ///--------------------------------------------------------------------
        public void PurgeErrors()
        {
            this.ErrorLogRepository.PurgeAll();
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Write to the error repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private void WriteLog(
                   IdentityEntity          identityEntity,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   DiagnosticSeverityType  severityType,
                   String                  formatMessage,
            params Object[]                arguments)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            ErrorLogEntity logEntry = new ErrorLogEntity(instantNow)
                {
                    Identity      = identityEntity,
                    Flags         = ErrorLogFlags.None,
                    ComponentType = componentType,
                    SeverityType  = severityType,
                    Message       = String.Format(formatMessage, arguments),
                    TrackingTag   = tag,
                    ServerName    = String.Empty,
                    StackTrace    = String.Empty
                };
            
            this.ErrorLogRepository.Add(logEntry);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Write to the error repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private void WriteLog(
                   IdentityEntity          identityEntity,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   DiagnosticSeverityType  severityType,
                   Exception               exception)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            ErrorLogEntity logEntry = new ErrorLogEntity(instantNow)
                {
                    Identity      = identityEntity,
                    Flags         = ErrorLogFlags.None,
                    ComponentType = componentType,
                    SeverityType  = severityType,
                    Message       = exception.Message,
                    TrackingTag   = tag,
                    ServerName    = String.Empty,
                    StackTrace    = exception.StackTrace
                };
            
            this.ErrorLogRepository.Add(logEntry);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return a stack-trace from an exception location.  The type should
        /// be from the class type where the exception occurred.
        /// </summary>
        ///--------------------------------------------------------------------
        /*
        private String GetStackFrame<T>()
        {
            StackTrace stackTrace = new StackTrace();
        
            StackFrame[] frames = stackTrace.GetFrames();
        
            StringBuilder sb = new StringBuilder();
        
            for (Int32 idx = 1; idx < 4; idx++)
            {
                sb.AppendLine(frames[idx].GetMethod().Name);
            }
        
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            MethodInfo methodInfo = typeof(T).GetMethod(methodBase.Name, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        
            return String.Format("{0}.{1}:\n{2}", methodInfo.DeclaringType.FullName, methodBase.Name, sb.ToString());
        }
        */
        #endregion
    }
}
