//-----------------------------------------------------------------------------
// <copyright file="AdvanceTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Service.Clock
{
    using Codev.Core.Service.Clock;
    using Microsoft.Extensions.Caching.Memory;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This impolements tests for the clock service Advance method.
    /// </summary>
    ///------------------------------------------------------------------------
    public class AdvanceTests
    {
        #region Setup
        /// -------------------------------------------------------------------
        /// <summary>
        /// Setup the test.
        /// </summary>
        /// -------------------------------------------------------------------
        [SetUp]
        public void Setup()
        {
            MemoryCacheOptions options = new MemoryCacheOptions()
                {

                };

            IMemoryCache cache = new MemoryCache(options);

            this.ClockService = new ClockService(cache);
        }
        #endregion

        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        /// -------------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test the advance of one day.
        /// </summary>
        /// -------------------------------------------------------------------
        [Test]
        public void Advance_Day()
        {
            this.ClockService.Reset();

            Instant instant0 = this.ClockService.GetCurrentInstant();

            LocalDateTime ldt0 = this.ClockService.GetLocalDateTime(DateTimeZone.Utc);

            this.ClockService.Advance(Duration.FromDays(1));

            Instant instant1 = this.ClockService.GetCurrentInstant();

            LocalDateTime ldt1 = this.ClockService.GetLocalDateTime(DateTimeZone.Utc);

            Int32 x = ldt0.Date.CompareTo(ldt1.Date);

            Boolean isAdvanced = ldt1.Date.Day.Equals(ldt0.Date.Day + 1);

            Assert.IsTrue(isAdvanced);
        }
    }
}