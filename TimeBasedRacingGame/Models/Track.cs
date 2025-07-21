namespace TimeBasedRacingGame.Models
{
    /// <summary>
    /// Represents a race track with lap information
    /// </summary>
    public class Track
    {
        public string Name { get; set; }
        public double LapDistance { get; set; } // in km
        public int TotalLaps { get; set; } = 5;
        
        /// <summary>
        /// Calculates progress percentage based on current distance
        /// </summary>
        /// <param name="currentDistance">Distance traveled in current lap</param>
        /// <returns>Percentage completion of current lap</returns>
        public double CalculateLapProgress(double currentDistance)
        {
            return (currentDistance / LapDistance) * 100;
        }
    }
}