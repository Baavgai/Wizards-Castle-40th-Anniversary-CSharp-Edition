// Created by Daniel Kill on 2020-08-25
// Modified: 2020-08-26 by Daniel Kill
// Modified: 2020-08-27 by Daniel Kill
// Modified: 2020-08-28 by Daniel Kill
// Modified: 2020-08-29 by Daniel Kill
// Modified: 2020-08-30 by Daniel Kill
// Modified: 2020-08-31 by Daniel Kill
// Modified: 2020-09-01 by Daniel Kill
// Modified: 2020-09-02 by Daniel Kill
// Modified: 2020-09-03 by Daniel Kill
// Modified: 2020-09-04 by Daniel Kill
// Modified: 2020-09-05 by Daniel Kill
// Modified: 2020-09-06 by Daniel Kill
// Modified: 2020-09-07 by Daniel Kill

using System;
using YWMenuNS;

namespace The_Wizard_s_Castle
{
    class Program
    {
        static int Main()
        {
            // Set the WindowPosition, WindowHeight and WindowWidth
            Console.SetWindowPosition(0, 0);
            System.Console.WindowHeight = System.Console.LargestWindowHeight - 25;
            System.Console.WindowWidth = System.Console.LargestWindowWidth - 50;
            Random rand = new Random();
            YWMenu gameMenu = new YWMenu();
            ShowStartingMessage();
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            System.Console.Clear();
            Instructions.ViewInstructions();
            System.Console.Clear();
            dynamic dynamicTemp;
            string[,,]theMap = Map.GetMap(gameMenu);
            string[,,]knownMap = theMap.Clone() as string[,,];
            bool fallThrough;
            Map.BlankMap(knownMap);
            Player player = Player.CreatePlayer();
            //*** Testing *** int[] locationOfZot = Map.FindOrbOfZot(theMap); // COMMENT THIS
            GameCollections.runestaffLocation = Map.FindMonster(theMap, GameCollections.Monsters[new Random().Next(0, GameCollections.Monsters.Count)]);
            Console.Clear();
            Console.WriteLine($"\tOk, {player.race}, you are now entering Zot's castle!\n");
            //*** Testing *** player.strength = 999;        // For testing
            //*** Testing *** player.intelligence = 999;    // For testing
            //*** Testing *** player.flares = 999;          // For testing
            do
            {
                Console.Clear();
                dynamicTemp = new string[] { "", "" };
                player.turns += 1;
                knownMap[player.location[0], player.location[1], player.location[2]] = theMap[player.location[0], player.location[1], player.location[2]];
                fallThrough = RoomEvents.GetRoomEvent(ref player, ref theMap);
                CheckCurses(ref player, ref knownMap);
                CheckIfDead(player, ref fallThrough);
                if (! fallThrough)
                {
                    if (rand.Next(0, 10) > 8) { Console.WriteLine($"\n{ManipulateListObjects.ReplaceRandomMonster(GameCollections.GameMessages)[rand.Next(0, GameCollections.GameMessages.Count)]}"); }
                    //*** Testing *** Console.WriteLine($"locationOfZot: {locationOfZot[0] + 1}, {locationOfZot[1] + 1}, {locationOfZot[2] + 1}");
                    //*** Testing *** Console.WriteLine($"runestaffLocation: {GameCollections.runestaffLocation[0] + 1}, {GameCollections.runestaffLocation[1] + 1}, {GameCollections.runestaffLocation[2] + 1}");
                    //*** Testing *** Console.WriteLine($"\nRace'{player.race}', player.sex='{player.sex}', player.dexterity='{player.dexterity}', player.intelligence='{player.intelligence}', player.strength='{player.strength}'\nplayer.armor='{player.armor}', player.weapon='{player.weapon}', player.gold='{player.gold}', player.flares='{player.flares}'\nplayer.blind='{player.blind}', player.bookStuck='{player.bookStuck}', player.forgetfulness='{player.forgetfulness}', player.leech='{player.leech}', player.lethargy='{player.lethargy}'\nplayer.lamp='{player.lamp}', player.orbOfZot='{player.orbOfZot}', player.runeStaff='{player.runeStaff}, player.turns='{player.turns}'\nplayer.treasures='{string.Join(", ", player.treasures)}'");
                    knownMap[player.location[0], player.location[1], player.location[2]] = theMap[player.location[0], player.location[1], player.location[2]];
                    string[] choice = gameMenu.Menu("Your action", GameCollections.availableActions, ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages));
                    dynamicTemp = choice;
                    PlayerActions.Action(ref player, choice, ref knownMap, ref theMap);
                }
            } while (!(dynamicTemp[0] == "Q") && GameCollections.ExitCode == 999);

            Console.Clear();
            Console.WriteLine("\t\tThank you for playing Wizard's Castle!");
            ShowStartingMessage();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nPress ENTER to Exit.");
            Console.ReadLine();
            return 0;
        }

        static void ShowStartingMessage()
        {
            Console.Write("\n\t\t\t");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Wizard's Castle");
            Console.Write(" (");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("40th Anniversary Version");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(")\n\n\t");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Copyright (C) 1980 by ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Joseph R Power");
            Console.Write("\n\t");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Last Revised - 04/12/80  11:10 PM");
            Console.Write("\n\n\t");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("C# version written by ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Daniel Kill");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n\t");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Modified: 2020-09-07 by Daniel Kill\n\n");
        }

        static void CheckCurses(ref Player player, ref string[,,] knownMap)
        {
            if (player.treasures.Contains("The Ruby Red") && player.lethargy == true)
            {
                player.lethargy = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Ruby Red cures your Lethargy!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.treasures.Contains("The Pale Pearl") && player.leech == true)
            {
                player.leech = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Pale Pearl heals the curse of the Leech!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.leech == true)
            {
                player.DecStrength(new Random().Next(0, 3));
            }
            if (player.treasures.Contains("The Green Gem") && player.forgetfulness == true)
            {
                player.forgetfulness = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Green Gem cures your forgetfulness!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.forgetfulness == true)
            {
                int[] forgetRoom = Map.ForgetMapRoom(knownMap);
                knownMap[forgetRoom[0], forgetRoom[1], forgetRoom[2]] = "X";
            }
            if (player.treasures.Contains("The Opal Eye") && player.blind == true)
            {
                player.blind = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Opal Eye cures your blindness!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.treasures.Contains("The Blue Flame") && player.bookStuck == true)
            {
                player.bookStuck = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Blue Flame burns the book off your hands!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
        }
        public static void PlayerExit(Player player)
        {
            Console.Clear();
            Console.WriteLine("**** YOU EXITED THE CASTLE! ****");
            Console.WriteLine($"\n\tYou were in the castle for {player.turns}.");
            Console.WriteLine("\n\tWhen you exited, you had:");
            if(player.orbOfZot == true) {
                Console.Write("\n\t");
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("*** Congratulations, you made it out alive with the Orb Of Zot ***");
                Console.BackgroundColor = ConsoleColor.Black;
            } else
            {
                Console.WriteLine("\n\tYour miserable life");
            }
            Console.WriteLine($"\nYou were a {player.sex} {player.race}.");
            if (player.armor.Length > 0)
            {
                Console.WriteLine($"\nYou wore {player.armor} armor.");
            }
            if (player.weapon.Length > 0)
            {
                Console.WriteLine($"\nYou had a {player.weapon}.");
            }
            Console.WriteLine($"\nYou had {player.gold} Gold Pieces and {player.flares} flares.");
            if (player.lamp == true)
            {
                Console.WriteLine("\nYou had a lamp.");
            }
            if (player.runeStaff == true)
            {
                Console.WriteLine("\nYou had the RuneStaff.");
            }
            if (player.treasures.Count > 0)
            {
                Console.WriteLine($"\nYou also had the following treasures: {string.Join(", ", player.treasures)}");
            }
            GameCollections.ExitCode = 0;
            SharedMethods.WaitForKey();
        }
        public static void CheckIfDead(Player player, ref bool fallThrough)
        {
            if (player.dexterity < 1 || player.intelligence < 1 || player.strength < 1)
            {
                Console.Clear();
                fallThrough = true;
                GameCollections.ExitCode = 0;
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\n**** YOU DIED! ****");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("\n\tWhen you died: ");
                Console.WriteLine($"\nYou were a {player.sex} {player.race}.");
                if (player.armor.Length > 0)
                {
                    Console.WriteLine($"\nYou wore {player.armor} armor.");
                }
                if (player.weapon.Length > 0)
                {
                    Console.WriteLine($"\nYou had a {player.weapon}.");
                }
                Console.WriteLine($"\nYou had {player.gold} Gold Pieces and {player.flares} flares.");
                if (player.lamp == true)
                {
                    Console.WriteLine("\nYou had a lamp.");
                }
                if (player.runeStaff == true)
                {
                    Console.WriteLine("\nYou had the RuneStaff.");
                }
                if (player.treasures.Count > 0)
                {
                    Console.WriteLine($"\nYou also had the following treasures: {string.Join(", ", player.treasures)}");
                }
                Console.WriteLine($"\nYou * were * alive for {player.turns} turns.");
                SharedMethods.WaitForKey();
            }
        }
    }
}
