namespace Core.Test.Service.Clock
{
    using Codev.Core.Service.Clock;
    using Microsoft.Extensions.Caching.Memory;
    using NodaTime;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            MemoryCacheOptions options = new MemoryCacheOptions()
            {

            };

            IMemoryCache cache = new MemoryCache(options);

            IClockService clockService = new ClockService(cache);


            Instant instant = clockService.GetCurrentInstant();

            clockService.Advance(Duration.FromDays(1));

            instant = clockService.GetCurrentInstant();

            LocalDateTime ldt = clockService.GetLocalDateTime(DateTimeZone.Utc);

            clockService.Reset();


            Assert.Pass();
        }
    }
}