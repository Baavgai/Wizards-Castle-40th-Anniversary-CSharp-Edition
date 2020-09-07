using System;
using System.Collections.Generic;

namespace The_Wizard_s_Castle
{
    class Orb
    {
        public static string OrbEvent(ref Player player, ref string[,,] theMap)
        {
            Random rand = new Random();
            string orbEvent;
            int[] goldLocation = Map.FindGold(theMap);
            int[] flaresLocation = Map.FindFlares(theMap);
            int[] stairsLocation = Map.FindStairs(theMap);
            int[] chestLocation = Map.FindChest(theMap);
            int[] sinkHoleLocation = Map.FindSinkHole(theMap);
            int[] warpLocation = Map.FindWarp(theMap);
            int[] orbOfZotLocation = Map.FindOrbOfZot(theMap);
            if (rand.Next(0,2) > 0)
            {
                orbOfZotLocation[0] = rand.Next(0, theMap.GetLength(0));
                orbOfZotLocation[1] = rand.Next(0, theMap.GetLength(1));
                orbOfZotLocation[2] = rand.Next(0, theMap.GetLength(2));
            }
            string randomMonster = GameCollections.Monsters[rand.Next(0, GameCollections.Monsters.Count)];
            string randomTreasure = GameCollections.Treasures[rand.Next(0, GameCollections.Treasures.Count)];
            int[] monsterLocation = Map.FindMonster(theMap, randomMonster);
            int[] treasureLocation = Map.FindTreasure(theMap, randomTreasure);
            List<string> classes = new List<string> { "fencing", "religion", "language", "alchemy" };
            List<string> OrbEvents = new List<string>
            {
                "yourself in a bloody heap.",
                "your mother telling you to clean your room.",
                "a soap opera re-run.",
                "yourself playing The Wizard's Castle.",
                "your life drifting before your eyes.",
                $"yourself in {classes[new Random().Next(0, classes.Count)]} class.",
                $"a {GameCollections.Monsters[new Random().Next(0, GameCollections.Monsters.Count)]} gazing back at you.",
                $"a {GameCollections.Monsters[new Random().Next(0, GameCollections.Monsters.Count)]} eating the flesh from your corpse.",
                $"a {GameCollections.Monsters[new Random().Next(0, GameCollections.Monsters.Count)]} using your leg-bone as a tooth-pick.",
                $"yourself drinking from a pool and becomine a {GameCollections.Races[new Random().Next(0, GameCollections.Races.Count)].RaceName}.",
                $"a pile of gold at ({goldLocation[0] + 1}, {goldLocation[1] + 1}, {goldLocation[2] + 1}).",
                $"a chest at ({chestLocation[0] + 1}, {chestLocation[1] + 1}, {chestLocation[2] + 1}).",
                $"a sinkhole at ({sinkHoleLocation[0] + 1}, {sinkHoleLocation[1] + 1}, {sinkHoleLocation[2] + 1}).",
                $"a warp at ({warpLocation[0] + 1}, {warpLocation[1] + 1}, {warpLocation[2] + 1}).",
                $"flares at ({flaresLocation[0] + 1}, {flaresLocation[1] + 1}, {flaresLocation[2] + 1}).",
                $"{randomTreasure} at ({treasureLocation[0] + 1}, {treasureLocation[1] + 1}, {treasureLocation[2] + 1}).",
                $"{randomMonster} at ({monsterLocation[0] + 1}, {monsterLocation[1] + 1}, {monsterLocation[2] + 1}).",
                $"{theMap[stairsLocation[0], stairsLocation[1], stairsLocation[2]]} at ({stairsLocation[0] + 1}, {stairsLocation[1] + 1}, {stairsLocation[2] + 1}).",
                $"a large mug of ale at ({rand.Next(0, theMap.GetLength(0)) + 1}, {rand.Next(0, theMap.GetLength(1)) + 1}, {rand.Next(0, theMap.GetLength(2)) + 1}) and you feel VERY thristy.",
                $"THE ORB OF ZOT AT ({orbOfZotLocation[0] + 1}, {orbOfZotLocation[1] + 1}, {orbOfZotLocation[2] + 1})!"
            };
            do
            {
                orbEvent = OrbEvents[rand.Next(0, OrbEvents.Count)];
            } while (orbEvent.Contains("999"));
            if (orbEvent == "yourself in a bloody heap.")
            {
                player.DecStrength(new Random().Next(1, 3));
            }
            return "\nYou gaze into the Orb and see " + orbEvent;
        }
    }
}
