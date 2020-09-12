using System;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Items {
        public interface IWeapon : IItem {
            public int BaseDamage { get; }
            public int Cost { get; }
            public int CalcDamage();
        }

        public static IWeapon[] AllWeapons = new IWeapon[] {
            Dagger, Mace, Sword
        };


        public readonly static IWeapon Dagger = new WeaponImpl("Dagger", 1500, 1);
        public readonly static IWeapon Mace = new WeaponImpl("Mace", 2000, 2);
        public readonly static IWeapon Sword = new WeaponImpl("Sword", 2500, 3);

        private class WeaponImpl : Item, IWeapon {
            public int BaseDamage { get; }
            public int Cost { get; }
            public WeaponImpl(string name, int cost, int baseDamage) : base(name, ItemType.Weapon) {
                Cost = cost;
                BaseDamage = baseDamage;
            }
            public int CalcDamage() => Util.RandInt(1, 6) + BaseDamage;


        }
    }
}

