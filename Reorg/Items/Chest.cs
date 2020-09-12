using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Chest : Item, IHasOpen, IHasOnEntry {

        public Chest() : base("Chest", ItemType.Content) { }

        public void OnEntry(State state) => Util.WriteLine($"\nHere you find '{Name}'");

        public void Open(State state) {
            if (!state.Player.blind) {
                state.CurrentCell.Clear();
                Util.WriteLine($"\nYou open the chest and { Util.RandPick(AllHandlers)(state)}");
            } else {
                Util.WriteLine($"Sorry, {state.Player.Race}, it's not written in Braille!");
            }
            Util.WaitForKey();
        }

        private static readonly Func<State, string>[] AllHandlers = new Func<State, string>[] {
            s => {
                int randomGold = Util.RandInt(2, 1001);
                s.Player.Gold += randomGold;
                return $"there's {randomGold} Gold Pieces inside.";
            },
            s => {
                Util.RandPick(Direction.AllDirections).Exec(s);
                return "Gas! You stagger from the room!";
            },
            s => {
                var rndDmg = Util.RandInt(2, 10) - s.Player.armor switch
                {
                    "Leather" => 2,
                    "ChainMail" => 3,
                    "Plate" => 4,
                    _ => 0
                };
                if (rndDmg > 0) {
                    s.Player.Strength -= rndDmg;
                }
                return "Kaboom! It Explodes!";
            },
            s => {
                switch (Util.RandInt(3)) {
                    case 0:
                        s.Player.Dexterity += Util.RandInt(1, 5);
                        return "you find a potion of dexterity.";
                    case 1:
                        s.Player.Intelligence += Util.RandInt(1, 5);
                        return "you find a potion of intelligence.";
                    default:
                        s.Player.Strength += Util.RandInt(1, 5);
                        return "you find a potion of strength.";
                };
            },
            s => {
                var curse = Util.RandPick(Curse.AllCurses);
                curse.Exec(s);
                return $"a wizard jumps out and puts the curse of {curse.Name} on you and runs out of the room!";
            },

            s => {
                var loc = s.RandLocation();
                s.RevealMap(loc);
                return $"you find a piece of a map revealing the area around {loc.Display}).";
            }

        };
    }
}
