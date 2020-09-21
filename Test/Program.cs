using System;
using System.Threading.Tasks;
using WizardCastle;

namespace Test {
    class Program {
        static void Test1() {
            var view = new ConsoleView();
            view.SetColor(ConsoleColor.Red).WriteLine("Hello World!").ResetColors();
            var key = view.ReadChar("Hit any key").Result;
            view.SetColor(ConsoleColor.Green).Write("You hit:").SetColor(ConsoleColor.Yellow).Write($"{key}").ResetColors();
            var result = view.Menu("Foo", new string[] { "Yes", "No" }).Result.Item2;
            view.SetColor(ConsoleColor.Green).Write("You hit:").SetColor(ConsoleColor.Yellow).Write($"{result}").ResetColors();
        }

        static void Main(string[] args) {
            Test1();
            Console.WriteLine("Done");
        }
    }
}
