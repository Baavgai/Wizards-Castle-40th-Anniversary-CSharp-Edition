﻿using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Util {
        public const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
        public const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;

        public static void ResetColors() {
            Console.ForegroundColor = DefaultForegroundColor;
            Console.BackgroundColor = DefaultBackgroundColor;
        }

        public static void WriteLine(string s = "") => Console.WriteLine(s);
        public static void Write(string s = "") => Console.Write(s);
        public static void Write(params string[] xs) {
            foreach (var x in xs) {
                Console.Write(x ?? "");
            }
        }
        public static void WriteLine(params string[] xs) {
            foreach (var x in xs) {
                Console.WriteLine(x ?? "");
            }
        }

        public static void UseColor(Action action, ConsoleColor fgColor = DefaultForegroundColor, ConsoleColor bgColor = DefaultBackgroundColor) {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            action();
            ResetColors();
        }

        public static void Write(string s, ConsoleColor fgColor = DefaultForegroundColor, ConsoleColor bgColor = DefaultBackgroundColor) =>
            UseColor(() => Write(s), fgColor, bgColor);


        public static void WriteLine(string s, ConsoleColor fgColor = DefaultForegroundColor, ConsoleColor bgColor = DefaultBackgroundColor) =>
            UseColor(() => WriteLine(s), fgColor, bgColor);

        public static string ReadLine() => Console.ReadLine();

        // public static char ReadCharDefaultTranslate(ConsoleKeyInfo ki) => Char.ToUpper(ki.KeyChar);
        public static char ReadCharDefaultTranslate(ConsoleKeyInfo ki) {
            // System.Diagnostics.Debug.WriteLine(ki.Key == ConsoleKey.);
            if (ki.Key == ConsoleKey.UpArrow) { return 'N'; }
            if (ki.Key == ConsoleKey.DownArrow) { return 'S'; }
            if (ki.Key == ConsoleKey.LeftArrow) { return 'W'; }
            if (ki.Key == ConsoleKey.RightArrow) { return 'E'; }
            return Char.ToUpper(ki.KeyChar);
        }

        public static char ReadChar(Func<ConsoleKeyInfo, char> translate) =>
            translate(Console.ReadKey(true));

        public static char ReadChar() => ReadChar(ReadCharDefaultTranslate);
        // Char.ToUpper(Console.ReadKey(true).KeyChar);


        public static int ReadDigit(string msg = null) {
            if (!string.IsNullOrWhiteSpace(msg)) { WriteLine(msg); }
            int? n = null;
            while (n == null) {
                var x = ReadChar();
                var value = x - '0';
                if (value >= 0 && value < 10) {
                    n = value;
                }
            }
            return n.Value;
        }

        public static void WaitForKey(string msg = null) {
            if (!string.IsNullOrWhiteSpace(msg)) { WriteLine(msg); }
            WriteLine("\nPress ENTER to continue");
            ConsoleKeyInfo keyPressed;
            string regExPattern = @"[0-9a-zA-Z\?]";
            Regex regEx = new Regex(regExPattern);
            do {
                keyPressed = Console.ReadKey(true);
            } while ((!(regEx.IsMatch(keyPressed.KeyChar.ToString()))) && keyPressed.KeyChar != (char)13);
        }



        public static void ClearScreen() {
            ResetColors();
            System.Console.Clear();
        }

        public static void WriteLines(IEnumerable<string> lines, bool clear = true) {
            int counter = 0;
            if (clear) { System.Console.Clear(); }
            foreach (string item in lines) {
                if (counter < Console.WindowHeight - 8) {
                    Console.WriteLine(item);
                } else {
                    counter = 0;
                    WaitForKey();
                    ClearScreen();
                    Console.WriteLine(item);
                }
                counter += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(item.Length) / Convert.ToDouble(Console.WindowWidth)));
            }
            WaitForKey();
        }
    }
}

/*
 * 
 *         public static void WaitForKey(string msg = null) {
            WriteLine(msg ?? "Press any key to continue.");
            ReadChar();
        }

 *         public static void UserContintue(string msg = null) {
            WriteLine(msg ?? "Press ENTER to continue.");
            Console.ReadLine();
        }

        public static void WaitForKey(string msg = null) {
            if (!string.IsNullOrWhiteSpace(msg)) { WriteLine(msg); }
            WriteLine("\nPress ENTER to continue");
            ConsoleKeyInfo keyPressed;
            string regExPattern = @"[0-9a-zA-Z\?]";
            Regex regEx = new Regex(regExPattern);
            do {
                keyPressed = Console.ReadKey(true);
            } while ((!(regEx.IsMatch(keyPressed.KeyChar.ToString()))) && keyPressed.KeyChar != (char)13);
        }

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

 */
