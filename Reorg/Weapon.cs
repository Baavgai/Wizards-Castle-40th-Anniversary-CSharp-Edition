using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Weapon : Item {
        public readonly static Weapon Dagger = new Weapon("Dagger", 1500, 1);
        public readonly static Weapon Mace = new Weapon("Mace", 2000, 2);
        public readonly static Weapon Sword = new Weapon("Sword", 2500, 3);

        private readonly static List<Weapon> all = new List<Weapon>();
        public static Weapon[] All => all.ToArray();

        public int BaseDamage { get; }
        public int Cost { get; }
        public int IntialCost => BaseDamage * 10;
        private Weapon(string name, int cost, int baseDamage) : base(name, ItemType.Weapon) {
            Cost = cost;
            BaseDamage = baseDamage;
            all.Add(this);
        }
        public int CalcDamage() => Util.RandInt(1, 6) + BaseDamage;


    }
}

