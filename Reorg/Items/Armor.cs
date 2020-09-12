using System;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Items {
        public interface IArmor : IItem {
            public int DamageAbsorb { get; }
            public int Cost { get; }
            public int IntialCost { get; }
        }

        
        private class ArmorImpl : Item, IArmor {
            public int DamageAbsorb { get; }
            public int Cost { get; }
            public int IntialCost => DamageAbsorb * 10;
            public ArmorImpl(string name, int cost, int damageAbsorb) : base(name, ItemType.Armor) {
                Cost = cost;
                DamageAbsorb = damageAbsorb;
            }
            // public readonly static Weapon Unarmed = new Weapon("Unarmed", 0, 0);

        }
    }
}
