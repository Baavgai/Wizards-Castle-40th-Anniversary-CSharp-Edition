using System.Collections.Generic;

namespace WizardCastle {
    class Race {
        public string RaceName { get; private set; }
        public int Dexterity { get; private set; }
        public int Intelligence { get; private set; }
        public int Strength { get; private set; }

        public override string ToString() => RaceName;

        public static Race[] AllRaces = new List<Race> {
            new Race { RaceName = "Dwarf", Dexterity = 6, Intelligence = 8, Strength = 10},
            new Race { RaceName = "Elf", Dexterity = 10, Intelligence = 8, Strength = 6},
            new Race { RaceName = "Hobbit", Dexterity = 12, Intelligence = 8, Strength = 4},
            new Race { RaceName = "Homo-Sapien", Dexterity = 8, Intelligence = 8, Strength = 8}
        }.ToArray();

    }
}
