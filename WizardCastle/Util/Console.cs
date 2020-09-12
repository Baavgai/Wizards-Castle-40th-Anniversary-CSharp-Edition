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

    }
}
