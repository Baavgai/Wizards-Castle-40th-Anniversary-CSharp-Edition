using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Weapon : Item {
        public int BaseDamage { get; }
        public int Cost { get; }
        private Weapon(string name, int cost, int baseDamage) : base(name, ItemType.Weapon) {
            Cost = cost;
            BaseDamage = baseDamage;
        }
        public int CalcDamage() => Util.RandInt(1, 6) + BaseDamage;

        public readonly static Weapon Sword = new Weapon("Sword", 2500, 3);

        public static Weapon[] AllWeapons = new Weapon[] {
            new Weapon("Dagger", 1500, 1),
            new Weapon("Mace", 2000, 2),
            Sword
        };

    }
}
