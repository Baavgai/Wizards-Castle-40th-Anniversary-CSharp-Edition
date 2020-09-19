using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    internal static partial class Game {

        private static void StartupSplash() {
            Console.SetWindowPosition(0, 0);
            System.Console.WindowHeight = System.Console.LargestWindowHeight - 25;
            System.Console.WindowWidth = System.Console.LargestWindowWidth - 50;

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
            Util.WriteLine("Modified: 2020-09-07 by Daniel Kill", ConsoleColor.Gray);

            Util.Write("\n\n\tC# modifed version written by ", ConsoleColor.DarkYellow);
            Util.Write("Baavgai", ConsoleColor.Red);
            var lastModified = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;
            Util.Write($"\n\tModified: {lastModified:yyyy-MM-dd} by Baavgai\n\n", ConsoleColor.Gray);
        }


        public static State Startup() {
            StartupSplash();
            Util.WaitForKey();
            Util.ClearScreen();

            if (Util.Menu("Would you like to view the instructions", new Dictionary<char, string> {
                {'Y', "View the Instructions"},                {'N', "Start the Game"}
            }, showChoices: true).Item1 == 'Y') {
                ShowInstructions();
            }

            var map = new Map(Util.Menu("Would you like the standard 8x8x8 map or a random map",
                new string[] { "Standard 8x8x8 Map", "Random Map" }, showChoices: true).Item1 == 'R');
            Util.ClearScreen();

            var state = new State(map, InitPlayer());
            Util.ClearScreen();

            Util.WriteLine($"\tOk, {state.Player.Race}, you are now entering Zot's castle!\n");
            return state;
        }

        public static void GoodBye() {
            Util.ClearScreen();
            Console.WriteLine("\t\tThank you for playing Wizard's Castle!");
            Console.ForegroundColor = ConsoleColor.White;
            Util.WaitForKey();
        }

        public static void PlayerExit(State state) {
            (var player, _) = state;
            Util.ClearScreen();
            Util.WriteLine("**** YOU EXITED THE CASTLE! ****");
            Util.WriteLine($"\n\tYou were in the castle for {state.Turn}.");
            Util.WriteLine("\n\tWhen you exited, you had:");
            if (player.HasItem(Zot.Instance)) {
                Util.Write("\n\t");
                Util.WriteLine("*** Congratulations, you made it out alive with the Orb Of Zot ***", bgColor: ConsoleColor.DarkMagenta);
                Util.ResetColors();
            } else {
                Util.WriteLine("\n\tYour miserable life");
            }
            Util.WriteLine($"\nYou were a {player.Gender} {player.Race}.");
            Util.WriteLine($"\nYou wore {player.Armor?.ToString() ?? "no" } armor.");
            Util.WriteLine($"\nYou had a {player.Weapon?.ToString() ?? "weaponless hand" }.");
            Util.WriteLine($"\nYou had {player.Gold} Gold Pieces and {player.Flares} flares.");
            // if (player.Lamp == true) {                Console.WriteLine("\nYou had a lamp.");            }
            if (player.HasItem(RuneStaff.Instance)) {
                Util.WriteLine("\nYou had the RuneStaff.");
            }
            // if (player.treasures.Count > 0) {                Console.WriteLine($"\nYou also had the following treasures: {string.Join(", ", player.treasures)}");            }
            // GameCollections.ExitCode = 0;
            // SharedMethods.WaitForKey();
        }


    }
}