using System.Collections.Generic;

namespace The_Wizard_s_Castle.Models {
    class Mob {
        public string Name { get; }
        public int Dexterity { get; }
        public int Intelligence { get; }
        public int Strength { get; }

        public Mob(string name, int dexterity, int intelligence, int strength) { 
            Name = name;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
        }
    }
}
