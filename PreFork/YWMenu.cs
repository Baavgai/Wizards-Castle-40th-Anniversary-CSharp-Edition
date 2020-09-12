// YWMenu - YourWishIsMine's Menu
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YWMenuNS
{
    class YWMenu
    {
        public static Char playerDirection;
        public string[] Menu(string question, Dictionary<char, string> choices, List<string> errorMessages)
        {
            Random rand = new Random();
            ConsoleKeyInfo keyPressed;
            string[] result = { "***ERROR***", ""};
            string message = "";
            string regExPattern = @"[0-9a-zA-Z\?]";
            Regex regEx = new Regex(regExPattern);
            do
            {
                if (message != "")
                {
                    Console.WriteLine("\n" + message);
                }
                result[0] = "***ERROR***";
                Console.WriteLine();
                foreach (KeyValuePair<char, string> item in choices)
                {
                    Console.Write("[{0}], ", item.Key);
                }
                Console.Write("[?]\n");
                Console.Write("{0}: ", question);
                do
                {
                    keyPressed = Console.ReadKey(true);
                    playerDirection = keyPressed.KeyChar;
                }
                while (!(regEx.IsMatch(keyPressed.KeyChar.ToString())));
                Console.WriteLine();
                foreach (KeyValuePair<char, string> item in choices)
                {
                    if (keyPressed.KeyChar.ToString().ToLower() == item.Key.ToString().ToLower())
                    {
                        result[0] = item.Key.ToString();
                        result[1] = item.Value;
                    }
                }
                if (keyPressed.KeyChar.ToString() == "?")
                {
                    result[0] = "HELP";
                    message = "";
                    Console.WriteLine();
                    foreach (KeyValuePair<char, string> item in choices)
                    {
                        Console.WriteLine("[{0}] - {1}", item.Key, item.Value);
                    }
                }
                if (result[0] == "***ERROR***")
                {
                    message = errorMessages[rand.Next(0, errorMessages.Count)];
                }
            } while (result[0] == "***ERROR***" || result[0] == "HELP");
            return result;
        }
    }
}
