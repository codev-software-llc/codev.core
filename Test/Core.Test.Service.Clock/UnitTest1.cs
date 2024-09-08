namespace Core.Test.Service.Clock
{
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Repository.Ado;
    using Codev.Core.Service.Common;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ICoreDataSource dataSource = new CoreDataSource(
                "localhost,1411",
                "Codev.TimeCog.Local",
                "timecoguser",
                "Tim3in@B0tt13",
                "core",
                "Core",
                10,
                30,
                30, false, false);

            ICoreUnitOfWork unitOfWork = new CoreUnitOfWork(dataSource);

            ISettingRepository settingRepository = new SettingRepository(dataSource);

            IClockService clockService = new ClockService(unitOfWork, settingRepository);

            clockService.Reset();


            Assert.Pass();
        }
    }
}