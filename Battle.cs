using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle
{
    class Battle
    {
        static readonly Random rand = new Random();
        public static void BattleSequence(ref Player player, ref Monster monster, string[,,] theMap)
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
                Console.WriteLine($"\nYou are facing a {monster.race}!");
                question = "What would you like to do";
                if ((((rand.Next(0, 101) + monster.dexterity) > 75) || player.lethargy) && firstAttackRound) { 
                    Battle.MonsterAttack(ref player, ref monster);
                }
                else if (firstAttackRound) { choicesDict.Add('B', "Bribe"); }
                if (player.intelligence > 14) { choicesDict.Add('C', "Cast"); }
                if (player.strength > 0)
                {
                    string[] choice = battleMenu.Menu(question, choicesDict, GameCollections.ErrorMesssages);
                    switch (choice[1])
                    {
                        case "Attack":
                            Battle.PlayerAttack(ref player, ref monster);
                            break;
                        case "Bribe":
                            Battle.PlayerBribe(ref player, ref monster);
                            break;
                        case "Cast":
                            Battle.PlayerCast(ref player, ref monster);
                            break;
                        case "Retreat":
                            Battle.PlayerRetreat(ref player, ref monster, theMap);
                            break;
                    }
                    if (monster.strength > 0 && monster.mad && player.location.SequenceEqual(monster.location)) { Battle.MonsterAttack(ref player, ref monster); }
                }
                if (choicesDict.ContainsKey('B')) { choicesDict.Remove('B'); }
                if (choicesDict.ContainsKey('C')) { choicesDict.Remove('C'); }
                firstAttackRound = false;
            } while (monster.mad && monster.strength > 0 && player.strength > 0 && monster.location.SequenceEqual(player.location));
            if (monster.strength < 1)
            {
                monster.gold = rand.Next(1, 1001);
                Console.WriteLine($"\nYou killed the evil {monster.race}");
                Console.WriteLine($"You get his hoard of {monster.gold} Gold Pieces");
                player.gold += monster.gold;
                if (monster.runeStaff)
                {
                    Console.WriteLine("You've found the RuneStaff!");
                    player.runeStaff = true;
                }
                if (monster.treasures.Count > 0)
                {
                    Console.WriteLine($"You've recoverd the {monster.treasures[0]}");
                    player.treasures.Add(monster.treasures[0]);
                }
                Console.WriteLine();
            }
        }

        static void PlayerBribe(ref Player player, ref Monster monster)
        {
            if (player.treasures.Count > 0)
            {
                string treasure = player.treasures[rand.Next(player.treasures.Count)];
                Console.WriteLine($"The {monster.race} says I want the {treasure}. Will you give it to me?");
                YWMenu battleMenu = new YWMenu();
                string question = $"Give the {monster.race} the {treasure}";
                Dictionary<char, string> choicesDict = new Dictionary<char, string>
                {
                    { 'Y', "Yes" },
                    { 'N', "No" }
                };
                string[] choice = battleMenu.Menu(question, choicesDict, GameCollections.ErrorMesssages);
                if (choice[0] == "Y")
                {
                    Console.WriteLine($"\nThe {monster.race} says, ok, just don't tell anyone.");
                    player.treasures.Remove(treasure);
                    monster.treasures.Add(treasure);
                    monster.mad = false;
                } 
            }
            if (monster.mad)
            {
                Console.WriteLine($"\nThe {monster.race} says, all I want is your life!");
            }
        }

        static void PlayerCast(ref Player player, ref Monster monster)
        {
            YWMenu battleMenu = new YWMenu();
            string question = $"What spell do you want to cast";
            Dictionary<char, string> choicesDict = new Dictionary<char, string>
                {
                    { 'W', "Web" },
                    { 'F', "Fireball" },
                    { 'D', "Deathspell" }
                };
            string[] choice = battleMenu.Menu(question, choicesDict, GameCollections.ErrorMesssages);
            switch (choice[0])
            {
                case "W":
                    Console.WriteLine($"\nYou've caught the {monster.race} in a web, now it can't attack");
                    monster.webbedTurns = rand.Next(1, 11);
                    player.strength -= 1;
                    break;
                case "F":
                    Console.WriteLine($"\nYou blast the {monster.race} with a fireball.");
                    monster.strength -= rand.Next(1, 11);
                    player.intelligence -= 1;
                    player.strength -= 1;
                    break;
                case "D":
                    if ((player.intelligence > monster.intelligence) && (rand.Next(1,9) < 7))
                    {
                        Console.WriteLine($"\nDEATH! The {monster.race} is dead.");
                        monster.strength = 0;
                    } else
                    {
                        Console.WriteLine($"\nDEATH! The STUPID {player.race}'s death.");
                        player.strength = 0;
                        Util.WaitForKey();
                    }
                    break;
            }
        }

        static void PlayerRetreat(ref Player player, ref Monster monster, string[,,] theMap)
        {
            if ((rand.Next(0,101) - player.dexterity) > 50)
            {
                Battle.MonsterAttack(ref player, ref monster);
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
                string[] choice = battleMenu.Menu(question, choicesDict, GameCollections.ErrorMesssages);
                switch (choice[1])
                {
                    case "North":
                        player.North(theMap);
                        Console.WriteLine("\nYou retreat to the North!");
                        Util.WaitForKey();
                        break;
                    case "South":
                        player.South(theMap);
                        Console.WriteLine("\nYou retreat to the South!");
                        Util.WaitForKey();
                        break;
                    case "East":
                        player.East(theMap);
                        Console.WriteLine("\nYou retreat to the East!");
                        Util.WaitForKey();
                        break;
                    case "West":
                        player.West(theMap);
                        Console.WriteLine("\nYou retreat to the West!");
                        Util.WaitForKey();
                        break;
                }
            }
        }

        static void PlayerAttack(ref Player player, ref Monster monster)
        {
            if ((player.weapon.Length > 0) && ! player.bookStuck)
            {
                Console.WriteLine($"\nYou attack the {monster.race}!");
                if (rand.Next(1,11) > 5)
                {
                    Console.WriteLine("You hit it!");
                    int damage = rand.Next(1, 6);
                    if (player.weapon == "Sword") { damage += 3; }
                    if (player.weapon == "Mace") { damage += 2; }
                    if (player.weapon == "Dagger") { damage += 1; }
                    monster.strength -= damage;
                    if ((monster.race == "Dragon" || monster.race == "Gargoyle") && rand.Next(1, 11) > 9)
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
                    Console.WriteLine($"\nYou can't beat the {monster.race} to death with a book.");
                }
                else
                {
                    Console.WriteLine($"\nStupid {player.race}! You have no weapon and pounding on the {monster.race} won't do any good.");
                }
            }
        }

        static void MonsterAttack(ref Player player, ref Monster monster)
        {
            if (monster.webbedTurns > 0)
            {
                Console.WriteLine($"\nThe {monster.race} is caught in a web and can't attack.");
                monster.webbedTurns -= 1;
                if (monster.webbedTurns == 0)
                {
                    Console.WriteLine($"\nThe web breaks!");
                }
            }
            else
            {
                Console.WriteLine($"\nThe {monster.race} attacks!");
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
