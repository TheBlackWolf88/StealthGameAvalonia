# Stealth Game

A stealth-based strategy game where the player must reach the exit without being seen by patrolling guards.

## 📌 Overview

In this game, the player navigates through an `n x n` grid-based map filled with walls, floors, and patrolling guards. The objective is to reach the exit without entering a guard’s line of sight. Guards move every 2 seconds and can see in a 5x5 area unless blocked by walls. The player moves one tile at a time using the `W`, `A`, `S`, `D` keys.

## 🎮 Features

- Three distinct map sizes with unique layouts
- Player and guard movement with real-time visibility detection
- Dynamic patrol AI that changes direction on wall collision
- Pause menu with options to resume or start a new game
- Level selection menu on game start
- Visual representation using Avalonia UI with `UniformGrid`

## 🧱 Architecture

- **Model:** Core logic, including level representation (`Level`, `Board`, `Floor`, `Guard`, `Player`)
- **View:** Avalonia-based UI for rendering maps and handling input
- **Persistence:** Handles loading maps from default `Resources` or `%userprofile%\Documents\My Games\StealthGame\`

## 🧪 Testing

Unit tests cover all major components:

- `LevelTest`: Validates map setup and coordinate logic
- `FloorTests`: Tests entity management on tiles
- `PlayerTests`: Validates player movement
- `GuardTests`: Validates guard movement and simulation

Tests are located in the `StealthGameTest` namespace.

## 📂 Map Files

- Default maps: `/Resources`
- User maps: `%userprofile%\Documents\My Games\StealthGame\`
- Maps contain layout, entity start positions, and exit location

## 🛠 Controls

- **Move:** `W`, `A`, `S`, `D`
- **Pause/Resume:** `Esc`
- **New Game:** Select via pause menu

## 📧 Author

**Bekovics Dániel**  
📧 g9e74r@inf.elte.hu
