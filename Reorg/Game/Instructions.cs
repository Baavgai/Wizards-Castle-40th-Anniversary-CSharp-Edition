﻿using System;
using System.Collections.Generic;


namespace WizardCastle {
    internal static partial class Game {

        private static readonly string[] InstructionLines = {
            "THE STORY",
            "Many years ago, in the kingdom of N'dic, the gnomic wizard Zot forged his great Orb of power (known as The Orb Of Zot).",
            "Soon after this Zot vanished.",
            "The wizard left behind his vast sub-terranean castle filled with esurient monsters, fabulous treasures, and the incredible Orb Of Zot.",
            "From that time on many a bold adventurer has entered into the Zot's castle to find the fabulous Orb Of Zot.",
            "Many have either died in the castle or left the castle discouraged with their failed attempt.",
            "You now journey forth in your quest of the Orb with anticipation of great adventure, awesome treasure, and much reward.",
            "",
            "INTRODUCTION",
            "Wizard's Castle, aka The Orb of Zot, is a classic text-only Role-Playing-Game.",
            "It is a computerized simulation of the lone adventurer's quest within an immense underground labyrinth.",
            "The game is turn based and takes place in dungeon that is randomly stocked with monsters, treasures, and various other items.",
            "Each game is separate from all previous games, so the game is a still a fun challenge even after you have won several times.",
            "Each game will result in a win or a loss, depending on a player's skill and luck.",
            "The following instructions explain the rules and options of the game.",
            "If at any time, however, you are not sure of what to do - experiment.",
            "The program is designed to prevent invalid inputs.",
            "",
            "CHARACTER CREATION",
            "At the start of each game you will be asked a few questions:",
            "Race - each Race starts with a total of 32 points (except Hobbits, who get 28) but they are distributed differently for each race.",
            "Sex - You may be male or female (note: both sexes are equal in ability and number of points).",
            "Attributes - Strength (ST), Intelligence (IQ), and Dexterity (DX)",
            "Your ST, IQ, and DX may be any number from 1 to 18. If any of the three goes below 1, you have died.",
            "Your character also starts the game with 60 gold pieces (GPs) with which to purchase some items before you begin your quest.",
            "Armor - You can wear only one suit of armor at a time. The more expensive the armor, the more damage it will absorb.",
            "Weapons - You may carry only one weapon at a time. The more expensive the weapon, the more damage it will do in battle.",
            "Lamp - You may buy a lamp.",
            "Flares - You may buy flares at 1 GP a piece.",
            "Once you have created and equipped your character, you are ready to enter the castle and begin the game.",
            "",
            "THE CASTLE",
            "Each level of the castle is constructed like a donut in that the north edge is connected to the south edge and the east edge is connected to the west edge.",
            "Going East from the farthest East edge of the map will take you to the farthest West edge of the map.",
            "Going West from the farthest West edge of the map will take you to the farthest East edge of the map.",
            "Going North from the farthest North edge of the map will take you to the farthest South edge of the map.",
            "Going South from the farthest South edge of the map will take you to the farthest North edge of the map.",
            "The ONLY room that does not work in this manner is the ENTRANCE which is always at (1, 4) Level 1).",
            "Going North from the ENTRANCE takes you out of the castle and ends the game.",
            "Single letters on the map represent abbreviations for the room contents.",
            "The exception is your location on the map which is always bracketed by < >.",
            "  Each room in the castle will have as contents one of the following:",
            "•	E - the entrance/exit of the castle, always located at: (1,4) Level 1",
            "•	X - an area of the map that is un-known to you",
            "•	- - an empty room",
            "•	U - stairs going up to the level above",
            "•	D - stairs going down to the level below",
            "•	P - a magic pool you can drink from",
            "•	C - a chest which may be opened",
            "•	B - a book which may be opened",
            "•	G - from 1 to 1000 gold pieces",
            "•	O - a crystal orb which may be gazed into",
            "•	S - a sinkhole (a room with no floor) which causes you to fall to the same Row/Column of the Level below",
            "•	T - one of the multiple treasures in the game",
            "•	F - from 1 to 10 flares",
            "•	W - a warp which warps you to a random Level/Row/Column",
            "•	M - one of the multiple monsters in the game",
            "•	V - a vendor",
            "",
            "STANDARD PLAYER COMMANDS",
            "•	(M)AP causes a map of the level you are currently on to be printed.",
            "•	(O)PEN causes you to open the book or chest in the room you are in. This command will only work if you are in a room with a chest or book.",
            "•	(P) POOL drink causes you to take a drink from a magic pool. You may repeat this command as often as you wish, but you must be in a room with a magic pool.",
            "•	(T)ELEPORT allows you to teleport directly to a room. This is the only way to enter the room containing the Orb of Zot. You must have the Runestaff to teleport.",
            "•	(U)P causes you to ascend stairs going up (you must be in a room with stairs going up).",
            "•	(D)OWN causes you to descend stairs going down (you must be in a room with stairs going down).",
            "•	(N)ORTH moves you to the room north of your present position. WHEN YOU GO NORTH FROM THE ENTRANCE THE GAME ENDS (In all other cases the north edge wraps to the south).",
            "•	(S)OUTH moves you to the room south of your present position (In all cases the south edge wraps to the north edge).",
            "•	(E)AST moves you to the room east of your present position (In all cases the east edge wraps to the west edge).",
            "•	(W)EST moves you to the room west of your present position (In all cases the west edge wraps to the east edge).",
            "•	(G)AZE causes you to gaze into a crystal orb and see things.",
            "	Note: When you see the location of the Orb of Zot, there is only a 50% chance that the location is correct.",
            "•	(F)LARE causes one of your flares to be lit, revealing the contents of all the rooms around your current position.",
            "•	(L)AMP will shine into any one of the rooms north, south, east, or west of your current position, revealing that room's contents.",
            "	Note: Your LAMP has limited uses and you must purchase more LAMP oil from a vendor to get more uses.",
            "•	(Q)UIT allows you to end the game while still in the castle. If you quit, you will lose the game.",
            "•	(A)TTACK monster or vendor.",
            "•	(R)ETREAT from battle.",
            "•	(B)RIBE a monster or a angry vendor.",
            "•	(?) Get Help.",
            "•	* You will have some other options when you are in a battle, trading with a vendor, and certain other situations *",
            "",
            "CURSES",
            "•	Lethargy - this gives the monsters the first attack which prevents you from bribing them or casting spells on them.",
            "•	Leech - this takes from 1 to 2 Strength points from you each turn until you have no more and you die.",
            "•	Forgetfulness - this causes you to forget what you know about the castle. Your map slowly returns to all X characters, however, the room contents remain the same.",
            "•	* Possible other curses *",
            "",
            "MAGIC SPELLS",
            "When your Intelligence (IQ) becomes 15 or higher, you can cast a magic spell on a monster if you have the first combat option.",
            "The three spells and their effects are:",
            "•	Web - traps the monster in a web so it cannot fight back (lasts between 2 and 9 turns and costs you 1 Strength (ST) point.",
            "•	Fireball - hits the monster with a ball of flame that causes between 2 and 14 points of damage. It costs 1 ST point and 1 IQ point.",
            "•	Deathspell is a contest of wills between the monster and yourself. Whoever has the lower IQ dies at once.",
            "	Note: It costs nothing to use but is very risky as even with an IQ of 18 (the highest possible), you have a 25% chance of losing.",
            "•	* Possible other spells *",
            "",
            "TREASURES, CURSES, BLINDNESS, AND SUCH",
            "In the castle are mulitple randomly placed treasures:",
            "•	The Ruby Red - wards off the curse of Lethargy.",
            "•	The Norn Stone - has no special power.",
            "•	The Pale Pearl - wards off the curse of the Leech.",
            "•	The Opal Eye - cures blindness.",
            "•	The Green Gem - wards off the curse of Forgetfulness.",
            "•	The Blue Flame - dissolves books stuck to your hands.",
            "•	The Palantir - has no special power.",
            "•	The Silmaril - has no special power.",
            "•	* Possible other treasures *",
            "",
            "VENDORS",
            "On every level in the castle there are vendors who are more than willing to sell you various items at grossly inflated prices.",
            "If you choose to attack a vendor, you will antagonize every vendor in the castle and they will all then react like a monster when you encounter one.",
            "However, killing a vendor will give you a new set of plate armor, a sword, a new lamp, and his hoard of gold.",
            "To end hostilities and reestablish trade capabilities (returning all vendors to normal), you must bribe any vendor with the treasure of his choice.",
            "",
            "MONSTERS",
            "There are multiple types of monsters in the castle:",
            "Each monster has various attributes depending on the type of monster.",
            "Each monster possesses a hoard of gold from 1 to 1000 pieces which you get when you kill it.",
            "In addition, one of the monsters is carrying the Runestaff (you will not know which one until you kill it).",
            "",
            "WINNING THE GAME",
            "To win the game, you must Exit the castle through the ENTRANCE with the Orb Of Zot.",
            "*** END OF INSTRUCTIONS ***"
        };

        public static void ShowInstructions(IView view) => view.WriteLines(InstructionLines);


    }
}
