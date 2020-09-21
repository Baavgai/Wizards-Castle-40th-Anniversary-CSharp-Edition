using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    internal static partial class Game {

        public static void ShowStatus(State state) {
            // state.WriteLines(Lines());
            foreach(var line in Lines()) {
                state.WriteLine(line);
            }
            IEnumerable<string> Lines() {
                (var player, var map) = state;
                yield return $"You are at {player.Location.DisplayFull}";
                yield return $"You are a {player.Gender} {player.Race}";
                yield return $"Dexterity={player.Dexterity} Intelligence={player.Intelligence} Strength={player.Strength}";
                yield return $"Gold={player.Gold} Flares={player.Flares} Armor={player.Armor?.Name ?? "None"} Weapon={player.Weapon?.Name ?? "None"}";
                var treasures = player.Inventory.Where(x => x is Treasure);
                if (treasures.Count() > 0) {
                    yield return $"Treasures='{string.Join(",", treasures)}'";
                }
                var curses = player.Inventory.Where(x => x is Curse);
                if (curses.Count() > 0) {
                    yield return $"Cursed with='{string.Join(",", curses)}'";
                }
                yield return "";
            }
        }


    }
}