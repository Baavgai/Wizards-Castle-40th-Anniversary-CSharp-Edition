using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using YWMenuNS;

namespace The_Wizard_s_Castle
{
    class Player : Character
    {
        public Player()
        {
        }

        Player(string race, string sex, int dexterity, int intelligence, int strength)
        {
            this.race = race;
            this.sex = sex;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
            this.strength = strength;
        }

        public int turns = 0;
        public int lampBurn = 0;
        public int flares = 0;
        public string armor = "";
        public string sex = "";
        public string weapon = "";
        public bool blind = false;
        public bool bookStuck = false;
        public bool forgetfulness = false;
        public bool leech = false;
        public bool lethargy = false;
        public bool lamp = false;
        public bool orbOfZot = false;
        public bool runeStaff = false;
        readonly Random rand = new Random();
        private static readonly YWMenu menu = new YWMenu();

        public static Player CreatePlayer()
        {
            string[] race = GetPlayerRace();
            Console.Clear();
            string[] sex = GetPlayerSex();
            Console.Clear();
            int[] extraPoints = GetPlayerExtraPoints(race[1], GameCollections.Abilities);
            Player player = new Player
            (
                race[1],
                sex[1],
                GameCollections.Races[Convert.ToInt32(race[0])].Dexterity,
                GameCollections.Races[Convert.ToInt32(race[0])].Intelligence,
                GameCollections.Races[Convert.ToInt32(race[0])].Strength
            );
            player.IncDexterity(extraPoints[0]);
            player.IncIntelligence(extraPoints[1]);
            player.IncStrength(extraPoints[2]);
            player.gold = 60;
            Console.Clear();
            GetPlayerArmor(ref player, new int[] { 10, 20, 30 });
            Console.Clear();
            GetPlayerWeapon(ref player, new int[] { 10, 20, 30 });
            Console.Clear();
            if (player.gold > 19)
            {
                GetLamp(ref player, 20);
            }
            Console.Clear();
            if (player.gold > 0)
            {
                GetFlares(ref player);
            }
            return player;
        }
        static string[] GetPlayerRace()
        {
            Dictionary<char, string> choicesDict = new Dictionary<char, string>();
            for (int i = 0; i < GameCollections.Races.Count; i++)
            {
                choicesDict.Add(i.ToString()[0], GameCollections.Races[i].RaceName);
            }
            string[] choice = menu.Menu("Please choose your race", choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
            return choice;
        }

        static string[] GetPlayerSex()
        {
            string[] choice = menu.Menu("Please choose your sex", new Dictionary<char, string>
            {
                {'F', "FeMale"},
                {'M', "Male"}
            }, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
            return choice;
        }

        static int[] GetPlayerExtraPoints(string race, List<string> abilities)
        {
            int extraPoints = 8;
            int[] extraPointsArr = new int[3];
            ConsoleKeyInfo keyPressed;
            string regExPattern = @"[0-9]";
            Regex regEx = new Regex(regExPattern);
            if (race == "Hobbit")
            {
                extraPoints -= 4; // Hobbits get 4 less extraPoints
            }
            do
            {
                for (int i = 0; i < abilities.Count; i++)
                {
                    if (extraPoints > 0)
                    {
                        Console.Write($"\nYou have {extraPoints} points left, how many to add to {abilities[i]}: ");
                        do
                        {
                            keyPressed = Console.ReadKey(true);
                        }
                        while (!regEx.IsMatch(keyPressed.KeyChar.ToString()));
                        if (!(Convert.ToInt32(keyPressed.KeyChar.ToString()) > extraPoints))
                        {
                            extraPoints -= Convert.ToInt32(keyPressed.KeyChar.ToString());
                            extraPointsArr[i] += Convert.ToInt32(keyPressed.KeyChar.ToString());
                        }
                        else
                        {
                            Console.WriteLine($"\n\tSorry, {race}, you * DON'T * have that many points left to distribute");
                        }
                    }
                }
            } while (extraPoints > 0);
            return extraPointsArr;
        }

        public static void GetPlayerArmor(ref Player player, int[] costs)
        {
            Console.WriteLine();
            Dictionary<char, string> choicesDict = new Dictionary<char, string>();
            for (int i = 0; i < GameCollections.Armor.Count; i++)
            {
                choicesDict.Add(i.ToString()[0], $"{GameCollections.Armor[i]}, {costs[i]} Gold Pieces");
            }
            choicesDict.Add(choicesDict.Count.ToString()[0], $"None, 0 Gold Pieces");
            string[] choice = menu.Menu($"You have {player.gold} Gold Pieces to buy items, what type of Armor do you want to purchase", choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
            string numberOnly = Regex.Replace(choice[1], "[^0-9]", "");
            if (!(Convert.ToInt32(numberOnly) > player.gold) && !(choice[1].Split(',')[0] == "None"))
            {
                player.gold -= Convert.ToInt32(numberOnly);
                player.armor = choice[1].Split(',')[0];
            }
            else
            {
                if (!(choice[1].Split(',')[0] == "None"))
                {
                    Console.WriteLine($"\n\tSorry, {player.race}, you don't have that much gold left.");
                }
            }
        }

        public static void GetPlayerWeapon(ref Player player, int[] costs)
        {
            Console.WriteLine();
            Dictionary<char, string> choicesDict = new Dictionary<char, string>();
            for (int i = 0; i < GameCollections.Weapons.Count; i++)
            {
                choicesDict.Add(i.ToString()[0], $"{GameCollections.Weapons[i]}, {costs[i]} Gold Pieces");
            }
            choicesDict.Add(choicesDict.Count.ToString()[0], $"None, 0 Gold Pieces");
            string[] choice = menu.Menu($"You have {player.gold} Gold Pieces to buy items, what type of Weapon do you want to purchase", choicesDict, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
            string numberOnly = Regex.Replace(choice[1], "[^0-9]", "");
            if (!(Convert.ToInt32(numberOnly) > player.gold) && !(choice[1].Split(',')[0] == "None"))
            {
                player.weapon = choice[1].Split(',')[0];
                player.gold -= Convert.ToInt32(numberOnly);
            }
            else
            {
                if (!(choice[1].Split(',')[0] == "None"))
                {
                    Console.WriteLine($"\n\tSorry, {player.race}, you don't have that much gold left.");
                }
            }
        }

        public static void GetLamp(ref Player player, int cost)
        { 
            string[] choice = menu.Menu("Would you like to purchase a lamp", new Dictionary<char, string>
            {
                {'Y', $"Purchase lamp ({cost} Gold Pieces)"},
                {'N', "Don't purchase"}
            }, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
            if (choice[0] == "Y")
            {
                int numberOnly = Convert.ToInt32(Regex.Replace(choice[1], "[^0-9]", ""));
                player.gold -= numberOnly;
                player.lamp = true;
            }
        }


        static void GetFlares(ref Player player)
        {
            ConsoleKeyInfo keyPressed;
            string regExPattern = @"[0-9]";
            Regex regEx = new Regex(regExPattern);
            do
            {
                Console.Write($"\nOk, {player.race}, you have {player.gold} Gold Pieces left, how many flares do you want (1 Gold Piece each): ");
                do
                {
                    keyPressed = Console.ReadKey(true);
                } while (!regEx.IsMatch(keyPressed.KeyChar.ToString()));
                if (!(Convert.ToInt32(keyPressed.KeyChar.ToString()) > player.gold))
                {
                    player.flares += Convert.ToInt32(keyPressed.KeyChar.ToString());
                    player.gold -= Convert.ToInt32(keyPressed.KeyChar.ToString());
                }
                else
                {
                    Console.WriteLine($"\n\tSorry, {player.race}, you * DON'T * have that many Gold Pieces left.");
                }
            } while (player.gold > 0 && ! (Convert.ToInt32(keyPressed.KeyChar.ToString()) == 0));
        }

        public void East (string[,,] map)
        {
           if (this.location[2] == (map.GetLength(2) - 1)) {
                this.location[2] = 0;
            }
            else
            {
                this.location[2] += 1;
            }
        }

        public void West(string[,,] map)
        {
           if (this.location[2] == 0) {
                this.location[2] = (map.GetLength(2) - 1);
            }
        else
            {
                this.location[2] -= 1;
            }
        }

        public void North(string[,,] map)
        {
           if (this.location[1] == 0) {
                this.location[1] = (map.GetLength(1) - 1);
            }
        else
            {
                this.location[1] -= 1;
            }
        }

        public void South(string[,,] map)
        {
           if (this.location[1] == (map.GetLength(1) - 1)) {
                this.location[1] = 0;
            }
            else
            {
                this.location[1] += 1;
            }
        }

        public void Down()
        {
            this.location[0] += 1;
        }

        public void Up()
        {
            this.location[0] -= 1;
        }

        public void Sink()
        {
            this.location[0] += 1;
        }

        public void Warp(string[,,] map)
        {
            int level = rand.Next(0, map.GetLength(0));
            int row = rand.Next(0, map.GetLength(1));
            int column = rand.Next(0, map.GetLength(2));
            this.location = (new int[] { level, row, column });
        }
    }
}
