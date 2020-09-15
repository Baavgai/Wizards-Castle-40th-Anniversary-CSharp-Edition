using System.Collections.Generic;

namespace WizardCastle {
    class Mob : IItem, IAbilities {
        public string Name { get; }
        public int Dexterity { get; }
        public int Intelligence { get; }
        public int Strength { get; }
        public override string ToString() => Name;

        public Mob(string name, int dexterity, int intelligence, int strength) { 
            Name = name;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
        }
    }
}
