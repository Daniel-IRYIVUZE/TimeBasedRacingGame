using System;
using System.Collections.Generic;
using System.Windows;
using TimeBasedRacingGame.Models;

namespace TimeBasedRacingGame
{
    public partial class MainWindow : Window
    {
        public RaceManager RaceManager { get; set; }
        private List<Car> availableCars;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
            // Initialize race manager
            RaceManager = new RaceManager();
            
            // Setup available cars
            availableCars = new List<Car>
            {
                new Car {
                    Name = "Speedster",
                    MaxSpeed = 320,
                    FuelCapacity = 100,
                    FuelConsumptionRate = 0.15
                },
                new Car {
                    Name = "Eco Racer",
                    MaxSpeed = 250,
                    FuelCapacity = 120,
                    FuelConsumptionRate = 0.10
                },
                new Car {
                    Name = "Heavyweight",
                    MaxSpeed = 280,
                    FuelCapacity = 150,
                    FuelConsumptionRate = 0.18
                }
            };
            
            carComboBox.ItemsSource = availableCars;
            carComboBox.SelectedIndex = 0;
        }
        
        private void StartRaceButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCar = (Car)carComboBox.SelectedItem;
            var track = new Track { Name = "Grand Prix Circuit", LapDistance = 5.0, TotalLaps = 5 };
            
            RaceManager.InitializeRace(selectedCar, track);
            
            // Enable action buttons
            speedUpButton.IsEnabled = true;
            maintainSpeedButton.IsEnabled = true;
            pitStopButton.IsEnabled = true;
            
            // Disable setup controls
            carComboBox.IsEnabled = false;
            startRaceButton.IsEnabled = false;
            
            statusTextBlock.Text = "Race started! Make your move.";
        }
        
        private void SpeedUpButton_Click(object sender, RoutedEventArgs e)
        {
            statusTextBlock.Text = RaceManager.SpeedUp();
            CheckRaceFinished();
        }
        
        private void MaintainSpeedButton_Click(object sender, RoutedEventArgs e)
        {
            statusTextBlock.Text = RaceManager.MaintainSpeed();
            CheckRaceFinished();
        }
        
        private void PitStopButton_Click(object sender, RoutedEventArgs e)
        {
            // Refuel 25 liters on pit stop
            statusTextBlock.Text = RaceManager.PitStop(25);
            CheckRaceFinished();
        }
        
        private void CheckRaceFinished()
        {
            if (RaceManager.RaceFinished)
            {
                speedUpButton.IsEnabled = false;
                maintainSpeedButton.IsEnabled = false;
                pitStopButton.IsEnabled = false;
                
                // Enable setup for new race
                carComboBox.IsEnabled = true;
                startRaceButton.IsEnabled = true;
            }
        }
    }
}