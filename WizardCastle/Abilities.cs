using System.Collections.Generic;

namespace WizardCastle {
    interface IAbilities {
        public int Dexterity { get; }
        public int Intelligence { get; }
        public int Strength { get; }
    }

    class Abilities : IAbilities {
        public int Dexterity { get; set; } = 0;
        public int Intelligence { get; set; } = 0;
        public int Strength { get; set; } = 0;

        public static Abilities operator +(Abilities a, IAbilities b) =>
            new Abilities() {
                Dexterity = a.Dexterity + b.Dexterity,
                Intelligence = a.Intelligence + b.Intelligence,
                Strength = a.Strength + b.Strength
            };

    }
}
