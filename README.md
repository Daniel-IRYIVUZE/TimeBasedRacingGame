# Time-Based Racing Game Simulation

This repository contains my completed project for the **Time-Based Racing Game Simulation**, developed using **C# and WPF**. The game is a turn-based racing simulation called **C# Speed Rush**, where players manage speed, fuel, and time over 5 laps of a race track. The interface is built using WPF Forms and provides real-time updates based on player actions.

## Overview

In this simulation, the player selects a car and makes turn-based decisions to **Speed Up**, **Maintain Speed**, or make a **Pit Stop**. The game dynamically updates the race state including:

* Fuel level
* Time remaining
* Current lap
* Speed or current status

The game ends when either all 5 laps are completed or the player runs out of fuel or time.

---

## How to Run the Project

### Option 1: Using Visual Studio (Recommended)

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Daniel-IRYIVUZE/TimeBasedRacingGame.git
   cd TimeBasedRacingGame
   ```

2. **Open the Solution**

   * Launch **Visual Studio 2022** (or later).
   * Open the `TimeBasedRacingGame.sln` file.

3. **Build the Solution**

   * Go to `Build > Build Solution` or press `Ctrl + Shift + B`.

4. **Run the Application**

   * Press `F5` or click the `Start` button.
   * The WPF UI will launch, allowing you to:

     * Select a car
     * Monitor race progress (lap, fuel, time)
     * Make decisions using buttons (Speed Up, Maintain Speed, Pit Stop)

5. **Run Unit Tests**

   * Open the **Test Explorer** via `Test > Test Explorer`.
   * Run all tests to verify game logic and edge cases.

---

### Option 2: Using Terminal (Windows Only)

> Note: WPF is only supported on Windows. Ensure the .NET SDK (6.0+) is installed.

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Daniel-IRYIVUZE/TimeBasedRacingGame.git
   cd TimeBasedRacingGame
   ```

2. **Build the Project**

   ```bash
   dotnet build
   ```

3. **Run the Application**

   ```bash
   dotnet run --project TimeBasedRacingGame
   ```

4. **Run Unit Tests**

   ```bash
   dotnet test
   ```

---

### Architecture Document

This document describes the application's architecture, including the roles of the `Car`, `RaceManager`, and `Track` classes. It explains the choice of data structures (e.g., `List`, `Queue`, `Enum`) and includes a UML class diagram and a race logic flowchart.
[Architecture Document Link](https://example.com/architecture-document)

### Testing Strategy Document

This outlines my unit testing approach, including how I tested fuel usage, lap progression, and race-ending conditions. It also documents test tools used, edge cases considered, and test results.
[Testing Strategy Document Link](https://example.com/testing-strategy)

### Video Demonstration

This video walkthrough demonstrates a full gameplay session, showing how decisions impact the simulation through the WPF interface.
[Video Demo Link](https://example.com/video-demo)
