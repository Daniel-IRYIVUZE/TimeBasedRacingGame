using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimeBasedRacingGame.Tests
{
    [TestClass]
    public class RaceManagerTests
    {
        [TestMethod]
        public void ExecuteTurn_LapProgressesCorrectly()
        {
            var car = new Car("TestCar", CarType.Eco, 50, 0.05, 60);
            var track = new Track(2, 5);
            var manager = new RaceManager(car, track);

            manager.ExecuteTurn(PlayerAction.MaintainSpeed);
            Assert.IsTrue(manager.CurrentLap == 1);
            Assert.IsTrue(manager.LapProgressKm >= 0);
        }

        [TestMethod]
        public void ExecuteTurn_EndsRaceWhenLapsComplete()
        {
            var car = new Car("TestCar", CarType.Eco, 50, 0.05, 60);
            var track = new Track(1, 0.001); // Very small lap length for quick finish
            var manager = new RaceManager(car, track);

            while (!manager.RaceFinished)
                manager.ExecuteTurn(PlayerAction.MaintainSpeed);

            Assert.IsTrue(manager.RaceFinished);
        }
    }
}
