using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    internal static partial class Game {

        public static void RevealMapCell(State state, MapPos loc) =>
            state.Map[loc].Known = true;
        public static void HideMapCell(State state, MapPos loc) =>
            state.Map[loc].Known = false;

        public static void RevealMapArea(State state, MapPos loc) {
            var neighbors = new MapPos[] {
                new MapPos(row: -1, col: -1),
                new MapPos(row: -1, col: 0),
                new MapPos(row: -1, col: 1),
                new MapPos(row: 0, col: -1),
                new MapPos(row: 0, col: 1),
                new MapPos(row: 1, col: -1),
                new MapPos(row: 1, col: 0),
                new MapPos(row: 1, col: 1)
            }
            .Select(x => x + loc)
            .Where(x => state.Map.ValidPos(x));
            foreach(var p in neighbors) {
                RevealMapCell(state, p);
            }

        }




        public static void DisplayLevel(State state) {
            state.Map.Traverse((map, p) => {
                if (p == state.Player.Location) {
                    state.SetBgColor(ConsoleColor.DarkMagenta).Write(" * ");
                } else if (!map[p].Known) {
                    state.SetColor(ConsoleColor.White).Write(" X ");
                } else {
                    var content = map[p].Contents;
                    // if (map[player.location[0], j, k] == "Zot") {                    roomValue = "W";
                    if (content == null) {
                        state.SetColor(ConsoleColor.White).Write(" - ");
                    } else if (content is IMonster) {
                        state.SetColor(ConsoleColor.White).Write(" M ");
                    } else {
                        state.Write($" {content.Name[0]} ");
                    }
                }
                state.ResetColors();
                if (p.Col == map.Cols - 1) { state.WriteLine(); }
            }, state.Player.Location.Level);
            
        }




        /*
        public static MapPos RandEmptyMapPos(State state) => state.Map.RandEmptyPos();


        public static IEnumerable<(Map.Cell cell, MapPos pos)> SearchMap(State state, Func<Map.Cell, MapPos, bool> pred) =>
            state.Map.Search(pred);

        public static IEnumerable<(Map.Cell cell, MapPos pos)> SearchMap(State state, IHasName item) =>
            SearchMap(state, item);
        */

    }
}