using System;
using System.Collections.Generic;

namespace WizardCastle {
    // public interface ICurse : IItem, IHasExec { }
    class Curse : Item, IHasExec, IInventoryItem {
        private readonly static List<Curse> all = new List<Curse>();
        public static readonly Curse Blind = all.Register(new Curse("Blind"));
        public static readonly Curse BookStuck = all.Register(new Curse("Book-Stuck-To-Hands"));
        public static readonly Curse Forgetfulness = all.Register(new Curse("Forgetfulness", s => {
            Util.WriteLine("You forget something.");
            Game.HideMapCell(s, s.Map.RandPos());
        }));
        public static readonly Curse Leech = all.Register(new Curse("Leech", s => {
            var x = Util.RandInt(3);
            if (x > 0) {
                s.Player.Strength -= x;
                Util.WriteLine("Curse of the leech makes you weaker.");
            }
        }));
        public static readonly Curse Lethargy = all.Register(new Curse("Lethargy"));

        public static Curse[] All => all.ToArray();
        private readonly Action<State> exec;
        private Curse(string name, Action<State> exec = null) : base(name) {
            this.exec = exec;
        }
        public void Exec(State state) => exec?.Invoke(state);
        public void OnFound(State state) {
            Util.WriteLine($"You are cursed with {Name}!");
            state.Player.Add(this);
        }
    }
}
