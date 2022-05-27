# P2-semester-project
University semester Project 2: Game for blind players.

This is a mobile video game in the dungeon crawler genre. Everything in the game has sound cues
making it playable by blind players. You control a character in a tile-based dungeon, your goal is
to get to the end of the dungeon without getting hit by an obstacle. The game is controlled by swiping
the screen and occasionally shaking the device.

Unity was used to make the game, as it was a requirement for our university project to make an application
for android using unity. We used UAP Accessibility Manager to include more accesibility features in the
game, such as text to speech functionality for menus. We also Steam Audio for more realistic 3D audio.

It was difficult to make the audio helpful enough at points like turns in the dungeon. There were also
problems with colliders and implementing steam audio, all colliders had to be 3D so that sound could
bounce of of them realistically. The game had a lot of planned features that did not make it in,
including combat and lockpicking. These features were not implemented because of time restrictions.


This project can be opened by downloading [the latest release](https://github.com/DangelTheMangel/P2-semester-project/releases/latest), opening up Unity Hub and adding the folder as a project.
To run it optimally, you will need unity version: 2020.3.30f1
The game can be played directly in the unity editor or with a build. Do note however that there is no quit option 
while playing a level in a PC build.


## How to play:
PC: 
Single click an option in the menu to read it
Double click an option to select it
W to move forward
A and D to turn 90 degrees left and right respectively
F to Block Arrows

Android:
Tap a menu option to read it out loud
Double tap the option to select it

Controls are different depending on whether "Swipe movement" or "Tilted movement"  is selected on the menu. A better name for "Tilted movement" would be "Motion Gestures"

Swipe:
Swipe up to move forward
Swipe left or right to turn 90 degrees left or right respectively
Shake the device to block arrows

Motion Gestures:
Tilt device downwards to move forward.
Tilt device left or right to turn 90 degrees left or right respectively.
Shake the device to block arrows.

In the game you will be traversing three levels of a dungeon. The goal is to get to the end
and exit through a door. Stepping on a trap fires an arrow towards you, which you must block.
Some levels have enemies that walk arround and must not be touched.


## People who worked on this:

- Alba O. V. Simonsen
- Casper Agerskov
- Christian Peter Rosenberg Randleff
- Jacob Almdal Jensen
- Lucas Friborg Mitchell
- Lykke Legendre Dahl
- Maria Barbro Lanther
