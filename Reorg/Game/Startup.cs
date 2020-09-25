using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WizardCastle {
    internal static partial class Game {

        public static void StartupSplash(IView view) {
            // Console.SetWindowPosition(0, 0);
            // Console.WindowHeight = System.Console.LargestWindowHeight - 25;
            // Console.WindowWidth = System.Console.LargestWindowWidth - 50;
            // view.WriteLine($"{view.Width}, {view.Height}");

            view.WriteNewLine().WriteIndent(6)
            .SetColor(ConsoleColor.DarkGreen).Write("Wizard's Castle (")
            .SetColor(ConsoleColor.Magenta).SetBgColor(ConsoleColor.Yellow).Write("40th Anniversary Version")
            .SetColor(ConsoleColor.DarkGreen).SetBgColor(ConsoleColor.Black).WriteLine(")")
            .WriteNewLine(2);

            AuthorSlug("Copyright (C) 1980 by ", "Joseph R Power", "Last Revised - 04/12/80  11:10 PM");
            AuthorSlug("C# version written by", "Daniel Kill", "Modified: 2020-09-07 by Daniel Kill");

            var lastModified = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;
            AuthorSlug("C# modifed version written by", "Baavgai", $"Modified: {lastModified:yyyy-MM-dd} by Baavgai");
            view.WaitForKey();
            view.Clear();

            void AuthorSlug(string copy, string author, string revision) {
                view
                    .WriteNewLine()
                    .SetColor(ConsoleColor.DarkYellow).WriteIndent(2).Write(copy)
                    .SetColor(ConsoleColor.Red).WriteLine($" {author}")
                    .SetColor(ConsoleColor.Gray).WriteIndent(2).WriteLine(revision)
                    .WriteNewLine()
                    ;
            }
        }


        public static State Startup(IView view) {
            StartupSplash(view);

            if (view.AskYN("Would you like to view the instructions", "View the Instructions", "Start the Game", true)) { 
                ShowInstructions(view);
            }

            var map = new Map(view.Menu("Would you like the standard 8x8x8 map or a random map",
                new string[] { "Standard 8x8x8 Map", "Random Map" }, showChoices: true).Item1 == 'R');
            view.Clear();

            var state = new State(view, map, InitPlayer(view));
            view.Clear();

            state.WriteIndent().WriteLine($"Ok, {state.Player.Race}, you are now entering Zot's castle!").WriteNewLine();
            return state;
        }

        public static void GoodBye(IView view) {
            view.Clear();
            view.WriteNewLine().WriteIndent(2).WriteLine("Thank you for playing Wizard's Castle!").ResetColors();
            view.Clear();
        }

        public static void PlayerExit(State state) {
            (var player, _) = state;
            state.Clear();
            state.WriteLine("**** YOU EXITED THE CASTLE! ****");
            state.WriteLine($"\n\tYou were in the castle for {state.Turn}.");
            state.WriteLine("\n\tWhen you exited, you had:");
            if (player.HasItem(Zot.Instance)) {
                state.WriteNewLine().WriteIndent().SetBgColor(ConsoleColor.DarkMagenta)
                    .WriteLine("*** Congratulations, you made it out alive with the Orb Of Zot ***")
                    .ResetColors();
            } else {
                state.WriteNewLine().WriteIndent().WriteLine("Your miserable life");
            }
            state.WriteLine($"\nYou were a {player.Gender} {player.Race}.");
            state.WriteLine($"\nYou wore {player.Armor?.ToString() ?? "no" } armor.");
            state.WriteLine($"\nYou had a {player.Weapon?.ToString() ?? "weaponless hand" }.");
            state.WriteLine($"\nYou had {player.Gold} Gold Pieces and {player.Flares} flares.");
            // if (player.Lamp == true) {                Console.WriteLine("\nYou had a lamp.");            }
            if (player.HasItem(RuneStaff.Instance)) {
                state.WriteLine("\nYou had the RuneStaff.");
            }
            // if (player.treasures.Count > 0) {                Console.WriteLine($"\nYou also had the following treasures: {string.Join(", ", player.treasures)}");            }
            // GameCollections.ExitCode = 0;
            // SharedMethods.WaitForKey();
        }


    }
}

/*
 *         public static void StartupSplash() {
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
            state.WriteLine("Modified: 2020-09-07 by Daniel Kill", ConsoleColor.Gray);

            Util.Write("\n\n\tC# modifed version written by ", ConsoleColor.DarkYellow);
            Util.Write("Baavgai", ConsoleColor.Red);
            var lastModified = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;
            Util.Write($"\n\tModified: {lastModified:yyyy-MM-dd} by Baavgai\n\n", ConsoleColor.Gray);
        }

 * */
