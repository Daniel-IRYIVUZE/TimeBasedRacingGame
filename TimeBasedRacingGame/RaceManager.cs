using System;

namespace TimeBasedRacingGame
{
    /// <summary>
    /// Manages the state and progress of a racing game
    /// </summary>
    public class RaceManager
    {
        /// <summary>
        /// Gets the player's car
        /// </summary>
        public Car Car { get; private set; }

        /// <summary>
        /// Gets the race track
        /// </summary>
        public Track Track { get; private set; }

        /// <summary>
        /// Gets the current lap number
        /// </summary>
        public int CurrentLap { get; private set; }

        /// <summary>
        /// Gets the progress in the current lap in kilometers
        /// </summary>
        public double LapProgressKm { get; private set; }

        /// <summary>
        /// Gets the remaining race time in seconds
        /// </summary>
        public double TimeRemainingSeconds { get; private set; }

        /// <summary>
        /// Gets whether the race has finished
        /// </summary>
        public bool RaceFinished { get; private set; }

        private const int TotalRaceTimeSeconds = 1800; // 30 minutes

        /// <summary>
        /// Initializes a new race manager with the specified car and track
        /// </summary>
        /// <param name="car">The player's car</param>
        /// <param name="track">The race track</param>
        /// <exception cref="ArgumentNullException">Thrown if car or track is null</exception>
        public RaceManager(Car car, Track track)
        {
            Car = car ?? throw new ArgumentNullException(nameof(car));
            Track = track ?? throw new ArgumentNullException(nameof(track));

            CurrentLap = 1;
            LapProgressKm = 0;
            TimeRemainingSeconds = TotalRaceTimeSeconds;
            RaceFinished = false;
        }

        /// <summary>
        /// Executes a player action for the current turn
        /// </summary>
        /// <param name="action">The action to perform</param>
        /// <exception cref="InvalidOperationException">Thrown if race has finished</exception>
        public void ExecuteTurn(PlayerAction action)
        {
            if (RaceFinished)
                throw new InvalidOperationException("Race has finished.");

            switch (action)
            {
                case PlayerAction.SpeedUp:
                    Car.SpeedUp(10);
                    break;
                case PlayerAction.MaintainSpeed:
                    Car.MaintainSpeed();
                    break;
                case PlayerAction.PitStop:
                    PerformPitStop();
                    return;
            }

            // Calculate distance covered this turn (10 seconds at current speed)
            double timeStepSeconds = 10;
            double distanceCovered = Car.CurrentSpeed * (timeStepSeconds / 3600.0);

            try
            {
                Car.Drive(distanceCovered);
            }
            catch (InvalidOperationException)
            {
                RaceFinished = true;
                return;
            }

            LapProgressKm += distanceCovered;
            TimeRemainingSeconds -= timeStepSeconds;

            CheckRaceCompletion();
        }

        /// <summary>
        /// Generates a text-based progress bar for the current lap
        /// </summary>
        /// <returns>String representing lap progress</returns>
        public string GetLapProgressBar()
        {
            int totalBlocks = 20;
            double progressRatio = LapProgressKm / Track.LapLengthKm;
            int blocksFilled = (int)(progressRatio * totalBlocks);

            return "[" + new string('=', blocksFilled) + ">" + 
                   new string(' ', totalBlocks - blocksFilled) + "]";
        }

        private void PerformPitStop()
        {
            double refuelAmount = Car.MaxFuelCapacity - Car.CurrentFuel;
            if (refuelAmount > 0)
                Car.Refuel(refuelAmount);

            TimeRemainingSeconds -= 30;
            CheckRaceCompletion();
        }

        private void CheckRaceCompletion()
        {
            if (TimeRemainingSeconds <= 0 || Car.CurrentFuel <= 0)
            {
                RaceFinished = true;
                return;
            }

            if (LapProgressKm >= Track.LapLengthKm)
            {
                CurrentLap++;
                LapProgressKm = 0;

                if (CurrentLap > Track.TotalLaps)
                {
                    RaceFinished = true;
                }
            }
        }
    }
}