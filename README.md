# Game of Life Simulation

This is a simple implementation of Conway's Game of Life, a cellular automaton devised by mathematician John Conway. The Game of Life is known for its fascinating patterns and behavior arising from a few simple rules. This project allows you to interact with and observe the simulation in action.

## Table of Contents

- [About the Project](#about-the-project)
- [Getting Started](#getting-started)
- [Compile From Source](#compile-from-source)
- [Usage](#usage)
- [Features](#features)
- [Inspiration](#inspiration)
- [Contributing](#contributing)
## About the Project

The Game of Life is a "zero-player game" that evolves according to its initial state. It is governed by simple rules:

1. Any live cell with fewer than two live neighbors dies (underpopulation).
2. Any live cell with two or three live neighbors lives on to the next generation.
3. Any live cell with more than three live neighbors dies (overpopulation).
4. Any dead cell with exactly three live neighbors becomes a live cell (reproduction).

This project provides an interface to visualize and interact with the simulation. You can draw your initial pattern, randomize the grid, adjust the simulation speed, and more.

## Getting Started

To get started with the Game of Life simulation, you can download the latest release from the [Releases](https://github.com/RacialGamer/GameOfLife/releases) page of this repository. Follow these steps:

1. Visit the [Releases](https://github.com/RacialGamer/GameOfLife/releases) page.
2. Download the latest release
3. After downloading, open the executable file to run the simulation.


## Compile From Source
1. Clone this repository to your local machine:

   ```sh
   git clone https://github.com/RacialGamer/GameOfLife
2. Open the project in your preferred development environment that supports C# and Windows Forms.
3. Build and run the project.

## Usage
Once you run the application, you'll see a window containing the Game of Life grid. Here's how to use it:

- Left-click on a cell to make it alive.
- Right-click on a cell to make it dead.
- Use the "Clear Grid" button to clear the entire grid.
- Click the "Pause" button to pause or resume the simulation.
- Click the "Randomize" button to generate a random initial state.
- Adjust the speed of the simulation using the speed slider.
- Change the grid cell size using the grid size slider.
- You can also change the color of the live cells by entering a hex color code in the provided textbox.

## Features
- Drawing and erasing cells by clicking and dragging.
- Randomizing the initial state.
- Customizable cell color.

## Inspiration
This project was inspired by John Conway
https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life

### Contributing
Contributions are welcome! Feel free to open issues and submit pull requests to contribute to this project.
