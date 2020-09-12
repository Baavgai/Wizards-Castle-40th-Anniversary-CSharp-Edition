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
        private MapPos RandPos(Func<MapPos,bool> good) {
            var x = RandPos();
            while (!good(x)) { x = RandPos(); }
            return x;
        }


        public MapPos RandEmptyPos() => RandPos(p => this[p].IsEmpty);
        public MapPos RandKnownPos() => RandPos(p => this[p].Known);


        public MapPos FindGold() => null;

        // public void Clear() => Traverse((_, p) => this[p].Clear());



        private void Populate() {
            // GameCollections.RuneStaffLocation = Map.FindMonster(theMap, GameCollections.Monsters[new Random().Next(0, GameCollections.Monsters.Count)]);
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
