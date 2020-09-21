using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WizardCastle {
    public static class ExtIView {

        public static async Task<int> ReadDigit(this IView view, string prompt = null) {
            if (!string.IsNullOrWhiteSpace(prompt)) { view.Write(prompt); }
            int? n = null;
            while (n == null) {
                var x = await view.ReadChar();
                var value = x - '0';
                if (value >= 0 && value < 10) {
                    n = value;
                }
            }
            return n.Value;
        }

        public static async Task WaitForKey(this IView view, string msg = null) {
            if (!string.IsNullOrWhiteSpace(msg)) { view.WriteLine(msg); }
            view.WriteLine("\nPress ENTER to continue");
            bool done = false;
            while (!done) {
                var ch = await view.ReadChar();
                done = ch == (char)13;
            }
        }

        public static async void WriteLines(this IView view, IEnumerable<string> lines, bool clear = true) {
            int counter = 0;
            if (clear) { view.Clear(); }
            foreach (string item in lines) {
                if (counter < Console.WindowHeight - 8) {
                    Console.WriteLine(item);
                } else {
                    counter = 0;
                    await view.WaitForKey();
                    view.Clear();
                    Console.WriteLine(item);
                }
                counter += Convert.ToInt32(Math.Ceiling(Convert.ToDouble(item.Length) / Convert.ToDouble(Console.WindowWidth)));
            }
            await view.WaitForKey();
        }


        public static async Task<Tuple<char, string>> Menu(this IView view, string question, IEnumerable<string> choices, bool showChoices = false) =>
            await Menu(view, question, choices, (x, _) => x[0], showChoices);


        public static async Task<Tuple<char, T>> Menu<T>(this IView view, string question, IEnumerable<T> choices, Func<T, int, char> getKey, bool showChoices = false) =>
            await Menu(view, question, new Dictionary<char, T>(choices.Select((x, i) => new KeyValuePair<char, T>(getKey(x, i), x))), showChoices);

        public static async Task<Tuple<char, T>> Menu<T>(this IView view, string question, IDictionary<char, T> choices, bool showChoices = false) {
            var lookup = new Dictionary<char, T>(choices.Select(x => new KeyValuePair<char, T>(Char.ToUpper(x.Key), x.Value)));
            if (showChoices) { ShowFullChoices(); } else { ShowPrompt(); }

            while (true) {
                var keyChar = await view.ReadChar();
                view.WriteLine($"{keyChar}");

                if (keyChar == '?') {
                    ShowFullChoices();
                } else if (!lookup.ContainsKey(keyChar)) {
                    view.WriteLine().WriteLine("Bad Choice");
                    ShowPrompt();
                } else {
                    view.WriteLine();
                    return new Tuple<char, T>(keyChar, lookup[keyChar]);
                }
            }
            void ShowFullChoices() {
                view.WriteLine();
                foreach (var item in lookup) {
                    view.WriteLine($"[{item.Key}] - {item.Value}");
                }
                view.Write($"{question}: ");
            }
            void ShowPrompt() {
                view.WriteLine();
                foreach (var item in lookup) {
                    view.Write($"[{item.Key}], ");
                }
                view.Write($"[?]\n{question}: ");
            }
        }

    }
}
