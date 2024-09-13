//-----------------------------------------------------------------------------
// <copyright file="ClockService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Clock
{
    using System;
    using Codev.Core.Model;
    using Microsoft.Extensions.Caching.Memory;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IClockService interface for clock manipulation.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ClockService : IClockService
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Defines the name in the cache that stores our time-hop duration.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String CacheName = "ClockService_Duration";
        #endregion

        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the clock service.
        /// </summary>
        ///---------------------------------------------------------------
        public ClockService(
            IMemoryCache memoryCache)
        {
            Validation.ValidateParameter<IMemoryCache>("memoryCache", memoryCache);

            this.Cache = memoryCache;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the memory cache.
        /// </summary>
        ///---------------------------------------------------------------
        private IMemoryCache Cache { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Advance the clock by a duration.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Advance(
            Duration timeAdvance)
        {
            // Get the current advance setting.
            //
            Duration duration = this.Cache.Get<Duration>(CacheName);

            if (duration != null)
            {
                duration = duration.Plus(timeAdvance);

                this.Cache.Set<Duration>(CacheName, duration, DateTimeOffset.Now.AddDays(1));
            }
            else
            {
                this.Cache.Set<Duration>(CacheName, timeAdvance, DateTimeOffset.Now.AddDays(1));
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the current instant.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant GetCurrentInstant()
        {
            Instant instantNow = SystemClock.Instance.GetCurrentInstant();

            // Get the advances to apply to current instance.
            //
            Duration duration = this.Cache.Get<Duration>(CacheName);

            if (duration != null)
            {
                instantNow = instantNow.Plus(duration);
            }

            return instantNow;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the UTC time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        public DateTimeZone GetDefaultTimeZone()
        {
            return DateTimeZone.Utc;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the local time in the time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime GetLocalDateTime(
            DateTimeZone dtz)
        {
            // Get the current time.
            //
            Instant instantNow = SystemClock.Instance.GetCurrentInstant();

            // Get any advances that we need to apply to the current
            // time.
            //
            Duration duration = this.Cache.Get<Duration>(CacheName);

            if (duration != null)
            {
                instantNow = instantNow.Plus(duration);
            }

            // Return the time.
            //
            ZonedDateTime zdt = instantNow.InZone(dtz);

            return zdt.LocalDateTime;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Reset the clock.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Reset()
        {
            this.Cache.Set<Duration>(CacheName, Duration.Zero);
        }
        #endregion
    }
}
