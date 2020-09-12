using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    class Map {
        public class Cell {
            public IHasOnEntry Contents { get; set; }
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
            Populate();
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

        public void TraverseLevel(int level, Action<Map, MapPos> action) {
            var p = new MapPos() { Level = level };
            for (p.Row = 0; p.Row < Rows; p.Row++) {
                for (p.Col = 0; p.Col < Cols; p.Col++) {
                    action(this, p);
                }
            }
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
        public MapPos RandEmptyPos() {
            var x = RandPos();
            while(!this[x].IsEmpty) {
                x = RandPos();
            }
            return x;
        }
        

        public MapPos FindGold() => null;

        // public void Clear() => Traverse((_, p) => this[p].Clear());



        private void Populate() {
            Traverse((_, p) => this[p] = new Cell());
            this[0, 0, 3].Contents = Items.Exit;
            foreach (var x in Items.AllTreasures) {
                this[RandEmptyPos()].Contents = x;
            }
            Traverse((map, p) => {
                int chance = Util.RandInt(101);
                if (map[p].IsEmpty) {
                    if (chance < 6) {
                        // 0-5: DownStairs, Sinkole or Warp
                        if (p.Level < map.Levels - 1) {
                            switch (Util.RandInt(3)) {
                                case 0:
                                    map[p].Contents = Items.DownStairs;
                                    map[p.Level + 1, p.Row, p.Col].Contents = Items.UpStairs;
                                    break;
                                case 1:
                                    map[p].Contents = Items.SinkHole;
                                    break;
                                case 2:
                                    map[p].Contents = Items.Warp;
                                    break;
                            }
                        } else {
                            map[p].Contents = Items.Warp;
                        }
                    } else if (chance < 11) {
                        //6-10: Book
                        map[p].Contents = Items.Book;
                    } else if (chance < 16) {
                        //11-15: Chest
                        map[p].Contents = Items.Chest;
                    } else if (chance < 21) {
                        //16-20: Orb
                        // map[p] = GameCollections.RoomContents.Find(item => item == "Orb");
                    } else if (chance < 26) {
                        //21-25: Pool
                        // map[p] = GameCollections.RoomContents.Find(item => item == "Pool");
                    } else if (chance < 31) {
                        //26-30: Flares
                        map[p].Contents = Items.Flares;
                    } else if (chance < 36) {
                        //31-35: Gold
                        map[p].Contents = Items.Gold;
                    } else if (chance < 46) {
                        //36-45: Vendor
                        // map[p] = GameCollections.RoomContents.Find(item => item == "Vendor");
                    } else if (chance < 61) {
                        //46-60: Monster 
                        map[p].Contents = Util.RandPick(Items.AllMonsters);
                    }
                }
            });
        }
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

}
