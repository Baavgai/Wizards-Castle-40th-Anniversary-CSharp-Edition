# Wizard's Castle (40th Anniversary C# Edition)
> A remake of the 1980 Exidy Sorcerer game written by JOSEPH R. POWER  
> The instructions are included with the game.  
> I stayed mostly true to the original, but added some additional functionality.  

## Table of contents
* [General info](#general-info)
* [Screenshots](#screenshots)
* [YouTube Videos](#youtube-videos)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [To-do-list](To-do-list)
* [Status](#status)
* [Inspiration](#inspiration)
* [Contact](#contact)

## General info
A full C# remake of the 1980 classic, the Wizard's Castle.  
In addition I have added some new elements to the game.  
I deeply enjoyed this game growing up and wanted to make it for a modern programming language.  
This project has been a the dedication of many of my evenings, nights and weekends.  
Exidy Sorcerer game code originally published in the July 1980 edition of Recreational Computing:  
* https://archive.org/details/1980-07-recreational-computing

## Screenshots
![ScreenShot1 screenshot](./images/ScreenShot1.jpg)
![ScreenShot2 screenshot](./images/ScreenShot2.jpg)

## YouTube Videos
* https://www.youtube.com/watch?v=wMHVUTNWXXc&list=PLd5LdvEbbj5SdOHW24EfNK57g9FkS9E_l

## Technologies
* C# Console Application
* .NET Framework 4.8
* .NET Core 3.1
* Microsoft Visual Studio Community 2019

## Setup
* Clone the repository:  
git clone "https://github.com/yourwishismine/Wizards-Castle-40th-Anniversary-CSharp-Edition.git"  
* open Visual Studio 2019 and build it, or you can download the .NET Core 3.1 SDK:  
https://dotnet.microsoft.com/download  
* Compile with the .NET Core 3.1 SDK:  
dotnet publish --runtime win-x64 --configuration Release /p:PublishSingleFile=true /p:PublishTrimmed=true

## Features
Game is fully playable.
* Added support for Random maps
* Added additional game messages
* Added non-interactive elements to empty rooms

## To-do-list
* Fix Orb gaze doesn't always give correct location of some items (Note: Orb of Zot location isn't always supposed to be correct)
* Add ability to attack Vendors
* Add ability to attack non-mad Monsters
* Fix any other bugs that become known
* Add addtional monsters, spells and curses
* Other possibilities...

## Status
Project is: _finished_

## Inspiration
Project based on the 1980 Exidy Sorcerer game written by JOSEPH R. POWER

## Contact
Created by Daniel Kill [@yourwishismine](https://twitter.com/yourwishismine)
