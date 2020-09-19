using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Race : Item, IAbilities {
        private readonly static List<Race> all = new List<Race>();
        public static readonly Race Dwarf = all.Register(new Race("Dwarf", dexterity: 6, intelligence: 8, strength: 10));
        public static readonly Race Elf = all.Register(new Race("Elf", dexterity: 10, intelligence: 8, strength: 6));
        public static readonly Race Hobbit = all.Register(new Race("Hobbit", dexterity: 12, intelligence: 8, strength: 4, extraPoints: 4));
        public static readonly Race HomoSap = all.Register(new Race("Homo-Sapien", dexterity: 8, intelligence: 8, strength: 8));

        public static Race[] All => all.ToArray();

        public int ExtraPoints { get; }
        private Race(string name, int dexterity, int intelligence, int strength, int extraPoints = 8) : base(name) {
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
            ExtraPoints = extraPoints;
        }

        public int Dexterity { get; }
        public int Intelligence { get; }
        public int Strength { get; }

    }
}
