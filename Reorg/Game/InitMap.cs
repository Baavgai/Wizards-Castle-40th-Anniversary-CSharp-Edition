using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    internal static partial class Game {

        public static void InitMap(Map map) {
            map[StartingLocation].Contents = Items.Exit;
            // foreach (var x in Items.AllTreasures) {                map[RandEmptyMapPos(map)].Contents = x;            }
            map.Traverse((_, p) => {
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
                        map[p].Contents = Util.RandPick(Monster.AllMonsters).CreateMonster();
                    }
                }
            });

            // give all treasures to monsters
            var richMonsters = map.AllPos()
                .Where(p => map[p].Contents is Monster.IMonster)
                .OrderBy(_ => Util.RandInt(1000))
                .Take(Items.AllTreasures.Length)
                .Select(p => map[p].Contents as Monster.IMonster)
                .ToList();
            for(int i=0; i< Items.AllTreasures.Length; i++) {
                richMonsters[i].Inventory.Add(Items.AllTreasures[i]);
            }
            //             foreach (var x in Items.AllTreasures) { map[RandEmptyMapPos(map)].Contents = x; }



            // GameCollections.RuneStaffLocation = Map.FindMonster(theMap, GameCollections.Monsters[new Random().Next(0, GameCollections.Monsters.Count)]);

        }
    }
}

