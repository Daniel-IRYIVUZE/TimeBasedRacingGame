using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeBasedRacingGame.Models;

namespace TimeBasedRacingGame.Tests
{
    [TestClass]
    public class RaceManagerTests
    {
        private Car testCar;
        private Track testTrack;
        private RaceManager raceManager;
        
        [TestInitialize]
        public void Setup()
        {
            testCar = new Car
            {
                Name = "Test Car",
                MaxSpeed = 300,
                FuelCapacity = 100,
                CurrentFuel = 100,
                FuelConsumptionRate = 0.1
            };
            
            testTrack = new Track
            {
                Name = "Test Track",
                LapDistance = 5.0,
                TotalLaps = 5
            };
            
            raceManager = new RaceManager();
            raceManager.InitializeRace(testCar, testTrack);
        }
        
        [TestMethod]
        public void SpeedUp_IncreasesSpeedWithoutExceedingMax()
        {
            // Arrange
            double initialSpeed = testCar.CurrentSpeed;
            
            // Act
            raceManager.SpeedUp();
            
            // Assert
            Assert.IsTrue(testCar.CurrentSpeed > initialSpeed);
            Assert.IsTrue(testCar.CurrentSpeed <= testCar.MaxSpeed);
        }
        
        [TestMethod]
        public void ExecuteTurn_ConsumesFuel()
        {
            // Arrange
            double initialFuel = testCar.CurrentFuel;
            
            // Act
            raceManager.MaintainSpeed();
            
            // Assert
            Assert.IsTrue(testCar.CurrentFuel < initialFuel);
        }
        
        [TestMethod]
        public void PitStop_RefuelsCar()
        {
            // Arrange
            testCar.CurrentFuel = 50;
            double refuelAmount = 25;
            
            // Act
            raceManager.PitStop(refuelAmount);
            
            // Assert
            Assert.AreEqual(75, testCar.CurrentFuel);
        }
        
        [TestMethod]
        public void ExecuteTurn_AdvancesLapWhenDistanceExceedsLapLength()
        {
            // Arrange
            testCar.CurrentSpeed = 300; // 5 km per minute (300 km/h)
            testTrack.LapDistance = 5;
            
            // Act - one turn at 300 km/h covers exactly one lap
            raceManager.MaintainSpeed();
            
            // Assert
            Assert.AreEqual(2, raceManager.CurrentLap);
            Assert.AreEqual(0, raceManager.CurrentLapDistance);
        }
        
        [TestMethod]
        public void RaceFinishes_WhenAllLapsCompleted()
        {
            // Arrange
            testCar.CurrentSpeed = 300; // 5 km per minute (300 km/h)
            testTrack.LapDistance = 5;
            testTrack.TotalLaps = 2; // Short race for testing
            
            // Act - complete two laps
            raceManager.MaintainSpeed(); // Lap 1
            raceManager.MaintainSpeed(); // Lap 2
            
            // Assert
            Assert.IsTrue(raceManager.RaceFinished);
        }
    }
}