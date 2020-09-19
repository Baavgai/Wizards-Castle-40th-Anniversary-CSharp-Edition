using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    class Orb : Item, IContent {
        private static Lazy<Orb> instance = new Lazy<Orb>(() => new Orb());
        public static Orb Instance => instance.Value;

        private Orb() : base("Orb") { }

        public void OnEntry(State state) => Game.DefaultItemMessage(this);

        public void Gaze(State state) {
            var effect = Util.RandPick(Effects.Value);
            Util.WriteLine($"\nYou gaze into the Orb and see {effect(state)}");
        }

        private static readonly Lazy<List<Func<State, string>>> Effects = new Lazy<List<Func<State, string>>>(() =>
        new List<Func<State, string>>() {
                _ => "yourself in a bloody heap.",
                _ => "your mother telling you to clean your room.",
                _ => "a soap opera re-run.",
                _ => "yourself playing The Wizard's Castle.",
                _ => "your life drifting before your eyes.",
                _ => $"yourself in {Util.RandPick(new string[] { "fencing", "religion", "language", "alchemy" })} class.",
                _ => $"a {Util.RandPick(MonsterFactory.All)} gazing back at you.",
                _ => $"a {Util.RandPick(MonsterFactory.All)} eating the flesh from your corpse.",
                _ => $"a {Util.RandPick(MonsterFactory.All)} using your leg-bone as a tooth-pick.",
                s => $"yourself drinking from a pool and becomine a {Util.RandPick(Race.All.Where(x => x != s.Player.Race))}.",
        });
    }
}
