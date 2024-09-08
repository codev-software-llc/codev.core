//-----------------------------------------------------------------------------
// <copyright file="NodaExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Globalization;
    using System.Text;
    using NodaTime;
    using NodaTime.Text;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This class contains extensions of the noda time framework.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class NodaExtensions
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the minimum date we use.
        /// </summary>
        ///--------------------------------------------------------------------
        private static readonly LocalDate LocalDateMin = new LocalDate(1900, 01, 01);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the maximum date we use.
        /// </summary>
        ///--------------------------------------------------------------------
        private static readonly LocalDate LocalDateMax = new LocalDate(4444, 12, 31);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the minimum date time we use.
        /// </summary>
        ///--------------------------------------------------------------------
        private static readonly LocalDateTime LocalDateTimeMin = new LocalDateTime(1900, 01, 01, 0, 0, 0);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the maximum date time we use.
        /// </summary>
        ///--------------------------------------------------------------------
        private static readonly LocalDateTime LocalDateTimeMax = new LocalDateTime(4444, 12, 31, 23, 59, 59);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the minimum date time we use.
        /// </summary>
        ///--------------------------------------------------------------------
        private static readonly Instant InstantMin = LocalDateTimeMin.ToInstant(NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault());

        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the maximum date time we use.
        /// </summary>
        ///--------------------------------------------------------------------
        private static readonly Instant InstantMax = LocalDateTimeMax.ToInstant(NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault());

        ///--------------------------------------------------------------------
        /// <summary>
        /// This constant represents the last day of February leap-year.
        /// </summary>
        ///--------------------------------------------------------------------
        private const Int32 LastDayInFebruaryLeapYear = 29;

        ///--------------------------------------------------------------------
        /// <summary>
        /// This contains the number of days in each month.
        /// </summary>
        ///--------------------------------------------------------------------
        private static readonly Int32[] EndOfMonthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the core minimum local date.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDate LocalDateMinValue
        {
            get
            {
                return LocalDateMin;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the core maximum local date.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDate LocalDateMaxValue
        {
            get
            {
                return LocalDateMax;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the core minimum local date time.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime LocalDateTimeMinValue
        {
            get
            {
                return LocalDateTimeMin;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the core maximum local date time.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime LocalDateTimeMaxValue
        {
            get
            {
                return LocalDateTimeMax;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return this core minumum instant.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Instant InstantMinValue
        {
            get
            {
                return InstantMin;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the core maximum local date time.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Instant InstantMaxValue
        {
            get
            {
                return InstantMax;
            }
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the bytes from an instant.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Byte[] ToBytes(
            this Instant instant)
        {
            String value = instant.ToString();

            return Encoding.UTF8.GetBytes(value);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the instant from the iso string.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Instant ToInstant(
            this String isoString)
        {
            return InstantPattern.General.Parse(isoString).Value;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the instant for the time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Instant ToInstant(
            this LocalDateTime localTime,
                 DateTimeZone  dtz)
        {
            Period milliseconds = Period.FromMilliseconds(localTime.Millisecond);

            return localTime.Minus(milliseconds).InZoneStrictly(dtz).ToInstant();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns a local date from a date time.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDate ToLocalDate(
            this DateTime dateTime)
        {
            return LocalDate.FromDateTime(dateTime);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the local date for the instant.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToLocalDateTime(
            this Instant      instant,
                 DateTimeZone dtz)
        {
            ZonedDateTime zdt = instant.InZone(dtz);

            return zdt.LocalDateTime;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the LocalDateTime for the start of the day.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToStartOfDay(
            this LocalDate localDate)
        {
            return new LocalDateTime(localDate.Year, localDate.Month, localDate.Day, 0, 0, 0, 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the LocalDateTime for the end of the day.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToEndOfDay(
            this LocalDate localDate)
        {
            return new LocalDateTime(localDate.Year, localDate.Month, localDate.Day, 23, 59, 59, 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the LocalDateTime for the start of the day.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToStartOfWeek(
            this LocalDate localDate)
        {
            LocalDateTime dateLocal = localDate.ToStartOfDay();

            Int32 dayOfWeek = (Int32)dateLocal.DayOfWeek % 7;

            Int32 offset = (Int32)DayOfWeek.Sunday - dayOfWeek;

            return dateLocal.PlusDays(offset);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns the End of the week for the given date.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToEndOfWeek(
            this LocalDate dateTime)
        {
            LocalDateTime dateLocal = dateTime.ToEndOfDay();

            Int32 dayOfWeek = (Int32)dateLocal.DayOfWeek % 7;

            Int32 offset = (Int32)DayOfWeek.Saturday - dayOfWeek;

            return dateLocal.PlusDays(offset);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns the start of the month for the given date.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToStartOfMonth(
            this LocalDate dateTime)
        {
            return new LocalDateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns the End of the month for the given date.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToEndOfMonth(
            this LocalDate dateTime)
        {
            Int32 dayEnd;

            if (DateTime.IsLeapYear(dateTime.Year) && (dateTime.Month == 2))
            {
                dayEnd = NodaExtensions.LastDayInFebruaryLeapYear;
            }
            else
            {
                dayEnd = NodaExtensions.EndOfMonthDays[dateTime.Month - 1];
            }

            return new LocalDateTime(dateTime.Year, dateTime.Month, dayEnd, 23, 59, 59, 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns the star of the year for the given date.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToStartOfYear(
            this LocalDate dateTime)
        {
            return new LocalDateTime(dateTime.Year, 1, 1, 0, 0, 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns the star of the year for the given date.
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDateTime ToEndOfYear(
            this LocalDate dateTime)
        {
            return new LocalDateTime(dateTime.Year, 12, 31, 23, 59, 59, 0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns a base display string.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String ToDisplayString(
            this LocalDateTime localDateTime)
        {
            return String.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", localDateTime.Year, localDateTime.Month, localDateTime.Day, localDateTime.Hour, localDateTime.Minute, localDateTime.Second);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns a base display string.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String ToDisplayString(
            this LocalDate localDate)
        {
            return String.Format("{0:0000}-{1:00}-{2:00}", localDate.Year, localDate.Month, localDate.Day);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns a base display string.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String ToDisplayString(
            this Duration duration)
        {
            return String.Format("{0:00}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns a duration from a string (00:00:00).
        /// </summary>
        ///--------------------------------------------------------------------
        public static Duration ToDuration(
            this String text)
        {
            DurationPattern pattern = DurationPattern.Create("HH:mm:ss", CultureInfo.InvariantCulture);

            ParseResult<Duration> result = pattern.Parse(text);

            return result.Success ? result.Value : Duration.FromHours(0);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns a duration from a string (00:00:00).
        /// </summary>
        ///--------------------------------------------------------------------
        public static LocalDate ToLocalDate(
            this String text)
        {
            LocalDatePattern pattern = LocalDatePattern.Create("yyyy-MM-dd", CultureInfo.InvariantCulture);

            ParseResult<LocalDate> result = pattern.Parse(text);

            return result.Success ? result.Value : new LocalDate(1900, 01, 01);
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Convert to duration to hours in the format of the type.  This can be
        /// either (Int32, Int64, Double, Single or Decimal).
        /// </summary>
        ///------------------------------------------------------------------------
        public static T AsTotalHours<T>(
            this Duration duration)
        {
            if (typeof(T) == typeof(Int32))
            {
                return (T)(Object)Convert.ToInt32(duration.TotalHours);
            }
            else if (typeof(T) == typeof(Int64))
            {
                return (T)(Object)Convert.ToInt64(duration.TotalHours);
            }
            else if (typeof(T) == typeof(Decimal))
            {
                return (T)(Object)Convert.ToDecimal(duration.TotalHours);
            }
            else if (typeof(T) == typeof(Single))
            {
                return (T)(Object)Convert.ToSingle(duration.TotalHours);
            }
            else if (typeof(T) == typeof(Double))
            {
                return (T)(Object)duration.TotalHours;
            }

            throw new InvalidCastException("Type is not a valid number");
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Convert to duration to minutes in the format of the type.  This can be
        /// either (Int32, Int64, Double, Single or Decimal).
        /// </summary>
        ///------------------------------------------------------------------------
        public static T AsTotalMinutes<T>(
            this Duration duration)
        {
            if (typeof(T) == typeof(Int32))
            {
                return (T)(Object)Convert.ToInt32(duration.TotalMinutes);
            }
            else if (typeof(T) == typeof(Int64))
            {
                return (T)(Object)Convert.ToInt64(duration.TotalMinutes);
            }
            else if (typeof(T) == typeof(Decimal))
            {
                return (T)(Object)Convert.ToDecimal(duration.TotalMinutes);
            }
            else if (typeof(T) == typeof(Single))
            {
                return (T)(Object)Convert.ToSingle(duration.TotalMinutes);
            }
            else if (typeof(T) == typeof(Double))
            {
                return (T)(Object)duration.TotalMinutes;
            }

            throw new InvalidCastException("Type is not a valid number");
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Convert to duration to seconds in the format of the type.  This can be
        /// either (Int32, Int64, Double, Single or Decimal).
        /// </summary>
        ///------------------------------------------------------------------------
        public static T AsTotalSeconds<T>(
            this Duration duration)
        {
            if (typeof(T) == typeof(Int32))
            {
                return (T)(Object)Convert.ToInt32(duration.TotalSeconds);
            }
            else if (typeof(T) == typeof(Int64))
            {
                return (T)(Object)Convert.ToInt64(duration.TotalSeconds);
            }
            else if (typeof(T) == typeof(Decimal))
            {
                return (T)(Object)Convert.ToDecimal(duration.TotalSeconds);
            }
            else if (typeof(T) == typeof(Single))
            {
                return (T)(Object)Convert.ToSingle(duration.TotalSeconds);
            }
            else if (typeof(T) == typeof(Double))
            {
                return (T)(Object)duration.TotalSeconds;
            }

            throw new InvalidCastException("Type is not a valid number");
        }
        #endregion
    }
}
