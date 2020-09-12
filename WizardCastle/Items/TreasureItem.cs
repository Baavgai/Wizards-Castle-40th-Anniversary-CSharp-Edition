using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace WizardCastle {
    class TreasureItem : Item {
        private readonly Action<State> update;
        private TreasureItem(string name, Action<State> update = null) : base(name, ItemType.Treasure) {
            this.update = update;
        }

        public void Exec(State state) => update?.Invoke(state);

        public static readonly List<TreasureItem> AllTreasures = new List<TreasureItem>() {
            new TreasureItem("The Blue Flame", state => {
                if (state.Player.bookStuck) {
                    state.Player.bookStuck = false;
                    Util.WriteLine("The Blue Flame burns the book off your hands!", bgColor: ConsoleColor.DarkGray);
                    Util.WaitForKey();
                }
            }),
            new TreasureItem("The Green Gem"),
            new TreasureItem("The Norn Stone"),
            new TreasureItem("The Opal Eye"),
            new TreasureItem("The Palantir"),
            new TreasureItem("The Pale Pearl"),
            new TreasureItem("The Ruby Red"),
            new TreasureItem("The Silmaril"),
    };

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
