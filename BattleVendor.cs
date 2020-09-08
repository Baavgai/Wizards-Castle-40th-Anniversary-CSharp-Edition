using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle
{
    class BattleVendor
    {
        static readonly Random rand = new Random();
        public static void BattleSequence(ref Player player, ref Vendor vendor, string[,,] theMap)
        {
            YWMenu battleMenu = new YWMenu();
            string question;
            bool firstAttackRound = true;
            Dictionary<char, string> choicesDict = new Dictionary<char, string>
            {
                { 'A', "Attack" },
                { 'R', "Retreat" }
            };
            do
            {
                Console.WriteLine($"\nYou are facing a {vendor.race}!");
                question = "What would you like to do";
                if ((((rand.Next(0, 101) + vendor.dexterity) > 75) || player.lethargy) && firstAttackRound) { 
                    BattleVendor.VendorAttack(ref player, ref vendor);
                }
                else if (firstAttackRound) { choicesDict.Add('B', "Bribe"); }
                if (player.intelligence > 14) { choicesDict.Add('C', "Cast"); }
                if (player.strength > 0)
                {
                    string[] choice = battleMenu.Menu(question, choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
                    switch (choice[1])
                    {
                        case "Attack":
                            BattleVendor.PlayerAttack(ref player, ref vendor);
                            break;
                        case "Bribe":
                            BattleVendor.PlayerBribe(ref player, ref vendor);
                            break;
                        case "Cast":
                            BattleVendor.PlayerCast(ref player, ref vendor);
                            break;
                        case "Retreat":
                            BattleVendor.PlayerRetreat(ref player, ref vendor, theMap);
                            break;
                    }
                    if (vendor.strength > 0 && vendor.mad && player.location.SequenceEqual(vendor.location)) { BattleVendor.VendorAttack(ref player, ref vendor); }
                }
                if (choicesDict.ContainsKey('B')) { choicesDict.Remove('B'); }
                if (choicesDict.ContainsKey('C')) { choicesDict.Remove('C'); }
                firstAttackRound = false;
            } while (vendor.mad && vendor.strength > 0 && player.strength > 0 && vendor.location.SequenceEqual(player.location));
            if (vendor.strength < 1)
            {
                vendor.gold = rand.Next(1, 1001);
                Console.WriteLine($"\nYou killed the evil {vendor.race}");
                Console.WriteLine($"You get his hoard of {vendor.gold} Gold Pieces");
                Console.WriteLine($"You also get the {vendor.race}'s Sword and Plate armor and Lamp");
                player.weapon = "Sword";
                player.armor = "Plate";
                player.lamp = true;
                player.gold += vendor.gold;
                if (vendor.runeStaff)
                {
                    Console.WriteLine("You've found the RuneStaff!");
                    player.runeStaff = true;
                }
                if (vendor.treasures.Count > 0)
                {
                    Console.WriteLine($"You've recoverd the {vendor.treasures[0]}");
                    player.treasures.Add(vendor.treasures[0]);
                }
                Console.WriteLine();
            }
        }

        static void PlayerBribe(ref Player player, ref Vendor vendor)
        {
            if (player.treasures.Count > 0)
            {
                string treasure = player.treasures[rand.Next(player.treasures.Count)];
                Console.WriteLine($"The {vendor.race} says I want the {treasure}. Will you give it to me?");
                YWMenu battleMenu = new YWMenu();
                string question = $"Give the {vendor.race} the {treasure}";
                Dictionary<char, string> choicesDict = new Dictionary<char, string>
                {
                    { 'Y', "Yes" },
                    { 'N', "No" }
                };
                string[] choice = battleMenu.Menu(question, choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
                if (choice[0] == "Y")
                {
                    Console.WriteLine($"\nThe {vendor.race} says, ok, just don't tell anyone.");
                    player.treasures.Remove(treasure);
                    vendor.treasures.Add(treasure);
                    vendor.mad = false;
                    GameCollections.AllVendorMad = false;
                } 
            }
            if (vendor.mad)
            {
                Console.WriteLine($"\nThe {vendor.race} says, all I want is your life!");
            }
        }

        static void PlayerCast(ref Player player, ref Vendor vendor)
        {
            YWMenu battleMenu = new YWMenu();
            string question = $"What spell do you want to cast";
            Dictionary<char, string> choicesDict = new Dictionary<char, string>
                {
                    { 'W', "Web" },
                    { 'F', "Fireball" },
                    { 'D', "Deathspell" }
                };
            string[] choice = battleMenu.Menu(question, choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
            switch (choice[0])
            {
                case "W":
                    Console.WriteLine($"\nYou've caught the {vendor.race} in a web, now it can't attack");
                    vendor.webbedTurns = rand.Next(1, 11);
                    player.strength -= 1;
                    break;
                case "F":
                    Console.WriteLine($"\nYou blast the {vendor.race} with a fireball.");
                    vendor.strength -= rand.Next(1, 11);
                    player.intelligence -= 1;
                    player.strength -= 1;
                    break;
                case "D":
                    if ((player.intelligence > vendor.intelligence) && (rand.Next(1,9) < 7))
                    {
                        Console.WriteLine($"\nDEATH! The {vendor.race} is dead.");
                        vendor.strength = 0;
                    } else
                    {
                        Console.WriteLine($"\nDEATH! The STUPID {player.race}'s death.");
                        player.strength = 0;
                        SharedMethods.WaitForKey();
                    }
                    break;
            }
        }

        static void PlayerRetreat(ref Player player, ref Vendor vendor, string[,,] theMap)
        {
            if ((rand.Next(0,101) - player.dexterity) > 50)
            {
                BattleVendor.VendorAttack(ref player, ref vendor);
            }
            if (player.strength > 0)
            {
                YWMenu battleMenu = new YWMenu();
                string question = $"Retreat which way";
                Dictionary<char, string> choicesDict = new Dictionary<char, string>
                {
                    { 'N', "North" },
                    { 'S', "South" },
                    { 'E', "East" },
                    { 'W', "West" }
                };
                string[] choice = battleMenu.Menu(question, choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
                switch (choice[1])
                {
                    case "North":
                        player.North(theMap);
                        Console.WriteLine("\nYou retreat to the North!");
                        SharedMethods.WaitForKey();
                        break;
                    case "South":
                        player.South(theMap);
                        Console.WriteLine("\nYou retreat to the South!");
                        SharedMethods.WaitForKey();
                        break;
                    case "East":
                        player.East(theMap);
                        Console.WriteLine("\nYou retreat to the East!");
                        SharedMethods.WaitForKey();
                        break;
                    case "West":
                        player.West(theMap);
                        Console.WriteLine("\nYou retreat to the West!");
                        SharedMethods.WaitForKey();
                        break;
                }
            }
        }

        static void PlayerAttack(ref Player player, ref Vendor vendor)
        {
            if ((player.weapon.Length > 0) && ! player.bookStuck)
            {
                Console.WriteLine($"\nYou attack the {vendor.race}!");
                if (rand.Next(1,11) > 5)
                {
                    Console.WriteLine("You hit it!");
                    int damage = rand.Next(1, 6);
                    if (player.weapon == "Sword") { damage += 3; }
                    if (player.weapon == "Mace") { damage += 2; }
                    if (player.weapon == "Dagger") { damage += 1; }
                    vendor.strength -= damage;
                    if ((vendor.race == "Dragon" || vendor.race == "Gargoyle") && rand.Next(1, 11) > 9)
                    {
                        Console.WriteLine($"Oh No! Your {player.weapon} just broke!");
                        player.weapon = "";
                    }
                }
                else
                {
                    Console.WriteLine("Drats! You Missed!");
                }
            }
            else
            {
                if (player.bookStuck)
                {
                    Console.WriteLine($"\nYou can't beat the {vendor.race} to death with a book.");
                }
                else
                {
                    Console.WriteLine($"\nStupid {player.race}! You have no weapon and pounding on the {vendor.race} won't do any good.");
                }
            }
        }

        static void VendorAttack(ref Player player, ref Vendor vendor)
        {
            if (vendor.webbedTurns > 0)
            {
                Console.WriteLine($"\nThe {vendor.race} is caught in a web and can't attack.");
                vendor.webbedTurns -= 1;
                if (vendor.webbedTurns == 0)
                {
                    Console.WriteLine($"\nThe web breaks!");
                }
            }
            else
            {
                Console.WriteLine($"\nThe {vendor.race} attacks!");
                if (rand.Next(1,11) > 5)
                {
                    Console.WriteLine("It hit you!");
                    int damage = rand.Next(1, 6);
                    if (player.armor == "Plate") { damage -= 3; }
                    if (player.armor == "ChainMail") { damage -= 2; }
                    if (player.armor == "Leather") { damage -= 1; }
                    if (damage > 0)
                    {
                        player.strength -= damage;
                    }
                    if (rand.Next(1, 11) > 9)
                    {
                        Console.WriteLine($"\nOh No! Your {player.armor} armor is destroyed!");
                        player.armor = "";
                    }
                }
                else
                {
                    Console.WriteLine("It Missed!");
                }
            }
        }
    }
}
