﻿using System.Collections.Generic;

namespace WizardCastle {
    class Race : Mob {
        public int ExtraPoints { get; }
        private Race(string name, int dexterity, int intelligence, int strength, int extraPoints = 8) : base(name, dexterity, intelligence, strength) {
            ExtraPoints = extraPoints;
        }
        public static readonly Race Dwarf = new Race("Dwarf", dexterity: 6, intelligence: 8, strength: 10);
        public static readonly Race Elf = new Race("Elf", dexterity: 10, intelligence: 8, strength: 6);
        public static readonly Race Hobbit = new Race("Hobbit", dexterity: 12, intelligence: 8, strength: 4, extraPoints: 4);
        public static readonly Race HomoSap = new Race("Homo-Sapien", dexterity: 8, intelligence: 8, strength: 8);

        public static readonly Race[] AllRaces = new Race[] { Dwarf, Elf, Hobbit, HomoSap };

    }
}

/*
 *             int extraPoints = 8;
            int[] extraPointsArr = new int[3];
            ConsoleKeyInfo keyPressed;
            string regExPattern = @"[0-9]";
            Regex regEx = new Regex(regExPattern);
            if (race == "Hobbit") {
                extraPoints -= 4; // Hobbits get 4 less extraPoints

using System.Collections.Generic;

namespace WizardCastle {
    class Race {
        public string RaceName { get;  }
        public int Dexterity { get; }
        public int Intelligence { get;  }
        public int Strength { get;  }

        public override string ToString() => RaceName;

        public static readonly Race Dwarf = new Race { RaceName = "Dwarf", Dexterity = 6, Intelligence = 8, Strength = 10},
            new Race { RaceName = "Elf", Dexterity = 10, Intelligence = 8, Strength = 6},
            new Race { RaceName = "Hobbit", Dexterity = 12, Intelligence = 8, Strength = 4},
            new Race { RaceName = "Homo-Sapien", Dexterity = 8, Intelligence = 8, Strength = 8}
        }.ToArray();


        public static Race[] AllRaces = new List<Race> {
            new Race { RaceName = "Dwarf", Dexterity = 6, Intelligence = 8, Strength = 10},
            new Race { RaceName = "Elf", Dexterity = 10, Intelligence = 8, Strength = 6},
            new Race { RaceName = "Hobbit", Dexterity = 12, Intelligence = 8, Strength = 4},
            new Race { RaceName = "Homo-Sapien", Dexterity = 8, Intelligence = 8, Strength = 8}
        }.ToArray();

    }
}

 * */