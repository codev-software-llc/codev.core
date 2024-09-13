//-----------------------------------------------------------------------------
// <copyright file="ClockService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Clock
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;
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
        /// Advance the clock by a duration of time.
        /// </summary>
        ///--------------------------------------------------------------------
        void IClockService.Advance(
            Duration timeAdvance)
        {
            Validation.ValidateParameter<Duration>("timeAdvance", timeAdvance);

            try
            {
                this.Advance(timeAdvance);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the instant time (utc).
        /// </summary>
        ///--------------------------------------------------------------------
        Instant IClockService.GetCurrentInstant()
        {
            try
            {
                return this.GetCurrentInstant();
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
                return this.GetDefaultTimeZone();
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
            Validation.ValidateParameter<DateTimeZone>("dtz", dtz);

            try
            {
                return this.GetLocalDateTime(dtz);
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
                this.Reset();
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
