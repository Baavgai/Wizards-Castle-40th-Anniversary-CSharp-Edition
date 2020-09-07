using System;
using System.Collections.Generic;

namespace The_Wizard_s_Castle
{
    class Book
    {
        public static string BookEvent(ref Player player, ref string[,,] theMap)
        {
            Random rand = new Random();
            string bookEvent;
            string randomMonster = GameCollections.Monsters[rand.Next(0, GameCollections.Monsters.Count)];
            string randomRace = GameCollections.Races[rand.Next(0, GameCollections.Races.Count)].RaceName;
            int[] location = Map.FindGold(theMap);
            List<string> BookEvents = new List<string>
            {
                "it's another volume of Zot's poetry. Yeech!",
                $"it's an old copy of play {randomRace}.",
                $"it's a {randomMonster} cook book.",
                $"it's a self-improvement book on how to be a better {randomRace}.",
                "it's a manual of dexterity!",
                "it's a manual of intelligence!",
                "it's a manual of strength!",
                $"it's a treasure map leading to a pile of gold at ({location[0] + 1}, {location[1] + 1}, {location[2] + 1}).",
                $"FLASH! OH NO! YOU ARE NOW A BLIND {player.race}!",
                $"it sticks to your hands. Now you can't grab your {player.weapon}!"
            };
            bookEvent = BookEvents[rand.Next(0, BookEvents.Count)];
            switch (bookEvent)
            {
                case "it's a manual of dexterity!":
                    player.dexterity = player.maxAttrib;
                    break;
                case "it's a manual of intelligence!":
                    player.intelligence = player.maxAttrib;
                    break;
                case "it's a manual of strength!":
                    player.strength = player.maxAttrib;
                    break;
                case string caseMatch when caseMatch.Substring(0, 5) == "FLASH":
                    player.blind = true;
                    break;
                case string caseMatch when caseMatch.Substring(0, 9) == "it sticks":
                    player.bookStuck = true;
                    break;
                default:
                    break;
            }
            theMap[player.location[0], player.location[1], player.location[2]] = "-";
            return "\nYou open the book and " + bookEvent;
        }
    }
}
