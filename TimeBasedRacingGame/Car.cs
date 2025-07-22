using System;

namespace TimeBasedRacingGame
{
    /// <summary>
    /// Represents a racing car with attributes for speed, fuel consumption and capacity.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Gets the name of the car
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the car (Eco, Sport, Muscle)
        /// </summary>
        public CarType Type { get; private set; }

        /// <summary>
        /// Gets the maximum fuel capacity in liters
        /// </summary>
        public int MaxFuelCapacity { get; private set; }

        /// <summary>
        /// Gets the fuel consumption rate in liters per km
        /// </summary>
        public double FuelConsumptionPerKm { get; private set; }

        /// <summary>
        /// Gets the maximum speed in km/h
        /// </summary>
        public int MaxSpeed { get; private set; }

        /// <summary>
        /// Gets the current fuel level in liters
        /// </summary>
        public double CurrentFuel { get; private set; }

        /// <summary>
        /// Gets the current speed in km/h
        /// </summary>
        public int CurrentSpeed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Car class
        /// </summary>
        /// <param name="name">Display name of the car</param>
        /// <param name="type">Car type affecting performance</param>
        /// <param name="maxFuel">Maximum fuel capacity in liters</param>
        /// <param name="fuelConsumption">Fuel consumption in liters per km</param>
        /// <param name="maxSpeed">Maximum speed in km/h</param>
        public Car(string name, CarType type, int maxFuel, double fuelConsumption, int maxSpeed)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            MaxFuelCapacity = maxFuel;
            FuelConsumptionPerKm = fuelConsumption;
            MaxSpeed = maxSpeed;

            CurrentFuel = maxFuel;
            CurrentSpeed = 0;
        }

        /// <summary>
        /// Increases the car's speed by the specified increment
        /// </summary>
        /// <param name="increment">Speed increase in km/h</param>
        public void SpeedUp(int increment)
        {
            int newSpeed = CurrentSpeed + increment;
            CurrentSpeed = Math.Min(newSpeed, MaxSpeed);
        }

        /// <summary>
        /// Maintains the car's current speed
        /// </summary>
        public void MaintainSpeed()
        {
            // No changes, current speed remains the same
        }

        /// <summary>
        /// Refuels the car by the specified amount
        /// </summary>
        /// <param name="amount">Fuel to add in liters</param>
        /// <exception cref="InvalidOperationException">Thrown when attempting to overfill fuel</exception>
        public void Refuel(double amount)
        {
            if (CurrentFuel + amount > MaxFuelCapacity)
                throw new InvalidOperationException("Cannot overfill fuel beyond capacity.");

            CurrentFuel += amount;
        }

        /// <summary>
        /// Simulates driving the car for a specified distance
        /// </summary>
        /// <param name="distanceKm">Distance to drive in kilometers</param>
        /// <exception cref="InvalidOperationException">Thrown when not enough fuel</exception>
        public void Drive(double distanceKm)
        {
            if (distanceKm < 0)
                throw new ArgumentException("Distance cannot be negative.", nameof(distanceKm));

            double neededFuel = distanceKm * FuelConsumptionPerKm;
            if (neededFuel > CurrentFuel)
                throw new InvalidOperationException("Not enough fuel to drive the distance.");

            CurrentFuel -= neededFuel;
        }
    }
}