using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TimeBasedRacingGame.Tests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void Refuel_OverCapacity_ThrowsException()
        {
            var car = new Car("Test", CarType.Eco, 50, 0.05, 60);
            Assert.ThrowsException<InvalidOperationException>(() => car.Refuel(10));
        }

        [TestMethod]
        public void Drive_InsufficientFuel_ThrowsException()
        {
            var car = new Car("Test", CarType.Sport, 50, 0.05, 60);
            car.CurrentFuel = 0.1;
            Assert.ThrowsException<InvalidOperationException>(() => car.Drive(10));
        }

        [TestMethod]
        public void SpeedUp_DoesNotExceedMaxSpeed()
        {
            var car = new Car("Test", CarType.Muscle, 50, 0.05, 100);
            car.SpeedUp(150);
            Assert.IsTrue(car.CurrentSpeed <= car.MaxSpeed);
        }
    }
}
