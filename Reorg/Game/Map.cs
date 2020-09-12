using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    internal static partial class Game {

        public static void RevealMap(State state, MapPos loc) =>
            state.Map[loc].Known = true;
        public static void HideMap(State state, MapPos loc) =>
            state.Map[loc].Known = false;

        /*
        public static MapPos RandLocation(State state) => new MapPos() {
            Level = Util.RandInt(state.Map.Levels),
            Row = Util.RandInt(state.Map.Rows),
            Col = Util.RandInt(state.Map.Cols),
        };
        */

        public static void DisplayLevel(State state) =>
            state.Map.TraverseLevel(state.Player.Location.Level, (map, p) => {
                if (p == state.Player.Location) {
                    Util.Write(" * ", bgColor: ConsoleColor.DarkMagenta);
                } else {
                    var content = map[p].Contents;
                    // if (map[player.location[0], j, k] == "Zot") {                    roomValue = "W";
                    if (content == null) {
                        Util.Write(" - ", ConsoleColor.White);
                    } else if (content.ItemType == ItemType.Monster) {
                        Util.Write(" M ", ConsoleColor.Red);
                    } else {
                        Util.Write($" {content.Name[0]} ");
                    }
                    if (p.Col == map.Cols - 1) { Util.WriteLine(); }
                }
            });
    }

}
