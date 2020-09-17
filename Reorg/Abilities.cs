using System.Collections.Generic;

namespace WizardCastle {
    interface IAbilities {
        public int Dexterity { get; }
        public int Intelligence { get; }
        public int Strength { get; }
    }
    interface IAbilitiesMutable {
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }
    }

    class Abilities : IAbilities, IAbilitiesMutable {
        public int Dexterity { get; set; } = 0;
        public int Intelligence { get; set; } = 0;
        public int Strength { get; set; } = 0;

        public Abilities() { }
        public Abilities(int dexterity, int intelligence, int strength) {
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
        }
        public Abilities(IAbilities x) : this(x.Dexterity, x.Intelligence, x.Strength) { }
        public Abilities(IAbilitiesMutable x) : this(x.Dexterity, x.Intelligence, x.Strength) { }

        public static Abilities operator +(Abilities a, IAbilities b) =>
            new Abilities() {
                Dexterity = a.Dexterity + b.Dexterity,
                Intelligence = a.Intelligence + b.Intelligence,
                Strength = a.Strength + b.Strength
            };


    }
}
