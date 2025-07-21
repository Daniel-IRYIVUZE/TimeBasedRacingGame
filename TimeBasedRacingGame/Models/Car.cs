using System;

namespace TimeBasedRacingGame.Models
{
    /// <summary>
    /// Represents a race car with specific performance characteristics
    /// </summary>
    public class Car
    {
        public string Name { get; set; }
        public double MaxSpeed { get; set; }        // km/h
        public double FuelCapacity { get; set; }    // liters
        public double CurrentFuel { get; set; }
        public double FuelConsumptionRate { get; set; } // liters per km at max speed
        
        /// <summary>
        /// Current speed (0 to MaxSpeed)
        /// </summary>
        public double CurrentSpeed { get; set; }
        
        /// <summary>
        /// Calculates fuel consumption based on current speed
        /// </summary>
        /// <param name="distance">Distance traveled in km</param>
        /// <returns>Fuel consumed in liters</returns>
        public double CalculateFuelConsumption(double distance)
        {
            // Fuel consumption increases with speed
            double speedRatio = CurrentSpeed / MaxSpeed;
            return distance * FuelConsumptionRate * speedRatio;
        }
        
        /// <summary>
        /// Refuels the car by specified amount
        /// </summary>
        /// <param name="amount">Amount to refuel in liters</param>
        /// <exception cref="InvalidOperationException">Thrown if overfilling</exception>
        public void Refuel(double amount)
        {
            if (CurrentFuel + amount > FuelCapacity)
            {
                throw new InvalidOperationException("Cannot overfill the fuel tank");
            }
            CurrentFuel += amount;
        }
    }
}