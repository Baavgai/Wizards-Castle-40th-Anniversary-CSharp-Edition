using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    internal static partial class Game {

        public static Map InitMap(Map map, Action<MapPos, ICellContent> setContent) {
            // map[StartingLocation].Content = Content.Exit;
            setContent(StartingLocation, Content.Exit);
            map.Traverse((_, p) => {
                int chance = Util.RandInt(101);
                if (map[p].IsEmpty()) {
                    if (chance < 6) {
                        // 5%
                        // 0-5: DownStairs, Sinkole or Warp
                        if (p.Level < map.Levels - 1) {
                            switch (Util.RandInt(3)) {
                                case 0:
                                    setContent(p, Content.DownStairs);
                                    setContent(new MapPos(p.Level + 1, p.Row, p.Col), Content.UpStairs);
                                    break;
                                case 1:
                                    setContent(p, Content.SinkHole);
                                    break;
                                case 2:
                                    setContent(p, Content.Warp);
                                    break;
                            }
                        } else {
                            setContent(p, Content.Warp);
                        }
                    } else if (chance < 11) {
                        // 5%
                        //6-10: Book
                        setContent(p, Content.Book);
                    } else if (chance < 16) {
                        // 5%
                        //11-15: Chest
                        setContent(p, Content.Chest);
                    } else if (chance < 21) {
                        // 5%
                        //16-20: Orb
                        setContent(p, Content.Orb);
                    } else if (chance < 26) {
                        // 5%
                        //21-25: Pool
                        setContent(p, Content.Pool);
                    } else if (chance < 31) {
                        // 5%
                        //26-30: Flares
                        setContent(p, Content.Flares);
                    } else if (chance < 36) {
                        // 5%
                        //31-35: Gold
                        setContent(p, Content.Gold);
                    } else if (chance < 46) {
                        // 10%
                        //36-45: Vendor
                        setContent(p, VendorFactory.Create());
                    } else if (chance < 61) {
                        // 15%
                        //46-60: Monster 
                        setContent(p, Util.RandPick(MonsterFactory.All).Create());
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

