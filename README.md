[![Discord Shield](https://discord.com/api/guilds/1336392386024177786/widget.png?style=shield)](https://discord.gg/JtyuP26w95)

# Pantheon Addons
This project is an **experimental, third party, unofficial addon API** for Pantheon: Rise of the Fallen. Its primary purpose is to create addons in the game via a readonly API which alter or create user interface objects. It is designed to blend in with the overall design of the native user interface, enhancing the game without breaking immersion.

## Installation
Install MelonLoader, following along with their [installation instructions](https://melonwiki.xyz/#/?id=requirements)
When selecting a version to install, tick `Enable Nightly builds` and install the latest nightly build (0.7.1-ci.2207 at time of writing)
Once you're finished, run the game once as normal to allow MelonLoader to generate the required libraries. Once this is done, close the game.

This repository contains 3 projects. Once the solution is built, the following DLLs will be present in the build folder:
* `PantheonAddonLoader.dll`, which goes in the `/mods` directory in your game installation.
* `PantheonAddonFramework.dll`, which goes in the `/userlibs` directory.
* Optionally `PantheonAddons.dll`, which goes in `%APPDATA%\PantheonAddons`. This contains example addons.

## Developing addons
Create a new C# library project targeting **.NET 6**
In your project, reference the PantheonAddonFramework library from this repository.
Once you've built your project, place it in `%APPDATA\PantheonAddons`. The loader will automatically find your addon class and load it to the game.

Code snippets/wiki to follow...

## Disclaimer regarding cheating/anticheat
We believe that these addons do not violate the terms and conditions of Pantheon: Rise of the Fallen. From the EULA (as of 7th Jan 2025), we may not:

(b) use cheats, exploits, automation software, bots, hacks, mods or any unauthorized third-party software,
code or other device designed to modify or interfere with the Game or Service, or without Visionary Realms’
express written consent, modify or cause to be modified any files that are a part of the Game or Service;

This framework does not, and will never:

* Automate any gameplay actions
* Interfere with or change any gameplay
* Draw anything on the screen outside of changes to the user interface
* Modify any files, everything is applied at runtime

However, this framework and example addons are to be used at your own risk. We do not accept any liability or responsibility for any actions taken against your account for using these mods.
