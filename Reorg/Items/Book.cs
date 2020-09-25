using System;
using System.Collections.Generic;


namespace WizardCastle {

    class Book : Item, IBook {
        private static Lazy<Book> instance = new Lazy<Book>(() => new Book());
        public static Book Instance => instance.Value;

        private Book() : base("Book") { }

        public void OnEntry(State state) => Game.DefaultItemMessage(state, this);

        public void Open(State state) {
            if (!state.Player.IsBlind) {
                state.CurrentCell.Clear();
                state.WriteLine($"\nYou open the book and {Util.RandPick(AllHandlers)(state)}");
            } else {
                state.WriteLine($"Sorry, {state.Player.Race}, it's not written in Braille!");
            }
            // Util.WaitForKey();
        }


        private static readonly Func<State, string>[] AllHandlers = new Func<State, string>[] {
            s => "it's another volume of Zot's poetry. Yeech!",
            s => $"it's an old copy of play {Game.RandRace()}.",
            s => $"it's a {Util.RandPick(MonsterFactory.All)} cook book.",
            s => $"it's a self-improvement book on how to be a better {Game.RandRace()}.",
            s => {
                s.Player.Dexterity = Game.MaxAttrib;
                return "it's a manual of dexterity!";
            },
            s => {
                s.Player.Intelligence = Game.MaxAttrib;
                return "it's a manual of intelligence!";
            },
            s => {
                s.Player.Strength = Game.MaxAttrib;
                return "it's a manual of strength!";
            },
            s => {
                var loc = Util.RandPick(s.Map.Search(Content.Gold)).pos;
                return $"it's a treasure map leading to a pile of gold at ({loc.Display}).";
            },
            s => {
                s.Player.Add(Curse.Blind);
                return $"FLASH! OH NO! YOU ARE NOW A BLIND {s.Player.Race}!";
            },
            s => {
                s.Player.Add(Curse.BookStuck);
                return $"it sticks to your hands. Now you can't grab your {s.Player.Weapon?.Name}!";
            }
        };

    }
}

