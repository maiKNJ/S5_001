# Auditory interactive movie for dome screen
This program is a movie with two interactive scenes, which has been made in Unity 2019.4.9f1. The interactive scenes is controlled with sounds. 
The program is designed for a dome screen but it works well with any other screen as well.

## Getting started
Using these following instructions you can get the movie running on your screen.

### Prerequisities
To use the program you will need to have at least Unity 2019.4.9f1. You can get unity [here](https://unity3d.com/get-unity/download) and follow the instructions.

Before running the program you should be sure to have a connected microphone to your computer running from.
The movie can also be run with the use of buttons instead of using audio input. 
To change to button input, you will have to go into the script __object_spawner__ and change the line: `private bool soundInput = true;`
to: `private bool soundInput = false;`.

### Running the program
The movie can either be played in game mode or it can be build, and run through this.
If the movie is played through game mode, be sure to start it from the scene "IntroScene" (Assets -> Scenes -> IntroScene).
