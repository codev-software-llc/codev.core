//-----------------------------------------------------------------------------
// <copyright file="ClockService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
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
        /// Defines the name we use to lookup the setting with.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String TimeHopHoursName = "ClockService_Duration";
        #endregion

        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the cache client.
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
        /// Return the current instant.
        /// </summary>
        ///--------------------------------------------------------------------
        public Instant GetCurrentInstant()
        {
            Instant instantNow = SystemClock.Instance.GetCurrentInstant();

            // Get the advances to apply to current instance.
            //
            Duration duration = this.Cache.Get<Duration>(TimeHopHoursName);

            if (duration != null)
            {
                instantNow = instantNow.Plus(duration);
            }

            return instantNow;


            //Instant instantNow = SystemClock.Instance.GetCurrentInstant();
            //
            //DateTimeZone dtz = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault();
            //
            //LocalDateTime ldt = instantNow.ToLocalDateTime(dtz);
            //
            //Duration milliseconds = Duration.FromMilliseconds(ldt.Millisecond);
            //
            //SettingEntity entity = this.SettingRepository.GetByName(TimeHopHoursName);
            //
            //if (entity != null)
            //{
            //    Int32 seconds = Convert.ToInt32(entity.Value);
            //
            //    Duration duration = Duration.FromSeconds(seconds);
            //
            //    instantNow = instantNow.Plus(duration);
            //}
            //else
            //{
            //    entity = new SettingEntity(instantNow)
            //        {
            //            Flags = SettingFlags.None,
            //            Name  = TimeHopHoursName,
            //            Value = "0"
            //        };
            //
            //    this.SettingRepository.Add(entity);
            //}
            //
            //return instantNow.Minus(milliseconds);
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
            Duration duration = this.Cache.Get<Duration>(TimeHopHoursName);

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
        /// Return the UTC time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        public DateTimeZone GetDefaultTimeZone()
        {
            return DateTimeZone.Utc;
        }

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
            Duration duration = this.Cache.Get<Duration>(TimeHopHoursName);

            if (duration != null)
            {
                duration = duration.Plus(timeAdvance);

                this.Cache.Set<Duration>(TimeHopHoursName, duration, DateTimeOffset.Now.AddDays(1));
            }
            else
            {
                this.Cache.Set<Duration>(TimeHopHoursName, timeAdvance, DateTimeOffset.Now.AddDays(1));
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Reset the clock.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Reset()
        {
            this.Cache.Set<Duration>(TimeHopHoursName, Duration.Zero);
        }
        #endregion
    }
}
