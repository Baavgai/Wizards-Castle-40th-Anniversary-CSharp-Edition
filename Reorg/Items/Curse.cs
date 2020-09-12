using System;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Items {

        public interface ICurse : IItem, IHasExec { }

        public static readonly ICurse CurseBlind = new CurseImpl("Blind");
        public static readonly ICurse CurseBookStuck = new CurseImpl("Book-Stuck-To-Hands");
        public static readonly ICurse CurseForgetfulness = new CurseImpl("Forgetfulness", s => {
            Util.WriteLine("You forget something.");
            Game.HideMap(s, s.Map.RandPos());
        });
        public static readonly ICurse CurseLeech = new CurseImpl("Leech", s => {
            var x = Util.RandInt(3);
            if (x>0) {
                s.Player.Strength -= x;
                Util.WriteLine("Curse of the leech makes you weaker.");
            }
        });
        public static readonly ICurse CurseLethargy = new CurseImpl("Lethargy");

        public static readonly ICurse[] AllCurses = new ICurse[] {
            CurseBlind, CurseBookStuck, CurseForgetfulness, CurseLeech, CurseLethargy
        };
        


        private class CurseImpl : Item, ICurse {
            private readonly Action<State> exec;
            public CurseImpl(string name, Action<State> exec = null) : base(name, ItemType.Curse) {
                this.exec = exec;
            }
            public void Exec(State state) => exec?.Invoke(state);
        }
    }
}

/*
 *     // Console.WriteLine($"Cursed with='{string.Join(" ", new string[] { 
        player.blind? "Blind" : "", 
            player.bookStuck? "Book-Stuck-To-Hands" : "", 
            player.forgetfulness? "Forgetfulness" : "", 
player.leech? "Leech" : "", 
player.lethargy? "Lethargy" : "" }).Trim().Replace("  ", " ")}'");

 *         static void CheckCurses(ref Player player, ref string[,,] knownMap)
        {
            if (player.treasures.Contains("The Ruby Red") && player.lethargy == true)
            {
                player.lethargy = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Ruby Red cures your Lethargy!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.treasures.Contains("The Pale Pearl") && player.leech == true)
            {
                player.leech = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Pale Pearl heals the curse of the Leech!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.leech == true)
            {
                player.DecStrength(new Random().Next(0, 3));
            }
            if (player.treasures.Contains("The Green Gem") && player.forgetfulness == true)
            {
                player.forgetfulness = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Green Gem cures your forgetfulness!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.forgetfulness == true)
            {
                int[] forgetRoom = Map.ForgetMapRoom(knownMap);
                knownMap[forgetRoom[0], forgetRoom[1], forgetRoom[2]] = "X";
            }
            if (player.treasures.Contains("The Opal Eye") && player.blind == true)
            {
                player.blind = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Opal Eye cures your blindness!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
            if (player.treasures.Contains("The Blue Flame") && player.bookStuck == true)
            {
                player.bookStuck = false;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("The Blue Flame burns the book off your hands!");
                Console.BackgroundColor = ConsoleColor.Black;
                SharedMethods.WaitForKey();
            }
        }

 * */
