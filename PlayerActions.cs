using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using YWMenuNS;

namespace The_Wizard_s_Castle {
    class PlayerActions {
        public static void Action(ref Player player, string[] action, ref string[,,] knownMap, ref string[,,] theMap) {
            YWMenu menu = new YWMenu();
            switch (action[0]) {
                case "M":
                    Map.DisplayLevel(knownMap, player);
                    Util.WaitForKey();
                    break;
                case "N":
                    if (player.location[0] == 0 && player.location[1] == 0 && player.location[2] == 3) {
                        Program.PlayerExit(player);
                        break;
                    } else {
                        player.North(knownMap);
                        break;
                    }
                case "S":
                    player.South(knownMap);
                    break;
                case "E":
                    player.East(knownMap);
                    break;
                case "W":
                    player.West(knownMap);
                    break;
                case "F":
                    if (player.flares > 0) {
                        if (!player.blind) {
                            Map.RevealMap(player.location, theMap, ref knownMap);
                            player.flares -= 1;
                            Map.DisplayLevel(knownMap, player);
                        } else {
                            Console.WriteLine("Lighting a flare won't do you any good since you are BLIND!");
                        }
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                    }
                    Util.WaitForKey();
                    break;
                case "T":
                    string teleportTo;
                    int[] teleCoordinates = new int[3];
                    if (player.runeStaff == true) {
                        do {
                            Console.Clear();
                            Console.WriteLine();
                            Console.Write("\rTeleport where (Example: For Level 3, Row 5, Column 2 type: 3,5,2): ");
                            teleportTo = Console.ReadLine();
                            teleportTo = teleportTo.Replace(" ", "");
                            try {
                                teleCoordinates = Array.ConvertAll(teleportTo.Split(','), int.Parse);
                                if (teleCoordinates[0] - 1 < theMap.GetLength(0) && teleCoordinates[1] - 1 < theMap.GetLength(1) && teleCoordinates[2] - 1 < theMap.GetLength(2)) {
                                    player.location[0] = teleCoordinates[0] - 1;
                                    player.location[1] = teleCoordinates[1] - 1;
                                    player.location[2] = teleCoordinates[2] - 1;
                                } else {
                                    teleportTo = "";
                                    Util.WaitForKey("* Invalid * Coordinates");
                                }
                            } catch (Exception) {
                                teleportTo = "";
                                Util.WaitForKey("* Invalid * Coordinates");
                            }
                        } while (teleportTo.Length < 1);
                        Console.WriteLine($"\n\tTeleporting to: ({teleCoordinates[0]}, {teleCoordinates[1]}, {teleCoordinates[2]})");
                        for (int i = 0; i < 30; i++) {
                            Console.Write(".");
                            Thread.Sleep(100);
                        }
                    } else {
                        Util.WaitForKey($"\nSorry, {player.race}, but you need the RuneStaff to teleport!\n");
                    }
                    break;
                case "L":
                    if (player.lamp == true) {
                        if (!player.blind) {
                            string[] choice = menu.Menu("Shine lamp which direction", new Dictionary<char, string>
                        {
                            {'N', "North"},
                            {'S', "South"},
                            {'E', "East"},
                            {'W', "West"}
                        }, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
                            Map.RevealRoom(choice[1], player.location, theMap, ref knownMap);
                            Map.DisplayLevel(knownMap, player);
                        } else {
                            Util.WaitForKey($"You're BLIND and can't see anything, silly {player.race}.");
                        }
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                    }
                    Util.WaitForKey();
                    break;
                case "D":
                    if (knownMap[player.location[0], player.location[1], player.location[2]] == "DownStairs") {
                        player.Down();
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                        Util.WaitForKey();
                    }
                    break;
                case "U":
                    if (knownMap[player.location[0], player.location[1], player.location[2]] == "UpStairs") {
                        player.Up();
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                        Util.WaitForKey();
                    }
                    break;
                case "G":
                    if (knownMap[player.location[0], player.location[1], player.location[2]] == "Orb") {
                        if (!player.blind) {
                            Console.WriteLine(Orb.OrbEvent(ref player, ref theMap));
                        } else {
                            Console.WriteLine("The only thing you see is darkness because you are blind");
                        }
                        Util.WaitForKey();
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                        Util.WaitForKey();
                    }
                    break;
                case "O":
                    if (knownMap[player.location[0], player.location[1], player.location[2]] == "Book") {
                        if (!player.blind) {
                            Console.WriteLine(Book.BookEvent(ref player, ref theMap));
                        } else {
                            Console.WriteLine($"Sorry, {player.race}, it's not written in Braille!");
                        }
                        Util.WaitForKey();
                    } else if (knownMap[player.location[0], player.location[1], player.location[2]] == "Chest") {
                        Console.WriteLine(Chest.ChestEvent(ref player, ref theMap, ref knownMap));
                        Util.WaitForKey();
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                        Util.WaitForKey();
                    }
                    break;
                case "P":
                    if (knownMap[player.location[0], player.location[1], player.location[2]] == "Pool") {
                        Console.WriteLine(Util.PoolEvent(player));
                        Util.WaitForKey();
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                    }
                    break;
                case "Z":
                    if (knownMap[player.location[0], player.location[1], player.location[2]] == "Vendor") {
                        Vendor vendor = Vendor.GetOrCreateVendor(theMap, player, string.Join(string.Empty, new[] { player.location[0], player.location[1], player.location[2] }));
                        VendorTrade.Trade(ref player, ref vendor);
                    } else {
                        Console.WriteLine($"\nSorry, {player.race}, there's no vendor in the room.");
                        Util.WaitForKey();
                    }
                    break;
                case "A":
                    if (knownMap[player.location[0], player.location[1], player.location[2]] == "Vendor") {
                        Vendor vendor = Vendor.GetOrCreateVendor(theMap, player, string.Join(string.Empty, new[] { player.location[0], player.location[1], player.location[2] }));
                        GameCollections.AllVendorMad = true;
                        vendor.mad = true;
                        Console.WriteLine($"\n{Vendor.VendorMadMessage(vendor)}\n");
                        BattleVendor.BattleSequence(ref player, ref vendor, theMap);
                        if (vendor.strength < 1) {
                            theMap[player.location[0], player.location[1], player.location[2]] = "-";
                            Util.WaitForKey();
                        }
                    } else if (GameCollections.Monsters.Contains(theMap[player.location[0], player.location[1], player.location[2]])) {
                        Monster monster = Monster.GetOrCreateMonster(theMap, player, string.Join(string.Empty, new[] { player.location[0], player.location[1], player.location[2] }));
                        monster.mad = true;
                        Console.WriteLine($"\n{Monster.MonsterMadMessage(monster)}\n");
                        Battle.BattleSequence(ref player, ref monster, theMap);
                        if (monster.strength < 1) {
                            theMap[player.location[0], player.location[1], player.location[2]] = "-";
                        }
                        Util.WaitForKey();
                    } else {
                        Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                        Util.WaitForKey();
                    }
                    break;
                case "V":
                    Instructions.ViewInstructions();
                    break;
                case "Q":
                    break;
                default:
                    Console.WriteLine("\n{0}\n", ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages)[new Random().Next(0, GameCollections.ErrorMesssages.Count)]);
                    Util.WaitForKey();
                    break;
            }
        }
    }
}