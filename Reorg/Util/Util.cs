using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Util {

        public static void Sleep() {
            for (int i = 0; i < 30; i++) {
                Util.Write(".");
                System.Threading.Thread.Sleep(100);
            }

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
        public static int ReadDigit(string msg = null) {
            if (!string.IsNullOrWhiteSpace(msg)) { WriteLine(msg); }
            int? n = null;
            while(n==null) {
                var x = Console.ReadKey(true);
                var value = x.KeyChar - '0';
                if (value >= 0 && value<10) {
                    n = value;
                }
            }
            return n.Value;
        }
    }
}
