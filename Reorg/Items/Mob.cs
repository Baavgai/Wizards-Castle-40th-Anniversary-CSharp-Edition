using System.Collections.Generic;

namespace WizardCastle {
    class Mob : Abilities, IItem {
        public string Name { get; }
        public override string ToString() => Name;

        public Mob(string name, int dexterity, int intelligence, int strength) : base(dexterity, intelligence, strength) { 
            Name = name;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
        }
        public Mob(string name) { Name = name; }
    }
}
