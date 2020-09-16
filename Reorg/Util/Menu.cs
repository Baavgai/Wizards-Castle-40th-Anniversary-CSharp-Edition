using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Util {
        public static Tuple<char, string> Menu(string question, IEnumerable<string> choices, bool showChoices = false) =>
            Menu(question, choices, (x, _) => x[0], showChoices);

        public static Tuple<char, T> Menu<T>(string question, IEnumerable<T> choices, bool showChoices = false) where T : IHasName =>
            Menu(question, choices, (x, _) => x.Name[0], showChoices);

        public static Tuple<char, T> Menu<T>(string question, IEnumerable<T> choices, Func<T, int, char> getKey, bool showChoices = false) =>
            Menu(question, new Dictionary<char, T>(choices.Select((x, i) => new KeyValuePair<char, T>(getKey(x, i), x))), showChoices);

        public static Tuple<char, T> Menu<T>(string question, IDictionary<char, T> choices, bool showChoices = false) {
            var lookup = new Dictionary<char, T>(choices.Select(x => new KeyValuePair<char, T>(Char.ToUpper(x.Key), x.Value)));
            if (showChoices) { ShowFullChoices(); } else { ShowPrompt(); }
            
            while (true) {
                var keyChar = ReadChar();

                if (keyChar == '?') {
                    ShowFullChoices();
                } else if (!lookup.ContainsKey(keyChar)) {
                    WriteLine();
                    WriteLine(Game.RandErrorMsg());
                    ShowPrompt();
                } else {
                    return new Tuple<char, T>(keyChar, lookup[keyChar]);
                }
            }
            void ShowFullChoices() {
                WriteLine();
                foreach (var item in lookup) {
                    WriteLine($"[{item.Key}] - {item.Value}");
                }
                Write($"{question}: ");
            }
            void ShowPrompt() {
                WriteLine();
                foreach (var item in lookup) {
                    Write($"[{item.Key}], ");
                }
                Write($"[?]\n{question}: ");
            }
        }

    }
}

/*
 *         public static string Menu(string question, IEnumerable<string> choices) =>
            Menu(question, choices, (x, _) => x[0], Game.ErrorMesssages);
        public static T Menu<T>(string question, IEnumerable<T> choices) where T : IHasName =>
            Menu(question, choices, (x, _) => x.Name[0], Game.ErrorMesssages);

        public static T Menu<T>(string question, IEnumerable<T> choices, Func<T, int, char> getKey) =>
            Menu(question, choices, getKey, Game.ErrorMesssages);

        public static T Menu<T>(string question, IEnumerable<T> choices, Func<T, int, char> getKey, IEnumerable<string> errorMessages) =>
            Menu(question, new Dictionary<char, T>(choices.Select((x, i) => new KeyValuePair<char, T>(getKey(x, i), x))), errorMessages);

        public static T Menu<T>(string question, IDictionary<char, T> choices) =>
            Menu(question, choices, Game.ErrorMesssages);

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

    }
 * 
 * */
