namespace TimeBasedRacingGame
{
    /// <summary>
    /// Enum for speed levels
    /// </summary>
    public enum SpeedLevel
    {
        /// <summary>Slow speed (30 km/h)</summary>
        Slow = 30,
        /// <summary>Medium speed (60 km/h)</summary>
        Medium = 60,
        /// <summary>Fast speed (90 km/h)</summary>
        Fast = 90
    }

    /// <summary>
    /// Enum for player actions
    /// </summary>
    public enum PlayerAction
    {
        /// <summary>Increase speed</summary>
        SpeedUp,
        /// <summary>Maintain current speed</summary>
        MaintainSpeed,
        /// <summary>Stop to refuel</summary>
        PitStop
    }

    /// <summary>
    /// Enum for car types
    /// </summary>
    public enum CarType
    {
        /// <summary>Fuel-efficient but slower</summary>
        Eco,
        /// <summary>Fast but high fuel consumption</summary>
        Sport,
        /// <summary>Balanced speed and fuel</summary>
        Muscle
    }
}