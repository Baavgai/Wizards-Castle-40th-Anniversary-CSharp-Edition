using System;
using System.Collections.Generic;


namespace WizardCastle {

    class Book : Item, IHasOpen, IContent {
        static Book() {
            Content.Register(new Book());
        }

        private Book() : base("Book") { }

        public void OnEntry(State state) => Game.DefaultItemMessage(this);

        public void Open(State state) {
            if (!state.Player.IsBlind) {
                state.CurrentCell.Clear();
                Util.WriteLine($"\nYou open the book and {Util.RandPick(AllHandlers)(state)}");
            } else {
                Util.WriteLine($"Sorry, {state.Player.Race}, it's not written in Braille!");
            }
            Util.WaitForKey();
        }


        private static readonly Func<State, string>[] AllHandlers = new Func<State, string>[] {
            s => "it's another volume of Zot's poetry. Yeech!",
            s => $"it's an old copy of play {Game.RandRace()}.",
            s => $"it's a {Util.RandPick(MonsterFactory.AllMonsters)} cook book.",
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
                var loc = Util.RandPick(Game.SearchMap(s, Content.Gold));
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

