using System;
using System.Collections.Generic;

namespace The_Wizard_s_Castle
{
    class Chest
    {
        public static string ChestEvent(ref Player player, ref string[,,] theMap, ref string[,,] knownMap)
        {
            Random rand = new Random();
            string chestEvent;
            string randomPotion = GameCollections.Abilities[rand.Next(0, GameCollections.Abilities.Count)];
            string randomCurse = GameCollections.Curses[rand.Next(0, GameCollections.Curses.Count)];
            string randomDirection = GameCollections.Directions[rand.Next(0, GameCollections.Directions.Count)];
            int randomGold = rand.Next(2, 1001);
            int[] randomMapLocation = new int[3] { rand.Next(0, theMap.GetLength(0)), rand.Next(0, theMap.GetLength(1)), rand.Next(0, theMap.GetLength(2)) };
            int randomDamage = rand.Next(2, 10);
            theMap[player.location[0], player.location[1], player.location[2]] = "-";
            knownMap[player.location[0], player.location[1], player.location[2]] = "-";
            List<string> ChestEvents = new List<string>
            {
                $"there's {randomGold} Gold Pieces inside.",
                "Gas! You stagger from the room!",
                "Kaboom! It Explodes!",
                $"you find a potion of {randomPotion}.",
                $"a wizard jumps out and puts the curse of {randomCurse} on you and runs out of the room!",
                $"you find a piece of a map revealing the area around ({randomMapLocation[0] + 1}, {randomMapLocation[1] + 1}, {randomMapLocation[2] + 1})."
            };
            chestEvent = ChestEvents[rand.Next(0, ChestEvents.Count)];
            switch (chestEvent)
            {
                case string caseMatch when caseMatch.Substring(0, 5) == "there":
                    player.gold += randomGold;
                    break;
                case "Gas! You stagger from the room!":
                    switch (randomDirection)
                    {
                        case "North":
                            player.North(theMap);
                            break;
                        case "South":
                            player.South(theMap);
                            break;
                        case "East":
                            player.East(theMap);
                            break;
                        case "West":
                            player.West(theMap);
                            break;
                    }
                    break;
                case "Kaboom! It Explodes!":
                    switch (player.armor)
                    {
                        case "Leather":
                            randomDamage -= 2;
                            break;
                        case "ChainMail":
                            randomDamage -= 3;
                            break;
                        case "Plate":
                            randomDamage -= 4;
                            break;
                        default:
                            break;
                    }
                    if (randomDamage > 0)
                    {
                        player.strength -= randomDamage;
                    }
                    break;
                case string caseMatch when caseMatch.Substring(0, 17) == "you find a potion":
                    int randomInc = rand.Next(1, 5);
                    switch (randomPotion)
                    {
                        case "Dexterity":
                            player.IncDexterity(randomInc);
                            break;
                        case "Intelligence":
                            player.IncIntelligence(randomInc);
                            break;
                        case "Strength":
                            player.IncStrength(randomInc);
                            break;
                    }
                    break;
                case string caseMatch when caseMatch.Substring(0, 8) == "a wizard":
                    switch (randomCurse)
                    {
                        case "Forgetfulness":
                            player.forgetfulness = true;
                            break;
                        case "Leech":
                            player.leech = true;
                            break;
                        case "Lethargy":
                            player.lethargy = true;
                            break;
                    }
                    break;
                case string caseMatch when caseMatch.Substring(0, 25) == "you find a piece of a map":
                    Map.RevealMap(randomMapLocation, theMap, ref knownMap);
                    break;
                default:
                    break;
            }
            return "\nYou open the chest and " + chestEvent;
        }
    }
}

