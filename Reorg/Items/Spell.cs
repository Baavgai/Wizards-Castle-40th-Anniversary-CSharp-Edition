﻿using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Spell : Item {
        private readonly static List<Spell> all = new List<Spell>();
        public readonly static Spell Web = all.Register(new Spell("Web", (s,m) => {
            s.WriteLine($"\nYou've caught the {m.Name} in a web, now it can't attack");
            m.WebbedTurns = Util.RandInt(1, 11);
            s.Player.Strength -= 1;
        }));
        public readonly static Spell Fireball = all.Register(new Spell("Fireball", (s, m) => {
            s.WriteLine($"\nYou blast the {m.Name} with a fireball.");
            m.Strength -= Util.RandInt(1, 11);
            s.Player.Intelligence -= 1;
            s.Player.Strength -= 1;
        }));
        public readonly static Spell Deathspell = all.Register(new Spell("Fireball", (s, monster) => {
            (var player, _) = s;
            if ((player.Intelligence > monster.Intelligence) && (Util.RandInt(1, 9) < 7)) {
                s.WriteLine($"\nDEATH! The {monster.Name} is dead.");
                monster.Strength = 0;
            } else {
                s.WriteLine($"\nDEATH! The STUPID {player.Race}'s death.");
                player.Strength = 0;
                // Util.WaitForKey();
            }
        }));

        public static Spell[] All => all.ToArray();

        private readonly Action<State, Mob> cast;

        private Spell(string name, Action<State, Mob> cast) : base(name) {
            this.cast = cast;
        }
        public void Cast(State state, Mob mob) => cast(state, mob);


    }
}
