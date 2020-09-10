using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace The_Wizard_s_Castle {
    internal static partial class Util {
        public static readonly Random Rand = new Random();

        public static void WriteLine(string s = "") => Console.WriteLine(s);
        public static void Write(string s = "") => Console.Write(s);

        public static T RandPick<T>(IEnumerable<T> xs) => xs.Skip(Rand.Next(xs.Count())).First();

        private static IEnumerable<string> DefaultErrorMessages =>
            ManipulateListObjects.ReplaceRandomMonster(GameCollections.ErrorMesssages);

        public static string Menu(string question, IEnumerable<string> choices) =>
            Menu(question, choices, (x, _) => x[0], DefaultErrorMessages);

        public static T Menu<T>(string question, IEnumerable<T> choices, Func<T, int, char> getKey) =>
            Menu(question, choices, getKey, DefaultErrorMessages);

        public static T Menu<T>(string question, IEnumerable<T> choices, Func<T, int, char> getKey, IEnumerable<string> errorMessages) =>
            Menu(question, new Dictionary<char, T>(choices.Select((x, i) => new KeyValuePair<char, T>(getKey(x, i), x))), errorMessages);

        public static T Menu<T>(string question, IDictionary<char, T> choices) =>
            Menu(question, choices, DefaultErrorMessages);


        public static T Menu<T>(string question, IDictionary<char, T> choices, IEnumerable<string> errorMessages) {
            var lookup = new Dictionary<char, T>(choices.Select(x => new KeyValuePair<char, T>(Char.ToUpper(x.Key), x.Value)));
            ShowPrompt();
            while (true) {
                var keyChar = Char.ToUpper(Console.ReadKey(true).KeyChar);

                if (keyChar == '?') {
                    WriteLine();
                    foreach (var item in lookup) {
                        WriteLine($"[{item.Key}] - {item.Value}");
                    }
                } else if (!lookup.ContainsKey(keyChar)) {
                    WriteLine();
                    WriteLine(RandPick(errorMessages));
                    ShowPrompt();
                } else {
                    return lookup[keyChar];
                }
            }
            void ShowPrompt() {
                WriteLine();
                foreach (var item in lookup) {
                    Write($"[{item.Key}], ");
                }
                Write($"[?]\n{question}: ");
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
            }
            while ((!(regEx.IsMatch(keyPressed.KeyChar.ToString()))) && keyPressed.KeyChar != (char)13);
        }
    }
}
