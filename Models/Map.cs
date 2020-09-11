using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    class Map {
        public class Cell {
            public Item Contents { get; set; }
            public bool Known { get; set; }

            public void Clear() => Contents = null;
            public bool IsEmpty => Contents == null;
        }

        private readonly Cell[,,] state;
        public Map(bool random) {
            if (random) {
                Levels = Util.RandInt(8, 31);
                Rows = Util.RandInt(8, 31);
                Cols = Util.RandInt(8, 31);
            } else {
                Levels = Rows = Cols = 8;
            }
            state = new Cell[Levels, Rows, Cols];
        }

        public int Levels { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public Cell this[MapPos p] {
            get => state[p.Level, p.Row, p.Col];
            set => state[p.Level, p.Row, p.Col] = value;
        }

        public Cell this[int level, int row, int col] {
            get => state[level, row, col];
            set => state[level, row, col] = value;
        }


        public void Traverse(Action<Map, MapPos> action) {
            var p = new MapPos();
            for (p.Level = 0; p.Level < Levels; p.Level++) {
                for (p.Row = 0; p.Row < Rows; p.Row++) {
                    for (p.Col = 0; p.Col < Cols; p.Col++) {
                        action(this, p);
                    }
                }
            }
        }

        public void DisplayMap() {
            Util.WriteLine($"Levels = {Levels}");
            Util.WriteLine($"Rows = {Rows}");
            Util.WriteLine($"Columns = {Cols}");
            // Console.WriteLine("Press ENTER to continue.");            Console.ReadLine();
            // System.Console.Clear();
            for (int i = 0; i < Levels; i++) {
                for (int j = 0; j < Rows; j++) {
                    for (int k = 0; k < Cols; k++) {
                        Util.Write($" {state[i, j, k]} ");
                    }
                    Util.WriteLine();
                }
                Util.WriteLine();
            }
        }

        public MapPos RandPos() => new MapPos() {
            Level = Util.RandInt(Levels),
            Row = Util.RandInt(Rows),
            Col = Util.RandInt(Cols),
        };

        public MapPos FindGold() => null;

        public void Clear() => Traverse((_, p) => this[p].Clear());

        public Map Populate() {
            // GameCollections collections = new GameCollections();
            this[0, 0, 3] = GameCollections.RoomContents.Find(item => item == "Entrance/Exit");
            int treasuresPlaced = 0;
            int orbPlaced = 0;
            while (treasuresPlaced < GameCollections.Treasures.Count) {
                var p = RandPos();
                if (this[p] == null) {
                    if (orbPlaced == 0) {
                        this[p] = "Zot"; //OrbOfZot
                        orbPlaced++;
                    } else {
                        this[p] = GameCollections.Treasures[treasuresPlaced];
                        treasuresPlaced++;
                    }
                }
            }
            Traverse((map, p) => {
                int chance = Util.RandInt(101);
                if (map[p] == null) {
                    if (chance < 6) {
                        // 0-5: DownStairs, Sinkole or Warp
                        if (p.Level < map.Levels - 1) {
                            switch (Util.RandInt(3)) {
                                case 0:
                                    map[p] = GameCollections.RoomContents.Find(item => item == "DownStairs");
                                    map[p.Level + 1, p.Row, p.Col] = GameCollections.RoomContents.Find(item => item == "UpStairs");
                                    break;
                                case 1:
                                    map[p] = GameCollections.RoomContents.Find(item => item == "SinkHole");
                                    break;
                                case 2:
                                    map[p] = GameCollections.RoomContents.Find(item => item == "Warp");
                                    break;
                            }
                        } else {
                            map[p] = GameCollections.RoomContents.Find(item => item == "Warp");
                        }
                    } else if (chance < 11) {
                        //6-10: Book
                        map[p] = GameCollections.RoomContents.Find(item => item == "Book");
                    } else if (chance < 16) {
                        //11-15: Chest
                        map[p] = GameCollections.RoomContents.Find(item => item == "Chest");
                    } else if (chance < 21) {
                        //16-20: Orb
                        map[p] = GameCollections.RoomContents.Find(item => item == "Orb");
                    } else if (chance < 26) {
                        //21-25: Pool
                        map[p] = GameCollections.RoomContents.Find(item => item == "Pool");
                    } else if (chance < 31) {
                        //26-30: Flares
                        map[p] = GameCollections.RoomContents.Find(item => item == "Pool");
                    } else if (chance < 36) {
                        //31-35: Gold
                        map[p] = GameCollections.RoomContents.Find(item => item == "Pool");
                    } else if (chance < 46) {
                        //36-45: Vendor
                        map[p] = GameCollections.RoomContents.Find(item => item == "Vendor");
                    } else if (chance < 61) {
                        //46-60: Monster 
                        map[p] = Util.RandPick(GameCollections.Monsters);
                    } else {
                        map[p] = "-";
                    }
                }
            });
            return this;
        }
    }

    public static void DisplayLevel(string[,,] map, Player player) {
        string roomValue;
        for (int j = 0; j < map.GetLength(1); j++) {
            for (int k = 0; k < map.GetLength(2); k++) {
                if (map[player.location[0], j, k] == "Zot") {
                    roomValue = "W";
                } else if (GameCollections.Monsters.Contains(map[player.location[0], j, k])) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    roomValue = "M";
                } else {
                    Console.ForegroundColor = ConsoleColor.White;
                    roomValue = (map[player.location[0], j, k])[0].ToString();
                }
                if (player.location.SequenceEqual(new int[] { player.location[0], j, k })) {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    roomValue = "*";
                    Console.Write(" {0} ", roomValue);
                } else {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" {0} ", roomValue);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }
    }

    static string[,,] PopulateMap(string[,,] map) {
        Random rand = new Random();
        GameCollections collections = new GameCollections();
        map[0, 0, 3] = GameCollections.RoomContents.Find(item => item == "Entrance/Exit");
        int treasuresPlaced = 0;
        int orbPlaced = 0;
        do {
            int i = rand.Next(0, map.GetLength(0));
            int j = rand.Next(0, map.GetLength(1));
            int k = rand.Next(0, map.GetLength(2));
            if (map[i, j, k] == null) {
                if (orbPlaced == 0) {
                    map[i, j, k] = "Zot"; //OrbOfZot
                    orbPlaced++;
                } else {
                    map[i, j, k] = GameCollections.Treasures[treasuresPlaced];
                    treasuresPlaced++;
                }
            }
        } while (treasuresPlaced < GameCollections.Treasures.Count);
        for (int i = 0; i < map.GetLength(0); i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                for (int k = 0; k < map.GetLength(2); k++) {
                    int chance = rand.Next(0, 101);
                    if (map[i, j, k] == null) {
                        if (chance == 0 || chance < 6) {
                            // 0-5: DownStairs, Sinkole or Warp
                            if (i < map.GetLength(0) - 1) {
                                chance = rand.Next(0, 3);
                                switch (chance) {
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
                            } else {
                                map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Warp");
                            }
                        }
                        //6-10: Book
                        else if (chance > 5 && chance < 11) {
                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Book");
                        }
                        //11-15: Chest
                        else if (chance > 10 && chance < 16) {
                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Chest");
                        }
                        //16-20: Orb
                        else if (chance > 15 && chance < 21) {
                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Orb");
                        }
                        //21-25: Pool
                        else if (chance > 20 && chance < 26) {
                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Pool");
                        }
                        //26-30: Flares
                        else if (chance > 25 && chance < 31) {
                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Pool");
                        }
                        //31-35: Gold
                        else if (chance > 30 && chance < 36) {
                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Pool");
                        }
                        //36-45: Vendor
                        else if (chance > 35 && chance < 46) {
                            map[i, j, k] = GameCollections.RoomContents.Find(item => item == "Vendor");
                        }
                        //46-60: Monster 
                        else if (chance > 45 && chance < 61) {
                            map[i, j, k] = GameCollections.Monsters[rand.Next(0, GameCollections.Monsters.Count)];
                        } else {
                            map[i, j, k] = "-";
                        }
                    }
                }
            }
        }
        return map;
    }

    public static string[,,] GetMap(YWMenu gameMenu) {
        bool randomMap = false;
        string[] choice = gameMenu.Menu("Would you like the standard 8x8x8 map or a random map", new Dictionary<char, string>
        {
                {'S', "Standard 8x8x8 Map"},
                {'R', "Random Map"}
            }, GameCollections.ErrorMesssages);
        System.Console.Clear();
        if (choice[0] == "R") {
            randomMap = true;
        }
        string[,,] theMap = Map.CreateMap(randomMap);
        theMap = Map.PopulateMap(theMap);
        return theMap;
    }

    public static int[] FindOrbOfZot(string[,,] map) {
        int[] location = new int[3];
        for (int i = 0; i < map.GetLength(0); i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                for (int k = 0; k < map.GetLength(2); k++) {
                    if (map[i, j, k] == "Zot") {
                        location[0] = i;
                        location[1] = j;
                        location[2] = k;
                    }
                }
            }
        }
        return location;
    }

    public static int[] FindGold(string[,,] map) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != "Gold" && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static int[] FindSinkHole(string[,,] map) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != "SinkHole" && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static int[] FindWarp(string[,,] map) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != "Warp" && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static int[] FindMonster(string[,,] map, string monster) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != monster && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static int[] FindTreasure(string[,,] map, string treasure) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != treasure && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static int[] FindChest(string[,,] map) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != "Chest" && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static int[] FindStairs(string[,,] map) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != "UpStairs" && map[location[0], location[1], location[2]] != "DownStairs" && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static int[] FindFlares(string[,,] map) {
        Random rand = new Random();
        int[] location = new int[3];
        int count = 0;
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
            count++;
        } while (map[location[0], location[1], location[2]] != "Flares" && count < 5001);
        if (count == 5000) {
            location[0] = 999;
            location[1] = 999;
            location[2] = 999;
        }
        return location;
    }

    public static void RevealRoom(string direction, int[] location, string[,,] map, ref string[,,] knownMap) {
        int level = location[0], row = location[1], column = location[2];
        int rowMinus = row - 1, rowPlus = row + 1, columnMinus = column - 1, columnPlus = column + 1;
        if (rowMinus < 0) {
            rowMinus = map.GetLength(1) - 1;
        }
        if (rowPlus == map.GetLength(1)) {
            rowPlus = 0;
        }
        if (columnMinus < 0) {
            columnMinus = map.GetLength(2) - 1;
        }
        if (columnPlus == map.GetLength(2)) {
            columnPlus = 0;
        }
        switch (direction) {
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

    public static void RevealMap(int[] location, string[,,] map, ref string[,,] knownMap) {
        int level = location[0], row = location[1], column = location[2];
        int rowMinus = row - 1, rowPlus = row + 1, columnMinus = column - 1, columnPlus = column + 1;
        if (rowMinus < 0) {
            rowMinus = map.GetLength(1) - 1;
        }
        if (rowPlus == map.GetLength(1)) {
            rowPlus = 0;
        }
        if (columnMinus < 0) {
            columnMinus = map.GetLength(2) - 1;
        }
        if (columnPlus == map.GetLength(2)) {
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
    public static int[] ForgetMapRoom(string[,,] map) {
        Random rand = new Random();
        int[] location = new int[3];
        do {
            location[0] = rand.Next(0, map.GetLength(0));
            location[1] = rand.Next(0, map.GetLength(1));
            location[2] = rand.Next(0, map.GetLength(2));
        } while (map[location[0], location[1], location[2]] == "X");
        return location;
    }
}
}
