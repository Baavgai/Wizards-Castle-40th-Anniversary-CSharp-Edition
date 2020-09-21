using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    internal static partial class Game {

        public static Map InitMap(Map map) {
            map[StartingLocation].Contents = Content.Exit;
            map.Traverse((_, p) => {
                int chance = Util.RandInt(101);
                if (map[p].IsEmpty) {
                    if (chance < 6) {
                        // 5%
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
                        // 5%
                        //6-10: Book
                        map[p].Contents = Content.Book;
                    } else if (chance < 16) {
                        // 5%
                        //11-15: Chest
                        map[p].Contents = Content.Chest;
                    } else if (chance < 21) {
                        // 5%
                        //16-20: Orb
                        map[p].Contents = Content.Orb;
                    } else if (chance < 26) {
                        // 5%
                        //21-25: Pool
                        map[p].Contents = Content.Pool;
                    } else if (chance < 31) {
                        // 5%
                        //26-30: Flares
                        map[p].Contents = Content.Flares;
                    } else if (chance < 36) {
                        // 5%
                        //31-35: Gold
                        map[p].Contents = Content.Gold;
                    } else if (chance < 46) {
                        // 10%
                        //36-45: Vendor
                        map[p].Contents = VendorFactory.Create();
                    } else if (chance < 61) {
                        // 15%
                        //46-60: Monster 
                        map[p].Contents = Util.RandPick(MonsterFactory.All).Create();
                    }
                }
            });

            // give all treasures to monsters
            foreach(var item in Treasure.All) {
                map.RandCellPosContent<IMonster>().Value.content.Inventory.Add(item);
            }
            map.RandCellPosContent<IMonster>().Value.content.Inventory.Add(RuneStaff.Instance);

            return map;

        }
    }
}

