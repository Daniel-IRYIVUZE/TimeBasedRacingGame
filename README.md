# C# Speed Rush - Time-Based Racing Game

## Game Description

C# Speed Rush is a strategic, turn-based racing simulation game developed using WPF and C#. The player must complete a 5-lap race while managing three critical resources: **speed**, **fuel**, and **time**. Every decision impacts the race outcome, encouraging players to balance performance and resource efficiency.

## Key Features

* Three car types with distinct performance characteristics
* Turn-based gameplay emphasizing strategy and resource management
* Real-time tracking of fuel, speed, and lap progress
* Intuitive UI with progress indicators
* Robust input validation and exception handling

---

## System Requirements

* **Operating System:** Windows 10 or later
* **.NET Runtime:** Version 6.0 or higher
* **Recommended IDE:** Visual Studio 2022

---

## Installation and Execution

### Prerequisites

1. Install [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. (Optional) Install Visual Studio 2022 with the WPF workload

---

### Steps to Clone and Run the Application

#### Method 1: Using Visual Studio

1. Clone the repository:

   ```bash
   git clone https://github.com/Daniel-IRYIVUZE/TimeBasedRacingGame.git
   cd TimeBasedRacingGame
   ```
2. Open the solution file `TimeBasedRacingGame.sln` in Visual Studio
3. Build the solution (`Ctrl + Shift + B`)
4. Run the project (`F5` or select "Start" in the toolbar)

#### Method 2: Using .NET CLI

1. Clone the repository:

   ```bash
   git clone https://github.com/Daniel-IRYIVUZE/TimeBasedRacingGame.git
   cd TimeBasedRacingGame
   ```
2. Navigate to the project directory:

   ```bash
   cd TimeBasedRacingGame
   ```
3. Build the project:

   ```bash
   dotnet build
   ```
4. Run the game:

   ```bash
   dotnet run --project TimeBasedRacingGame
   ```

---

## Gameplay Instructions

### Car Selection

Players can choose from three car types, each with different performance and fuel efficiency:

* **Eco Car**

  * Maximum Speed: 160 km/h
  * Fuel Capacity: 120L
  * Fuel Consumption: 0.1 L/km

* **Sport Car**

  * Maximum Speed: 220 km/h
  * Fuel Consumption: 0.25 L/km

* **Muscle Car**

  * Maximum Speed: 180 km/h
  * Fuel Consumption: 0.3 L/km

---

### Turn Actions

* **Speed Up**: Increases the car's speed by 10 km/h. Higher speed consumes more fuel.
* **Maintain Speed**: Keeps the current speed and maintains average fuel consumption.
* **Pit Stop**: Refuels the car to full but costs 30 seconds of race time.

---

### Victory Conditions

To win the race, the player must complete five laps before either:

* Fuel runs out
* The race time limit (30 minutes) is exceeded

---

## XML Documentation

All public classes and methods are documented using XML comments.

To generate the XML documentation file, run:

```bash
dotnet build /p:DocumentationFile=bin\Debug\net6.0-windows\TimeBasedRacingGame.xml
```

