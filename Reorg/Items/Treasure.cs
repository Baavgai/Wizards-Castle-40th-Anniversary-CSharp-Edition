using System;
using System.Collections.Generic;
using System.Linq;


namespace WizardCastle {
    class Treasure : Item, IHasOnEntry, IHasExec {
        private readonly static List<Treasure> all = new List<Treasure>();
        public static Treasure[] All => all.ToArray();
        public static readonly Treasure BlueFlame = all.Register(new Treasure("The Blue Flame",
            "dissolves books stuck to your hands",
            BasicCurseRemoval(Curse.BookStuck, "The Blue Flame burns the book off your hands!")));

        public static readonly Treasure GreenGem = all.Register(new Treasure("The Green Gem",
            "wards off the curse of Forgetfulness",
            BasicCurseRemoval(Curse.Forgetfulness, "The Green Gem cures your forgetfulness!")));

        public static readonly Treasure NornStone = all.Register(new Treasure("The Norn Stone"));

        public static readonly Treasure OpalEye = all.Register(new Treasure("The Opal Eye",
            "cures blindness",
            BasicCurseRemoval(Curse.Blind, "The Opal Eye cures your blindness!")));

        public static readonly Treasure Palantir = all.Register(new Treasure("The Palantir"));

        public static readonly Treasure PalePearl = all.Register(new Treasure("The Pale Pearl",
            "wards off the curse of the Leech",
            BasicCurseRemoval(Curse.Leech, "The Pale Pearl heals the curse of the Leech!")));

        public static readonly Treasure RedRuby = all.Register(new Treasure("The Ruby Red",
            "wards off the curse of Lethargy",
            BasicCurseRemoval(Curse.Lethargy, "The Ruby Red cures your Lethargy!")));

        public static readonly Treasure Silmaril = all.Register(new Treasure("The Silmaril"));

        private static Action<State> BasicCurseRemoval(Curse curse, string message) =>
            state => {
                if (state.Player.HasItem(curse)) {
                    state.Player.Remove(curse);
                    Util.WriteLine(message, bgColor: ConsoleColor.DarkGray);
                }
            };



        private readonly Action<State> update;
        
        private Treasure(string name) : base(name) {
            Description = "has no special power";
        }

        private Treasure(string name, string desc, Action<State> update) : base(name) {
            Description = desc;
            this.update = update;
        }
        public string Description { get; }

        public void OnEntry(State state) {
            // Game.DefaultItemMessage(this);
            Util.WriteLine($"You've found the {Name}, it's yours!");
            state.Player.Add(this);
            state.CurrentCell.Clear();
        }

        public void Exec(State state) => update?.Invoke(state);

    }
}

