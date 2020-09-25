using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Weapon : Item, IWeapon {
        private readonly static List<Weapon> all = new List<Weapon>();
        public readonly static Weapon Dagger = all.Register(new Weapon("Dagger", 1500, 1));
        public readonly static Weapon Mace = all.Register(new Weapon("Mace", 2000, 2));
        public readonly static Weapon Sword = all.Register(new Weapon("Sword", 2500, 3));
        public static Weapon[] All => all.ToArray();

        public int BaseDamage { get; }
        public int Cost(bool vendor = true) => vendor ? vendorCost : InitalCost;
        private readonly int vendorCost;
        private int InitalCost => BaseDamage * 10;
        private Weapon(string name, int vendorCost, int baseDamage) : base(name) {
            this.vendorCost = vendorCost;
            BaseDamage = baseDamage;
        }
        public int CalcDamage() => Util.RandInt(1, 6) + BaseDamage;

        public void OnFound(State state) => state.WriteLine($"You have acquired a {Name}");


    }
}

