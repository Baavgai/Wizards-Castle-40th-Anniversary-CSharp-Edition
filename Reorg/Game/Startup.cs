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
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Modified: 2020-09-07 by Daniel Kill\n\n");

            Util.Write("\n\n\tC# modifed version written by ", ConsoleColor.DarkYellow);
            Util.Write("Baavgai", ConsoleColor.Red);
            var lastModified = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;
            Util.Write($"\n\tModified: {lastModified:yyyy-MM-dd} by Baavgai\n\n", ConsoleColor.Gray);
        }

        private static Map GetMap() {
            /*
            bool randomMap = Util.Menu("Would you like the standard 8x8x8 map or a random map", new Dictionary<char, string> {
                {'S', "Standard 8x8x8 Map"},
                {'R', "Random Map"}
            }).Item1 == 'R';
            Util.ClearScreen();
            return new Map(randomMap);
            */
            return new Map(false);
        }


        public static State Startup() {
            StartupSplash();
            Util.WaitForKey();
            Util.ClearScreen();
            ViewInstructions();
            var m = InitMap(GetMap());
            var state = new State(m, InitPlayer());
            Util.ClearScreen();
            Util.WriteLine($"\tOk, {state.Player.Race}, you are now entering Zot's castle!\n");
            return state;
        }

        public static void GoodBye() {
            Console.Clear();
            Console.WriteLine("\t\tThank you for playing Wizard's Castle!");
            Util.ClearScreen();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nPress ENTER to Exit.");
            Console.ReadLine();
        }

    }
}