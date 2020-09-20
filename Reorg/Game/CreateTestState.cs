using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    internal static partial class Game {
        public static State CreateTestState() {
            var ab = new Abilities() + Race.Elf;
            var player = new Player() {
                Race = Race.Elf,
                Gender = Gender.Female,
                Dexterity = ab.Dexterity, Intelligence = ab.Intelligence, Strength = ab.Strength,
                Flares = 2,
                Armor = Armor.ChainMail,
                Weapon = Weapon.Mace,
            };
            player.Add(Lamp.Instance);
            return new State(InitMap(new Map(false)), player);
        }

    }
}

