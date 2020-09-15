﻿using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Armor : Item {
        private readonly static List<Armor> all = new List<Armor>();
        public static readonly Armor Leather = all.Register(new Armor("Leather", 1500, 1));
        public static readonly Armor ChainMail = all.Register(new Armor("ChainMail", 2000, 2));
        public static readonly Armor Plate = all.Register(new Armor("Plate", 2500, 3));

        public static Armor[] All => all.ToArray();
        public int DamageAbsorb { get; }
        public int Cost { get; }
        public int IntialCost => DamageAbsorb * 10;
        private Armor(string name, int cost, int damageAbsorb) : base(name, ItemType.Armor) {
            Cost = cost;
            DamageAbsorb = damageAbsorb;
        }

    }
}
