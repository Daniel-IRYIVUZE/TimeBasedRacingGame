using System;
using System.ComponentModel;

namespace TimeBasedRacingGame.Models
{
    /// <summary>
    /// Manages the core racing game logic and state
    /// </summary>
    public class RaceManager : INotifyPropertyChanged
    {
        private const double TimePerTurn = 1.0; // 1 minute per turn
        private const double MaxRaceTime = 60.0; // 60 minutes max
        
        public Car SelectedCar { get; private set; }
        public Track CurrentTrack { get; private set; }
        
        private int _currentLap = 1;
        private double _currentLapDistance = 0;
        private double _elapsedTime = 0;
        private bool _raceFinished = false;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        /// <summary>
        /// Current lap number (1 to TotalLaps)
        /// </summary>
        public int CurrentLap
        {
            get => _currentLap;
            private set
            {
                _currentLap = value;
                OnPropertyChanged(nameof(CurrentLap));
                OnPropertyChanged(nameof(LapDisplay));
            }
        }
        
        /// <summary>
        /// Distance traveled in current lap (km)
        /// </summary>
        public double CurrentLapDistance
        {
            get => _currentLapDistance;
            private set
            {
                _currentLapDistance = value;
                OnPropertyChanged(nameof(CurrentLapDistance));
                OnPropertyChanged(nameof(LapProgress));
                OnPropertyChanged(nameof(LapDisplay));
            }
        }
        
        /// <summary>
        /// Percentage completion of current lap
        /// </summary>
        public double LapProgress => CurrentTrack.CalculateLapProgress(CurrentLapDistance);
        
        /// <summary>
        /// Formatted lap display (e.g., "Lap 2/5")
        /// </summary>
        public string LapDisplay => $"Lap {CurrentLap}/{CurrentTrack.TotalLaps}";
        
        /// <summary>
        /// Elapsed race time in minutes
        /// </summary>
        public double ElapsedTime
        {
            get => _elapsedTime;
            private set
            {
                _elapsedTime = value;
                OnPropertyChanged(nameof(ElapsedTime));
                OnPropertyChanged(nameof(TimeRemaining));
                OnPropertyChanged(nameof(TimeRemainingDisplay));
            }
        }
        
        /// <summary>
        /// Remaining race time in minutes
        /// </summary>
        public double TimeRemaining => MaxRaceTime - ElapsedTime;
        
        /// <summary>
        /// Formatted time remaining display
        /// </summary>
        public string TimeRemainingDisplay => $"{TimeRemaining:0} min remaining";
        
        /// <summary>
        /// Indicates if the race has finished
        /// </summary>
        public bool RaceFinished
        {
            get => _raceFinished;
            private set
            {
                _raceFinished = value;
                OnPropertyChanged(nameof(RaceFinished));
            }
        }
        
        /// <summary>
        /// Initializes a new race with selected car and track
        /// </summary>
        /// <param name="car">Selected car</param>
        /// <param name="track">Selected track</param>
        public void InitializeRace(Car car, Track track)
        {
            SelectedCar = car ?? throw new ArgumentNullException(nameof(car));
            CurrentTrack = track ?? throw new ArgumentNullException(nameof(track));
            
            SelectedCar.CurrentSpeed = SelectedCar.MaxSpeed * 0.5; // Start at 50% speed
            SelectedCar.CurrentFuel = SelectedCar.FuelCapacity;
            
            CurrentLap = 1;
            CurrentLapDistance = 0;
            ElapsedTime = 0;
            RaceFinished = false;
        }
        
        /// <summary>
        /// Executes a turn where the player chooses to speed up
        /// </summary>
        /// <returns>Result message</returns>
        public string SpeedUp()
        {
            if (RaceFinished) return "Race is already finished!";
            
            // Increase speed by 10% of max speed, but don't exceed max
            SelectedCar.CurrentSpeed = Math.Min(
                SelectedCar.CurrentSpeed + SelectedCar.MaxSpeed * 0.1,
                SelectedCar.MaxSpeed);
            
            return ExecuteTurn();
        }
        
        /// <summary>
        /// Executes a turn where the player chooses to maintain speed
        /// </summary>
        /// <returns>Result message</returns>
        public string MaintainSpeed()
        {
            if (RaceFinished) return "Race is already finished!";
            return ExecuteTurn();
        }
        
        /// <summary>
        /// Executes a pit stop to refuel
        /// </summary>
        /// <param name="fuelAmount">Amount to refuel</param>
        /// <returns>Result message</returns>
        public string PitStop(double fuelAmount)
        {
            if (RaceFinished) return "Race is already finished!";
            
            try
            {
                SelectedCar.Refuel(fuelAmount);
                ElapsedTime += TimePerTurn;
                return $"Pit stop successful! Added {fuelAmount} liters of fuel.";
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
        }
        
        private string ExecuteTurn()
        {
            // Calculate distance traveled this turn (km)
            double distance = (SelectedCar.CurrentSpeed / 60) * TimePerTurn;
            
            // Calculate fuel consumption
            double fuelUsed = SelectedCar.CalculateFuelConsumption(distance);
            
            // Check for out of fuel
            if (fuelUsed > SelectedCar.CurrentFuel)
            {
                RaceFinished = true;
                return "Out of fuel! Race over.";
            }
            
            // Update state
            SelectedCar.CurrentFuel -= fuelUsed;
            CurrentLapDistance += distance;
            ElapsedTime += TimePerTurn;
            
            // Check for lap completion
            if (CurrentLapDistance >= CurrentTrack.LapDistance)
            {
                CurrentLapDistance = 0;
                CurrentLap++;
                
                if (CurrentLap > CurrentTrack.TotalLaps)
                {
                    RaceFinished = true;
                    return $"Race completed in {ElapsedTime:0} minutes!";
                }
                
                return $"Lap {CurrentLap-1} completed! Starting lap {CurrentLap}.";
            }
            
            // Check for time out
            if (ElapsedTime >= MaxRaceTime)
            {
                RaceFinished = true;
                return "Time's up! Race over.";
            }
            
            return $"Advanced {distance:0.00} km. Fuel remaining: {SelectedCar.CurrentFuel:0.0} liters.";
        }
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}