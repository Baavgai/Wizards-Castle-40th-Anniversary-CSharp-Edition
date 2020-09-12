using System;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Items {
        public interface IArmor : IItem {
            public int DamageAbsorb { get; }
            public int Cost { get; }
        }

        public static readonly IArmor Leather = new ArmorImpl("Leather", 1500, 1);
        public static readonly IArmor ChainMail = new ArmorImpl("ChainMail", 2000, 2);
        public static readonly IArmor Plate = new ArmorImpl("Plate", 2500, 3);

        public static IArmor[] AllArmor = new IArmor[] {
            Leather, ChainMail, Plate
        };


        private class ArmorImpl : Item, IArmor {
            public int DamageAbsorb { get; }
            public int Cost { get; }
            public ArmorImpl(string name, int cost, int damageAbsorb) : base(name, ItemType.Armor) {
                Cost = cost;
                DamageAbsorb = damageAbsorb;
            }
            // public readonly static Weapon Unarmed = new Weapon("Unarmed", 0, 0);

        }
    }
}
