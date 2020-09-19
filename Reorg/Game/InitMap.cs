using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    internal static partial class Game {

        public static Map InitMap(Map map) {
            map[StartingLocation].Contents = Content.Exit;
            // foreach (var x in Items.AllTreasures) {                map[RandEmptyMapPos(map)].Contents = x;            }
            map.Traverse((_, p) => {
                int chance = Util.RandInt(101);
                if (map[p].IsEmpty) {
                    if (chance < 6) {
                        // 0-5: DownStairs, Sinkole or Warp
                        if (p.Level < map.Levels - 1) {
                            switch (Util.RandInt(3)) {
                                case 0:
                                    map[p].Contents = Content.DownStairs;
                                    map[p.Level + 1, p.Row, p.Col].Contents = Content.UpStairs;
                                    break;
                                case 1:
                                    map[p].Contents = Content.SinkHole;
                                    break;
                                case 2:
                                    map[p].Contents = Content.Warp;
                                    break;
                            }
                        } else {
                            map[p].Contents = Content.Warp;
                        }
                    } else if (chance < 11) {
                        //6-10: Book
                        map[p].Contents = Content.Book;
                    } else if (chance < 16) {
                        //11-15: Chest
                        map[p].Contents = Content.Chest;
                    } else if (chance < 21) {
                        //16-20: Orb
                        map[p].Contents = Content.Orb;
                    } else if (chance < 26) {
                        //21-25: Pool
                        map[p].Contents = Content.Pool;
                    } else if (chance < 31) {
                        //26-30: Flares
                        map[p].Contents = Content.Flares;
                    } else if (chance < 36) {
                        //31-35: Gold
                        map[p].Contents = Content.Gold;
                    } else if (chance < 46) {
                        //36-45: Vendor
                        map[p].Contents = VendorFactory.Create();
                    } else if (chance < 61) {
                        //46-60: Monster 
                        map[p].Contents = Util.RandPick(MonsterFactory.All).Create();
                    }
                }
            });

            // give all treasures to monsters
            var richMonsters = map.AllPos()
                .Where(p => map[p].Contents != null && map[p].Contents is IMonster)
                .Select(p => map[p].Contents)
                .OrderBy(_ => Util.RandInt(1000))
                .Take(Treasure.All.Length + 1)
                .Cast<IMonster>()
                .ToList();
            richMonsters[0].Inventory.Add(RuneStaff.Instance);
            for (int i = 1; i < richMonsters.Count; i++) {
                richMonsters[i].Inventory.Add(Treasure.All[i]);
            }

            return map;

        }
    }
}

