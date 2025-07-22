using System;
using System.Windows;
using System.Windows.Controls;

namespace TimeBasedRacingGame
{
    public partial class MainWindow : Window
    {
        private RaceManager raceManager;
        private const int TotalRaceTime = 1800; // 30 minutes in seconds

        public MainWindow()
        {
            InitializeComponent();
            UpdateUI("Select a car and start the race!");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (carComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a car first!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Car car = CreateSelectedCar();
            Track track = new Track(5, 5.0); // 5 laps, 5km each
            raceManager = new RaceManager(car, track);

            startButton.IsEnabled = false;
            carComboBox.IsEnabled = false;
            speedUpButton.IsEnabled = true;
            maintainButton.IsEnabled = true;
            pitStopButton.IsEnabled = true;

            UpdateUI("Race started! Make your move!");
        }

        private Car CreateSelectedCar()
        {
            string carType = ((ComboBoxItem)carComboBox.SelectedItem).Tag.ToString();
            
            return carType switch
            {
                "Eco" => new Car("Eco Car", CarType.Eco, 120, 0.1, 160),
                "Sport" => new Car("Sport Car", CarType.Sport, 80, 0.25, 220),
                "Muscle" => new Car("Muscle Car", CarType.Muscle, 100, 0.3, 180),
                _ => throw new InvalidOperationException("Invalid car type")
            };
        }

        private void UpdateUI(string status = null)
        {
            if (raceManager == null) return;

            if (!string.IsNullOrEmpty(status))
            {
                statusText.Text = status;
            }

            // Update lap info
            lapText.Text = $"{raceManager.CurrentLap}/{raceManager.Track.TotalLaps}";

            // Update fuel info
            fuelBar.Value = (raceManager.Car.CurrentFuel / raceManager.Car.MaxFuelCapacity) * 100;
            fuelText.Text = $"{raceManager.Car.CurrentFuel:F1}/{raceManager.Car.MaxFuelCapacity} L";

            // Update time info
            timeBar.Value = (raceManager.TimeRemainingSeconds / TotalRaceTime) * 100;
            TimeSpan time = TimeSpan.FromSeconds(raceManager.TimeRemainingSeconds);
            timeText.Text = $"{time:mm\\:ss} remaining";

            // Update speed
            speedText.Text = $"{raceManager.Car.CurrentSpeed} km/h";

            // Update progress
            progressText.Text = raceManager.GetLapProgressBar();

            // Check if race finished
            if (raceManager.RaceFinished)
            {
                speedUpButton.IsEnabled = false;
                maintainButton.IsEnabled = false;
                pitStopButton.IsEnabled = false;

                string message = raceManager.CurrentLap > raceManager.Track.TotalLaps 
                    ? "Race complete! You finished all laps!" 
                    : "Race over! You ran out of " + 
                      (raceManager.TimeRemainingSeconds <= 0 ? "time" : "fuel");
                
                statusText.Text = message;
                MessageBox.Show(message, "Race Finished", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExecuteAction(PlayerAction action)
        {
            try
            {
                raceManager.ExecuteTurn(action);
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SpeedUpButton_Click(object sender, RoutedEventArgs e) => ExecuteAction(PlayerAction.SpeedUp);
        private void MaintainButton_Click(object sender, RoutedEventArgs e) => ExecuteAction(PlayerAction.MaintainSpeed);
        private void PitStopButton_Click(object sender, RoutedEventArgs e) => ExecuteAction(PlayerAction.PitStop);
    }
}