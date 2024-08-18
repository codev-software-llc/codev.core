//-----------------------------------------------------------------------------
// <copyright file="IClockService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using Codev.Core.Common.Interface;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This service provides a common abstraction around the clock.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IClockService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the instant time (utc).
        /// </summary>
        ///--------------------------------------------------------------------
        Instant GetCurrentInstant();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the local time in the time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        LocalDateTime GetLocalDateTime(
            DateTimeZone dtz);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the UTC time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        DateTimeZone GetDefaultTimeZone();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the instant time (utc).
        /// </summary>
        ///--------------------------------------------------------------------
        void Advance(
            Duration duration);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Reset the clock.
        /// </summary>
        ///--------------------------------------------------------------------
        void Reset();
        #endregion
    }
}
