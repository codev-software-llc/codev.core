//-----------------------------------------------------------------------------
// <copyright file="DiagnosticService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Diagnostic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains the explicit interface hooks that we will use to perform
    /// call validation on the parameters.  Each of these methods will in-turn
    /// call the actual implementations.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class DiagnosticService : IDiagnosticService
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Log information.
        /// </summary>
        ///---------------------------------------------------------------
        void IDiagnosticService.LogInformation(
                   Reference<Identity>     identityReference,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   String                  formatMessage,
            params Object[]                arguments)
        {
            Validation.ValidateParameter<String>("formatMessage", formatMessage);

            tag = Validation.ValidateDefault<String>("tag", tag, String.Empty);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    this.LogInformation(identityEntity, componentType, tag, formatMessage, arguments);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
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
        /// Log an error.
        /// </summary>
        ///---------------------------------------------------------------
        void IDiagnosticService.LogError(
                   Reference<Identity>     identityReference,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   String                  formatMessage,
            params Object[]               arguments)
        {
            Validation.ValidateParameter<String>("formatMessage", formatMessage);

            tag = Validation.ValidateDefault<String>("tag", tag, String.Empty);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    this.LogError(identityEntity, componentType, tag, formatMessage, arguments);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
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
        /// Log an exception.
        /// </summary>
        ///---------------------------------------------------------------
        void IDiagnosticService.LogException(
                   Reference<Identity>     identityReference,
                   DiagnosticComponentType componentType,
                   String                  tag,
                   Exception               exception)
        {
            Validation.ValidateParameter<Exception>("exception", exception);

            identityReference = Validation.ValidateDefault<Reference<Identity>>("identityReference", identityReference, null);

            tag = Validation.ValidateDefault<String>("tag", tag, String.Empty);

            try
            {
                IdentityEntity identityEntity;

                if (identityReference != null)
                {
                    identityEntity = this.IdentityRepository.GetById(identityReference.Id);
                }
                else
                {
                    identityEntity = this.IdentityRepository.GetById(identityReference.Id);
                }

                if (identityEntity != null)
                {
                    this.LogException(identityEntity, componentType, tag, exception);
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

        ///--------------------------------------------------------------------
        /// <summary>
        /// Begin a profile session.
        /// </summary>
        ///--------------------------------------------------------------------
        void IDiagnosticService.BeginProfiling()
        {
            try
            {
                this.BeginProfiling();
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

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will end the profiling session.
        /// </summary>
        ///--------------------------------------------------------------------
        void IDiagnosticService.EndProfiling()
        {
            try
            {
                this.EndProfiling();
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

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will render the profile results to the UI.
        /// </summary>
        ///--------------------------------------------------------------------
        String IDiagnosticService.RenderProfileResults()
        {
            try
            {
                return this.RenderProfileResults();
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

        ///--------------------------------------------------------------------
        /// <summary>
        /// Profile a connection request.
        /// </summary>
        ///--------------------------------------------------------------------
        IDbConnection IDiagnosticService.ProfileConnection(
            String connectionString)
        {
            try
            {
                Validation.ValidateParameter<String>("connectionString", connectionString);

                return this.ProfileConnection(connectionString);

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

        ///--------------------------------------------------------------------
        /// <summary>
        /// Profile a reader request.
        /// </summary>
        ///--------------------------------------------------------------------
        DbDataReader IDiagnosticService.ProfileReader(
            IDbConnection connection,
            DbDataReader  reader)
        {
            Validation.ValidateParameter<IDbConnection>("connection", connection);
            Validation.ValidateParameter<DbDataReader> ("reader"    , reader    );

            try
            {
                return this.ProfileReader(connection, reader);
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
        /// Retreieve the errorlog entries by date range.
        /// </summary>
        ///---------------------------------------------------------------
        List<ErrorLog> IDiagnosticService.GetErrorsByDateRange(
            Reference<Identity> identityReference,
            Nullable<LocalDate> dateEnd,
            Nullable<LocalDate> dateStart)
        {
            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    return this.GetErrorsByDateRange(identityEntity, dateStart, dateEnd);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
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
        /// Retreieve the statistics.
        /// </summary>
        ///---------------------------------------------------------------
        List<ErrorLog> IDiagnosticService.GetStatistics(
            Reference<Identity> identityReference,
            Nullable<LocalDate> dateEnd,
            Nullable<LocalDate> dateStart)
        {
            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    return this.GetStatistics(identityEntity, dateStart, dateEnd);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
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
