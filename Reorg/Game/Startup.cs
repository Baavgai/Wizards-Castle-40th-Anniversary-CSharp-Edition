using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    internal static partial class Game {

        public static void StartupSplash() {
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

            Console.Write("C# modifed version written by ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Baavgai");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n\t");
            Console.ForegroundColor = ConsoleColor.Gray;
            var lastModified = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;

            Console.Write($"Modified: {lastModified:yyyy-MM-dd} by Baavgai\n\n");

        }

        private static Map GetMap() {
            bool randomMap = Util.Menu("Would you like the standard 8x8x8 map or a random map", new Dictionary<char, string> {
                {'S', "Standard 8x8x8 Map"},
                {'R', "Random Map"}
            }).Item1 == 'R';
            System.Console.Clear();
            return new Map(randomMap);
        }

        private static Player GetPlayer() {
            bool randomMap = Util.Menu("Would you like the standard 8x8x8 map or a random map", new Dictionary<char, string> {
                {'S', "Standard 8x8x8 Map"},
                {'R', "Random Map"}
            }).Item1 == 'R';
            System.Console.Clear();
            return new Map(randomMap);
        }

        public static void Startup() {
            Util.UserContintue();
            System.Console.Clear();
            ViewInstructions();

        }
    }
}