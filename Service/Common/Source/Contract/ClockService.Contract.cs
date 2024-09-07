//-----------------------------------------------------------------------------
// <copyright file="ClockService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
    using Codev.Core.Repository.Ado;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Clock Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ClockService : IClockService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the instant time (utc).
        /// </summary>
        ///--------------------------------------------------------------------
        Instant IClockService.GetCurrentInstant()
        {
            try
            {
                Instant instant;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    instant = this.GetCurrentInstant();

                    work.Commit();
                }

                return instant;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
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
        /// Return the local time in the time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        LocalDateTime IClockService.GetLocalDateTime(
            DateTimeZone dtz)
        {
            try
            {
                Validation.ValidateParameter<DateTimeZone>("dtz", dtz);

                LocalDateTime localDateTime;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    localDateTime = this.GetLocalDateTime(dtz);

                    work.Commit();
                }

                return localDateTime;
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
        /// Return the UTC time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        DateTimeZone IClockService.GetDefaultTimeZone()
        {
            try
            {
                DateTimeZone dtz;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    dtz = this.GetDefaultTimeZone();

                    work.Commit();
                }

                return dtz;
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
        /// Advance the clock by a duration of time.
        /// </summary>
        ///--------------------------------------------------------------------
        void IClockService.Advance(
            Duration timeAdvance)
        {
            try
            {
                Validation.ValidateParameter<Duration>("timeAdvance", timeAdvance);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Advance(timeAdvance);

                    work.Commit();
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
        /// Reset the clock.
        /// </summary>
        ///--------------------------------------------------------------------
        void IClockService.Reset()
        {
            try
            {
                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.Reset();

                    work.Commit();
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
