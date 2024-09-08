//-----------------------------------------------------------------------------
// <copyright file="ClockService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IClockService interface for clock manipulation.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ClockService : BaseService, IClockService
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Defines the name we use to lookup the setting with.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String TimeHopHoursName = "TimeHopHours";
        #endregion

        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the cache client.
        /// </summary>
        ///---------------------------------------------------------------
        public ClockService(
            ICoreUnitOfWork    unitOfWork,
            ISettingRepository settingRepository) : base(unitOfWork)
        {
            Validation.ValidateParameter<ISettingRepository>("settingRepository", settingRepository);

            this.SettingRepository = settingRepository;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the setting repository.
        /// </summary>
        ///---------------------------------------------------------------
        private ISettingRepository SettingRepository { get; set; }
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

            DateTimeZone dtz = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault();

            LocalDateTime ldt = instantNow.ToLocalDateTime(dtz);

            Duration milliseconds = Duration.FromMilliseconds(ldt.Millisecond);

            SettingEntity entity = this.SettingRepository.GetByName(TimeHopHoursName);

            if (entity != null)
            {
                Int32 seconds = Convert.ToInt32(entity.Value);

                Duration duration = Duration.FromSeconds(seconds);

                instantNow = instantNow.Plus(duration);
            }
            else
            {
                entity = new SettingEntity(instantNow)
                    {
                        Flags = SettingFlags.None,
                        Name  = TimeHopHoursName,
                        Value = "0"
                    };

                this.SettingRepository.Add(entity);
            }

            return instantNow.Minus(milliseconds);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the local time in the time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime GetLocalDateTime(
            DateTimeZone dtz)
        {
            Instant instantNow = SystemClock.Instance.GetCurrentInstant();

            SettingEntity entity = this.SettingRepository.GetByName(TimeHopHoursName);

            if (entity != null)
            {
                Int32 seconds = Convert.ToInt32(entity.Value);

                Duration duration = Duration.FromSeconds(seconds);

                instantNow = instantNow.Plus(duration);
            }
            else
            {
                entity = new SettingEntity(instantNow)
                    {
                        Flags = SettingFlags.None,
                        Name  = TimeHopHoursName,
                        Value = "0"
                    };

                this.SettingRepository.Add(entity);
            }

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
            return NodaTime.DateTimeZone.Utc;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Advance the clock by a duration.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Advance(
            Duration timeAdvance)
        {
            SettingEntity entity = this.SettingRepository.GetByName(TimeHopHoursName);

            if (entity != null)
            {
                Int32 currentSeconds = Convert.ToInt32(entity.Value);

                Duration current = Duration.FromSeconds(currentSeconds);

                current = current.Plus(timeAdvance);

                entity.Value = Convert.ToInt32(current.TotalSeconds).ToString();

                this.SettingRepository.Update(entity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Setting does not exist");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Reset the clock.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Reset()
        {
            SettingEntity entity = this.SettingRepository.GetByName(TimeHopHoursName);
            
            if (entity != null)
            {
                entity.Value = "0";
            
                this.SettingRepository.Update(entity);
            }
        }
        #endregion
    }
}
