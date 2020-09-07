using System;
using System.Linq;
using System.Threading;
using YWMenuNS;

namespace The_Wizard_s_Castle
{
    class RoomEvents
    {
        public static bool GetRoomEvent(ref Player player, ref string[,,] theMap)
        {
            Random rand = new Random();
            Console.WriteLine($"You are at Level {player.location[0] + 1} Row {player.location[1] + 1} Column {player.location[2] + 1}");
            Console.WriteLine($"\nHere you find '{theMap[player.location[0], player.location[1], player.location[2]]}'");
            switch (theMap[player.location[0], player.location[1], player.location[2]])
            {
                case "Flares":
                    int flaresFound = rand.Next(1, 11);
                    Console.WriteLine("You've found {0} flares", flaresFound);
                    player.flares += flaresFound;
                    theMap[player.location[0], player.location[1], player.location[2]] = "-";
                    break;

                case "Gold":
                    int goldFound = rand.Next(1, 1001);
                    Console.WriteLine("You've found {0} Gold Pieces", goldFound);
                    player.gold += goldFound;
                    theMap[player.location[0], player.location[1], player.location[2]] = "-";
                    break;

                case string _ when GameCollections.Monsters.Contains(theMap[player.location[0], player.location[1], player.location[2]]):
                    Monster monster = Monster.GetOrCreateMonster(theMap, player, string.Join(string.Empty, new[] { player.location[0], player.location[1], player.location[2] }));
                    if (monster.mad)
                    {
                        Console.WriteLine($"\n{Monster.MonsterMadMessage(monster)}\n");
                        Battle.BattleSequence(ref player, ref monster, theMap);
                    }
                    else
                    {
                        Console.WriteLine($"\nThe {monster.race} doesn't seem to notice you.\n");
                    }
                    if (monster.strength < 1) {
                        theMap[player.location[0], player.location[1], player.location[2]] = "-"; 
                    } 
                    if (! player.location.SequenceEqual(monster.location))
                    {
                        return true;
                    }
                    //*** Testing *** Console.WriteLine($"race={monster.race}, dexerity={monster.dexterity}, intelligence={monster.intelligence}, strength={monster.strength}, mad={monster.mad}, runeStaff={monster.runeStaff}, treasures={string.Join("+", player.treasures)}");
                    break;

                case "SinkHole":
                    player.Sink();
                    for (int i = 0; i < 30; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(100);
                    }
                    return true;

                case string caseMatch when GameCollections.Treasures.Contains(theMap[player.location[0], player.location[1], player.location[2]]):
                    Console.WriteLine("You've found the {0}, it's yours!", caseMatch);
                    player.treasures.Add(caseMatch);
                    theMap[player.location[0], player.location[1], player.location[2]] = "-";
                    break;

                case "Warp":
                    player.Warp(theMap);
                    for (int i = 0; i < 30; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(100);
                    }
                    return true;

                case "Zot":
                    if (!(YWMenu.playerDirection.ToString().ToUpper() == "T"))
                    {
                        switch (YWMenu.playerDirection.ToString().ToUpper())
                        {
                            case "N":
                                player.North(theMap);
                                return true;
                            case "S":
                                player.South(theMap);
                                return true;
                            case "E":
                                player.East(theMap);
                                return true;
                            case "W":
                                player.West(theMap);
                                return true;
                        }
                        return true;
                    }
                    else
                    {
                        Console.Write("\n\t\t");
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("YOU'VE FOUND THE ORB OF ZOT");
                        Thread.Sleep(1000);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("\n");
                        player.orbOfZot = true;
                        theMap[player.location[0], player.location[1], player.location[2]] = "-";
                        break;
                    }
            }
            return false;
        }
    }
}
