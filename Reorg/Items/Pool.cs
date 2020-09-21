using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    class Pool : Item, IContent {
        private static Lazy<Pool> instance = new Lazy<Pool>(() => new Pool());
        public static Pool Instance => instance.Value;

        private Pool() : base("Pool") { }

        public void OnEntry(State state) => Game.DefaultItemMessage(state, this);

        public void Drink(State state) {
            var effect = Util.RandPick(Effects.Value);
            state.WriteLine($"\nYou drink from the pool and {effect(state.Player)}");
        }

        private static readonly Lazy<List<Func<Player, string>>> Effects = new Lazy<List<Func<Player, string>>>(() =>
            new List<Func<Player, string>>() {
                p => {
                    p.Dexterity += Util.RandInt(1, 3);
                    return "you feel nimbler.";
                },
                p => {
                    p.Dexterity -= Util.RandInt(1, 3);
                    return "you feel clumsier.";
                },
                p => {
                    p.Intelligence += Util.RandInt(1, 3);
                    return "you feel smarter.";
                },
                p => {
                    p.Intelligence -= Util.RandInt(1, 3);
                    return "you feel dumber.";
                },
                p => {
                    p.Strength += Util.RandInt(1, 3);
                    return "you feel stronger.";
                },
                p => {
                    p.Strength -= Util.RandInt(1, 3);
                    return "you feel weaker.";
                },
                p => {
                    p.Race = Util.RandPick(Race.All.Where(x => x != p.Race));
                    return $"you turn into a {p.Race}.";
                },
                p => {
                    p.Gender = Util.RandPick(Gender.All.Where(x => x != p.Gender));
                    return $"you are now a {p.Gender} {p.Race}.";
                }
            });
    }
}
