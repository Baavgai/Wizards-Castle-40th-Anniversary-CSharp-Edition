using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    internal static partial class Game {

        public static void ShowStatus(State state) {
            Util.WriteLines(Lines());
            IEnumerable<string> Lines() {
                (var player, var map) = state;
                yield return $"You are at {player.Location.DisplayFull}";
                yield return $"You are a {player.Sex} {player.Race}";
                yield return $"Dexterity={player.Dexterity} Intelligence={player.Intelligence} Strength={player.Strength}";
                yield return $"Gold={player.Gold} Flares={player.flares} Armor={player.Armor?.Name ?? "None"} Weapon={player.Weapon?.Name ?? "None"}";
                var treasures = player.Inventory.Where(x => x.ItemType == ItemType.Treasure);
                if (treasures.Count() > 0) {
                    yield return $"Treasures='{string.Join(",", treasures)}'";
                }
                var curses = player.Inventory.Where(x => x.ItemType == ItemType.Curse);
                if (curses.Count() > 0) {
                    yield return $"Cursed with='{string.Join(",", curses)}'";
                }
            }
        }


    }
}