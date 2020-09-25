using System.Collections.Generic;

namespace WizardCastle {
    public interface IAbilities {
        int Dexterity { get; }
        int Intelligence { get; }
        int Strength { get; }
    }
    
    public interface IAbilitiesMutable : IAbilities {
        new int Dexterity { get; set; }
        new int Intelligence { get; set; }
        new int Strength { get; set; }
    }

    public class Abilities : IAbilitiesMutable {
        public virtual int Dexterity { get; set; } = 0;
        public virtual int Intelligence { get; set; } = 0;
        public virtual int Strength { get; set; } = 0;

        public Abilities() { }
        public Abilities(int dexterity, int intelligence, int strength) {
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
        }
        public Abilities(IAbilities x) : this(x.Dexterity, x.Intelligence, x.Strength) { }

        public static Abilities operator +(Abilities a, IAbilities b) =>
            new Abilities() {
                Dexterity = a.Dexterity + b.Dexterity,
                Intelligence = a.Intelligence + b.Intelligence,
                Strength = a.Strength + b.Strength
            };
    }
}
