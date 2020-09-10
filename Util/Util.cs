using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace The_Wizard_s_Castle {
    internal static partial class Util {


        public static void WaitForKey(string msg = null) {
            if (!string.IsNullOrWhiteSpace(msg)) { WriteLine(msg); }
            WriteLine("\nPress ENTER to continue");
            ConsoleKeyInfo keyPressed;
            string regExPattern = @"[0-9a-zA-Z\?]";
            Regex regEx = new Regex(regExPattern);
            do {
                keyPressed = Console.ReadKey(true);
            }
            while ((!(regEx.IsMatch(keyPressed.KeyChar.ToString()))) && keyPressed.KeyChar != (char)13);
        }
    }
}
