using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Curse {
        public string Name { get; private set; }
        public Action<State> Exec { get; private set; }
        private Curse(string name, Action<State> exec) {
            Name = name;
            Exec = exec;
        }
        public static Curse[] AllCurses = new List<Curse> {
            new Curse("Forgetfulness", s => s.Player.forgetfulness = true),
            new Curse("Leech", s => s.Player.leech = true),
            new Curse("Lethargy", s => s.Player.lethargy = true)
        }.ToArray();

    }
}
