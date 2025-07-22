namespace TimeBasedRacingGame
{
    /// <summary>
    /// Represents a race track with lap information
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Gets the total number of laps in the race
        /// </summary>
        public int TotalLaps { get; private set; }

        /// <summary>
        /// Gets the length of each lap in kilometers
        /// </summary>
        public double LapLengthKm { get; private set; }

        /// <summary>
        /// Initializes a new track with the specified laps and length
        /// </summary>
        /// <param name="laps">Total number of laps</param>
        /// <param name="lapLengthKm">Length of each lap in kilometers</param>
        /// <exception cref="ArgumentException">Thrown for invalid lap values</exception>
        public Track(int laps, double lapLengthKm)
        {
            if (laps <= 0)
                throw new ArgumentException("Laps must be positive", nameof(laps));
            if (lapLengthKm <= 0)
                throw new ArgumentException("Lap length must be positive", nameof(lapLengthKm));

            TotalLaps = laps;
            LapLengthKm = lapLengthKm;
        }
    }
}