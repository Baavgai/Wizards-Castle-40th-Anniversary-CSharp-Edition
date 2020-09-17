using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Util {
        public static Tuple<char, string> Menu(string question, IEnumerable<string> choices, bool showChoices = false, Func<ConsoleKeyInfo, char> translate = null) =>
            Menu(question, choices, (x, _) => x[0], showChoices, translate);

        public static Tuple<char, T> Menu<T>(string question, IEnumerable<T> choices, bool showChoices = false, Func<ConsoleKeyInfo, char> translate = null) where T : IHasName =>
            Menu(question, choices, (x, _) => x.Name[0], showChoices, translate);

        public static Tuple<char, T> Menu<T>(string question, IEnumerable<T> choices, Func<T, int, char> getKey, bool showChoices = false, Func<ConsoleKeyInfo, char> translate = null) =>
            Menu(question, new Dictionary<char, T>(choices.Select((x, i) => new KeyValuePair<char, T>(getKey(x, i), x))), showChoices, translate);

        public static Tuple<char, T> Menu<T>(string question, IDictionary<char, T> choices, bool showChoices = false, Func<ConsoleKeyInfo, char> translate = null) {
            var lookup = new Dictionary<char, T>(choices.Select(x => new KeyValuePair<char, T>(Char.ToUpper(x.Key), x.Value)));
            if (showChoices) { ShowFullChoices(); } else { ShowPrompt(); }
            
            while (true) {
                var keyChar = translate == null ? ReadChar() : ReadChar(translate);
                WriteLine($"{keyChar}");

                if (keyChar == '?') {
                    ShowFullChoices();
                } else if (!lookup.ContainsKey(keyChar)) {
                    WriteLine();
                    WriteLine(Game.RandErrorMsg());
                    ShowPrompt();
                } else {
                    WriteLine();
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
