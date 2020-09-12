using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Armor : Item {
        public int DamageAbsorb { get; }
        public int Cost { get; }
        private Armor(string name, int cost, int damageAbsorb) : base(name, ItemType.Armor) {
            Cost = cost;
            DamageAbsorb = damageAbsorb;
        }
        // public readonly static Weapon Unarmed = new Weapon("Unarmed", 0, 0);

        public static readonly Armor Plate = new Armor("Plate", 2500, 3);

        public static Armor[] AllArmor = new Armor[] {
            new Armor("Leather", 1500, 1),
            new Armor("ChainMail", 2000, 2),
            Plate
        };
    }
}
