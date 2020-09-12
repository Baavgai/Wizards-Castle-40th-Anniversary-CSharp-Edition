using System;
using System.Text.RegularExpressions;

namespace The_Wizard_s_Castle
{
    public static class SharedMethods
    {
        public static void WaitForKey()
        {
            Console.WriteLine("\nPress ENTER to continue");
            ConsoleKeyInfo keyPressed;
            string regExPattern = @"[0-9a-zA-Z\?]";
            Regex regEx = new Regex(regExPattern);
            do
            {
                keyPressed = Console.ReadKey(true);
            }
            while ((!(regEx.IsMatch(keyPressed.KeyChar.ToString()))) && keyPressed.KeyChar != (char)13);
        }
    }
}
