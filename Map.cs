using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle
{
    class Map
    {
        static string[,,] CreateMap(bool random)
        {
            Random rand = new Random();
            int levels, rows, columns;
            if (random)
            {
                levels = rand.Next(8, 31);
                rows = rand.Next(8, 31);
                columns = rand.Next(8, 31);
            }
            else
            {
                levels = rows = columns = 8;
            }
            string[,,] map = new string[levels, rows, columns];
            return map;
        }
        public static void DisplayMap(string[,,] map)
        {
            Console.WriteLine("Levels = {0}", map.GetLength(0));
            Console.WriteLine("Rows = {0}", map.GetLength(1));
            Console.WriteLine("Columns = {0}", map.GetLength(2));
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            System.Console.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    for (int k = 0; k < map.GetLength(2); k++)
                    {
                        Console.Write(" {0} ", map[i, j, k]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        public static void DisplayLevel(string[,,] map, Player player)
        {
            string roomValue;
            for (int j = 0; j < map.GetLength(1); j++)
            {
                for (int k = 0; k < map.GetLength(2); k++)
                {
                    if (map[player.location[0], j, k] == "Zot")
                    {
                        roomValue = "W";
                    }
                    else if (GameCollections.Monsters.Contains(map[player.location[0], j, k]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        roomValue = "M";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        roomValue = (map[player.location[0], j, k])[0].ToString();
                    }

                    if (player.location.SequenceEqual(new int[] { player.location[0], j, k }))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        roomValue = "*";
                        Console.Write(" {0} ", roomValue);
                    } else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" {0} ", roomValue);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
        }
        public static void BlankMap(string[,,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    for (int k = 0; k < map.GetLength(2); k++)
                    {
                        map[i, j, k] = "X";
                    }
                }
            }
        }

        static string[,,] PopulateMap(string[,,] map)
        {
            Random rand = new Random();
            GameCollections collections = new GameCollections();
            map[0, 0, 3] = GameCollections.RoomContents.Find(item => item == "Entrance/Exit");
            int treasuresPlaced = 0;
            int orbPlaced = 0;
            do
            {
                int i = rand.Next(0, map.GetLength(0));
                int j = rand.Next(0, map.GetLength(1));
                int k = rand.Next(0, map.GetLength(2));
                if (map[i, j, k] == null)
                {
                    if (orbPlaced == 0)
                    {
                        map[i, j, k] = "Zot"; //OrbOfZot
                        orbPlaced ++;
                    } else
                    {
                        map[i, j, k] = GameCollections.Treasures[treasuresPlaced];
                        treasuresPlaced++;
                    }
                }
            } while (treasuresPlaced < GameCollections.Treasures.Count);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    for (int k = 0; k < map.GetLength(2); k++)
                    {
                        int chance = rand.Next(0, 101);
                        if  (map[i, j, k] == null)
                        {
                            if (chance == 0 || chance < 6)
                            {
                                // 0-5: DownStairs, Sinkole or Warp
                                if (i < map.GetLength(0) - 1)
                                {
                                    chance = rand.Next(0, 3);
                                    switch (chance)
                                    {
                                        case 0:
                                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "DownStairs");
                                            map[i + 1, j, k] = GameCollections.RoomContents.Find(item => item == "UpStairs");
                                            break;
                                        case 1:
                                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "SinkHole");
                                            break;
                                        case 2:
                                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Warp");
                                            break;
                                    }
                                }
                                else
                                {
                                    map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Warp");
                                }
                            }
                            //6-10: Book
                            else if (chance > 5 && chance < 11)
                            {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Book");
                            }
                            //11-15: Chest
                            else if (chance > 10 && chance < 16)
                            {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Chest");
                            }
                            //16-20: Orb
                            else if (chance > 15 && chance < 21)
                            {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Orb");
                            }
                            //21-25: Pool
                            else if (chance > 20 && chance < 26)
                            {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Pool");
                            }
                            //26-30: Flares
                            else if (chance > 25 && chance < 31)
                            {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Pool");
                            }
                            //31-35: Gold
                            else if (chance > 30 && chance < 36)
                            {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Pool");
                            }
                            //36-45: Vendor
                            else if (chance > 35 && chance < 46)
                            {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Vendor");
                            }
                            //46-60: Monster 
                            else if (chance > 45 && chance < 61)
                            {
                                map[i, j, k] = GameCollections.Monsters[rand.Next(0, GameCollections.Monsters.Count)];
                            }
                            else
                            {
                                map[i, j, k] = "-";
                            }
                        }
                    }
                }
            }
            return map;
        }

        public static string[,,] GetMap(YWMenu gameMenu)
        {
            bool randomMap = false;
            string[] choice = gameMenu.Menu("Would you like the standard 8x8x8 map or a random map", new Dictionary<char, string>
            {
                {'S', "Standard 8x8x8 Map"},
                {'R', "Random Map"}
            }, GameCollections.ErrorMesssages);
            System.Console.Clear();
            if (choice[0] == "R")
            {
                randomMap = true;
            }
            string[,,] theMap = Map.CreateMap(randomMap);
            theMap = Map.PopulateMap(theMap);
            return theMap;
        }

        public static int[] FindOrbOfZot(string[,,] map)
        {
            int[] location = new int[3];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    for (int k = 0; k < map.GetLength(2); k++)
                    {
                        if (map[i, j, k] == "Zot")
                        {
                            location[0] = i;
                            location[1] = j;
                            location[2] = k;
                        }
                    }
                }
            }
            return location;
        }

        public static int[] FindGold(string[,,] map)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != "Gold" && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static int[] FindSinkHole(string[,,] map)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != "SinkHole" && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static int[] FindWarp(string[,,] map)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != "Warp" && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static int[] FindMonster(string[,,] map, string monster)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != monster && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static int[] FindTreasure(string[,,] map, string treasure)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != treasure && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static int[] FindChest(string[,,] map)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != "Chest" && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static int[] FindStairs(string[,,] map)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != "UpStairs" && map[location[0], location[1], location[2]] != "DownStairs" && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static int[] FindFlares(string[,,] map)
        {
            Random rand = new Random();
            int[] location = new int[3];
            int count = 0;
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
                count++;
            } while (map[location[0], location[1], location[2]] != "Flares" && count < 5001);
            if (count == 5000)
            {
                location[0] = 999;
                location[1] = 999;
                location[2] = 999;
            }
            return location;
        }

        public static void RevealRoom(string direction, int[] location, string[,,] map, ref string[,,] knownMap)
        {
            int level = location[0], row = location[1], column = location[2];
            int rowMinus = row - 1, rowPlus = row + 1, columnMinus = column - 1, columnPlus = column + 1;
            if (rowMinus < 0)
            {
                rowMinus = map.GetLength(1) - 1;
            }
            if (rowPlus == map.GetLength(1))
            {
                rowPlus = 0;
            }
            if (columnMinus < 0)
            {
                columnMinus = map.GetLength(2) - 1;
            }
            if (columnPlus == map.GetLength(2))
            {
                columnPlus = 0;
            }
            switch (direction)
            {
                case "North":
                    knownMap[level, rowMinus, column] = map[level, rowMinus, column];
                    break;
                case "South":
                    knownMap[level, rowPlus, column] = map[level, rowPlus, column];
                    break;
                case "East":
                    knownMap[level, row, columnPlus] = map[level, row, columnPlus];
                    break;
                case "West":
                    knownMap[level, row, columnMinus] = map[level, row, columnMinus];
                    break;
                default:
                    break;
            }
        }

        public static void RevealMap(int[] location, string[,,] map, ref string[,,] knownMap)
        {
            int level = location[0], row = location[1], column = location[2];
            int rowMinus = row - 1, rowPlus = row + 1, columnMinus = column - 1, columnPlus =  column + 1;
            if (rowMinus < 0)
            {
                rowMinus = map.GetLength(1) - 1;
            }
            if (rowPlus == map.GetLength(1))
            {
                rowPlus = 0;
            }
            if (columnMinus < 0)
            {
                columnMinus = map.GetLength(2) - 1;
            }
            if (columnPlus == map.GetLength(2))
            {
                columnPlus = 0;
            }
            knownMap[level, rowMinus, columnMinus] = map[level, rowMinus, columnMinus];
            knownMap[level, rowMinus, column] = map[level, rowMinus, column];
            knownMap[level, rowMinus, columnPlus] = map[level, rowMinus, columnPlus];
            knownMap[level, row, columnMinus] = map[level, row, columnMinus];
            knownMap[level, row, column] = map[level, row, column];
            knownMap[level, row, columnPlus] = map[level, row, columnPlus];
            knownMap[level, rowPlus, columnMinus] = map[level, rowPlus, columnMinus];
            knownMap[level, rowPlus, column] = map[level, rowPlus, column];
            knownMap[level, rowPlus, columnPlus] = map[level, rowPlus, columnPlus];
        }
        public static int[] ForgetMapRoom(string[,,] map)
        {
            Random rand = new Random();
            int[] location = new int[3];
            do
            {
                location[0] = rand.Next(0, map.GetLength(0));
                location[1] = rand.Next(0, map.GetLength(1));
                location[2] = rand.Next(0, map.GetLength(2));
            } while (map[location[0], location[1], location[2]] == "X");
            return location;
        }
    }
}
