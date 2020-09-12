using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Util {
        public const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
        public const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;


        public static void WriteLine(string s = "") => Console.WriteLine(s);
        public static void Write(string s = "") => Console.Write(s);

        public static void Write(string s, ConsoleColor fgColor = DefaultForegroundColor, ConsoleColor bgColor = DefaultBackgroundColor) {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Write(s);
        }

        public static void WriteLine(string s, ConsoleColor fgColor = DefaultForegroundColor, ConsoleColor bgColor = DefaultBackgroundColor) {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            WriteLine(s);
        }

        public static void UserContintue(string msg = null) {
            WriteLine(msg ?? "Press ENTER to continue.");
            Console.ReadLine();
        }

        public static void ClearScreen() => System.Console.Clear();

        public static void WriteLines(IEnumerable<string> lines, bool clear = true) {
            int counter = 0;
            if (clear) { System.Console.Clear(); }
            foreach (string item in lines) {
                if (counter < Console.WindowHeight - 8) {
                    Console.WriteLine(item);
                } else {
                    counter = 0;
                    UserContintue();
                    ClearScreen();
                    Console.WriteLine(item);
                }
                counter += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(item.Length) / Convert.ToDouble(Console.WindowWidth)));
            }
            UserContintue();
        }
    }
}
