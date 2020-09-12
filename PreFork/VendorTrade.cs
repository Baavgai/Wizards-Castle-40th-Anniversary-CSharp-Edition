using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using YWMenuNS;

namespace The_Wizard_s_Castle
{
    class VendorTrade
    {
        static readonly Random rand = new Random();
        static readonly YWMenu vtMenu = new YWMenu();
        static int offerAmount;
        public static void Trade(ref Player player, ref Vendor vendor)
        {
            if (player.treasures.Count < 1 && player.gold < 1000)
            {
                Console.WriteLine($"Sorry, {player.race}. You are too poor to trade.");
                SharedMethods.WaitForKey();
            }
            else
            {
                if (player.treasures.Count > 0)
                {
                    List<string> treasuresSold = new List<string>();
                    foreach (string item in player.treasures)
                    {
                        offerAmount = rand.Next(1, 5001);
                        string question = $"Do you want to sell {item} for {offerAmount}";
                        Dictionary<char, string> choicesDict = new Dictionary<char, string>
                        {
                            { 'Y', "Yes" },
                            { 'N', "No" }
                        };
                        string[] choice = vtMenu.Menu(question, choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
                        if (choice[0] == "Y")
                        {
                            Console.WriteLine($"\nYou have accepted the Vendor's offer for {item}.");
                            treasuresSold.Add(item);
                            vendor.treasures.Add(item);
                            player.gold += offerAmount;
                        }
                    }
                    foreach (string item in treasuresSold)
                    {
                        player.treasures.Remove(item);
                    }
                    if (player.gold < 1000)
                    {
                        Console.WriteLine($"\nSorry, {player.race}. You are too poor to trade.");
                        SharedMethods.WaitForKey();
                    }
                }
                if  ((player.gold > 1499) && (player.armor != "Plate"))
                {
                    Player.GetPlayerArmor(ref player, new int[] { 1500, 2000, 2500 });
                }
                if ((player.gold > 1499) && (player.weapon != "Sword"))
                {
                    Player.GetPlayerWeapon(ref player, new int[] { 1500, 2000, 2500 });
                }
                if ((player.gold > 999) && (! player.lamp))
                {
                    Player.GetLamp(ref player, 1000);
                }
                if (player.gold > 999)
                {
                    GetPlayerPotion(ref player, 1000);
                }
            }
        }
        public static void GetPlayerPotion(ref Player player, int cost)
        {
            Console.WriteLine();
            Dictionary<char, string> choicesDict = new Dictionary<char, string>();
            for (int i = 0; i < GameCollections.Abilities.Count; i++)
            {
                choicesDict.Add(i.ToString()[0], $"{GameCollections.Abilities[i]}, {cost} Gold Pieces");
            }
            choicesDict.Add(choicesDict.Count.ToString()[0], $"None, 0 Gold Pieces");
            string[] choice;
            string numberOnly;
            do
            {
                choice = vtMenu.Menu($"You have {player.gold} Gold Pieces to buy items, what type of Potion do you want to purchase", choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
                numberOnly = Regex.Replace(choice[1], "[^0-9]", "");
                if (!(Convert.ToInt32(numberOnly) > player.gold) && !(choice[1].Split(',')[0] == "None"))
                {
                    switch (choice[1].Split(',')[0])
                    {
                        case "Dexterity":
                            player.IncDexterity(rand.Next(1, 6));
                            Console.WriteLine($"\nDexterity={player.dexterity}");
                            SharedMethods.WaitForKey();
                            break;
                        case "Intelligence":
                            player.IncIntelligence(rand.Next(1, 6));
                            Console.WriteLine($"\nIntelligence={player.intelligence}");
                            SharedMethods.WaitForKey();
                            break;
                        case "Strength":
                            player.IncStrength(rand.Next(1, 6));
                            Console.WriteLine($"\nStrength={player.strength}");
                            SharedMethods.WaitForKey();
                            break;
                    }
                    player.gold -= Convert.ToInt32(numberOnly);
                }
                else
                {
                    if (!(choice[1].Split(',')[0] == "None"))
                    {
                        Console.WriteLine($"\n\tSorry, {player.race}, you don't have that much gold left.");
                    }
                }
            } while (player.gold > 999 && !(choice[1].Split(',')[0] == "None"));
        }
    }
}
