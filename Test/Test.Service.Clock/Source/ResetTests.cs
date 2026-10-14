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
    /// This impolements tests for the clock service Reset method.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ResetTests
    {
        #region Setup
        /// -------------------------------------------------------------------
        /// <summary>
        /// This will setup the test.
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
        /// Test the reset of the clock service.
        /// </summary>
        /// -------------------------------------------------------------------
        [Test]
        public void Clock_Reset()
        {
            this.ClockService.Reset();

            Instant instant0 = this.ClockService.GetCurrentInstant();

            LocalDateTime ldt0 = this.ClockService.GetLocalDateTime(DateTimeZone.Utc);

            this.ClockService.Advance(Duration.FromDays(1));

            this.ClockService.Reset();

            Instant instant1 = this.ClockService.GetCurrentInstant();

            LocalDateTime ldt1 = this.ClockService.GetLocalDateTime(DateTimeZone.Utc);

            Int32 x = ldt0.Date.CompareTo(ldt1.Date);

            Boolean isAdvanced = ldt0.Date.CompareTo(ldt1.Date).Equals(0);

            Assert.IsTrue(isAdvanced);
        }
    }
}