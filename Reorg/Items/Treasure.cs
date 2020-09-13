using System;
using System.Collections.Generic;
using System.Linq;


namespace WizardCastle {
    internal static partial class Items {
        public interface ITreasure : IItem, IHasOnEntry, IHasExec { }

        public static readonly ITreasure RuneStaff = new TreasureImpl("Rune Staff");

        public static readonly ITreasure[] AllTreasures = new ITreasure[] {
            new TreasureImpl("The Blue Flame", state => {
                if (state.Player.HasItem(Curse.BookStuck)) {
                    state.Player.Remove(Curse.BookStuck);
                    Util.WriteLine("The Blue Flame burns the book off your hands!", bgColor: ConsoleColor.DarkGray);
                    Util.WaitForKey();
                }
            }),
            new TreasureImpl("The Green Gem"),
            new TreasureImpl("The Norn Stone"),
            new TreasureImpl("The Opal Eye"),
            new TreasureImpl("The Palantir"),
            new TreasureImpl("The Pale Pearl"),
            new TreasureImpl("The Ruby Red"),
            new TreasureImpl("The Silmaril"),
            RuneStaff
    };



        private class TreasureImpl : Item, ITreasure {
            private readonly Action<State> update;
            public TreasureImpl(string name, Action<State> update = null) : base(name, ItemType.Treasure) {
                this.update = update;
            }

            public void OnEntry(State state) {
                // Game.DefaultItemMessage(this);
                Util.WriteLine($"You've found the {Name}, it's yours!");
                state.Player.Add(this);
                state.CurrentCell.Clear();
            }

            public void Exec(State state) => update?.Invoke(state);

            /*
             *             "•	The Ruby Red - wards off the curse of Lethargy.",
                "•	The Norn Stone - has no special power.",
                "•	The Pale Pearl - wards off the curse of the Leech.",
                "•	The Opal Eye - cures blindness.",
                "•	The Green Gem - wards off the curse of Forgetfulness.",
                "•	The Blue Flame - dissolves books stuck to your hands.",
                "•	The Palantir - has no special power.",
                "•	The Silmaril - has no special power.",

             *         static void CheckCurses(ref Player player, ref string[,,] knownMap)
            {
                if (player.treasures.Contains("The Ruby Red") && player.lethargy == true)
                {
                    player.lethargy = false;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("The Ruby Red cures your Lethargy!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Util.WaitForKey();
                }
                if (player.treasures.Contains("The Pale Pearl") && player.leech == true)
                {
                    player.leech = false;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("The Pale Pearl heals the curse of the Leech!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Util.WaitForKey();
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
                    Util.WaitForKey();
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
                    Util.WaitForKey();
                }
                if (player.treasures.Contains("The Blue Flame") && player.bookStuck == true)
                {
                    player.bookStuck = false;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("The Blue Flame burns the book off your hands!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Util.WaitForKey();
                }
            }

            */


        }
    }
}
