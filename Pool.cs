using System;
using System.Collections.Generic;

namespace The_Wizard_s_Castle
{
    class Pool
    {
        public static string PoolEvent(ref Player player)
        {
            Random rand = new Random();
            string newRace;
            string newSex;
            string poolEvent;
            do
            {
                newRace = GameCollections.Races[rand.Next(0, GameCollections.Races.Count)].RaceName;
            } while (player.race == newRace);
            if (player.sex == "Male")
            {
                newSex = "FeMale";
            } else
            {
                newSex = "Male";
            }
            List<string> PoolEvents = new List<string>
            {
                "you feel nimbler.",
                "you feel clumsier.",
                "you feel smarter.",
                "you feel dumber.",
                "you feel stronger.",
                "you feel weaker.",
                $"you turn into a {newRace}.",
                $"you are now a {newSex} {player.race}."
            };
            poolEvent = PoolEvents[rand.Next(0, PoolEvents.Count)];
            switch (poolEvent)
            {
                case "you feel nimbler.":
                    player.IncDexterity(rand.Next(1, 3));
                    break;
                case "you feel clumsier.":
                    player.DecDexterity(rand.Next(1, 3));
                    break;
                case "you feel smarter.":
                    player.IncIntelligence(rand.Next(1, 3));
                    break;
                case "you feel dumber.":
                    player.DecIntelligence(rand.Next(1, 3));
                    break;
                case "you feel stronger.":
                    player.IncStrength(rand.Next(1, 3));
                    break;
                case "you feel weaker.":
                    player.DecStrength(rand.Next(1, 3));
                    break;
                case string caseMatch when caseMatch.Substring(0, 5) == "you t":
                    player.race = newRace;
                    break;
                case string caseMatch when caseMatch.Substring(0, 5) == "you a":
                    player.sex = newSex;
                    break;
            }
            return "\nYou drink from the pool and " + poolEvent;
        }
    }
}
