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
                    Util.Write(" * ", bgColor: ConsoleColor.DarkMagenta);
                } else if (!map[p].Known) {
                    Util.Write(" X ", ConsoleColor.White);
                } else {
                    var content = map[p].Contents;
                    // if (map[player.location[0], j, k] == "Zot") {                    roomValue = "W";
                    if (content == null) {
                        Util.Write(" - ", ConsoleColor.White);
                    } else if (content is IMonster) {
                        Util.Write(" M ", ConsoleColor.Red);
                    } else {
                        Util.Write($" {content.Name[0]} ");
                    }
                }
                Util.ResetColors();
                if (p.Col == map.Cols - 1) { Util.WriteLine(); }
            }, state.Player.Location.Level);
            
        }



        public static MapPos RandMapPos(Map map) => new MapPos() {
            Level = Util.RandInt(map.Levels),
            Row = Util.RandInt(map.Rows),
            Col = Util.RandInt(map.Cols),
        };

        public static MapPos RandMapPos(Map map, Func<MapPos, bool> good) {
            var x = RandMapPos(map);
            while (!good(x)) { x = RandMapPos(map); }
            return x;
        }
        public static MapPos RandEmptyMapPos(Map map) => RandMapPos(map, p => map[p].IsEmpty);

        public static MapPos RandMapPos(State state, Func<MapPos, bool> good = null) =>
            good == null ? RandMapPos(state.Map) : RandMapPos(state.Map, good);


        public static MapPos RandEmptyMapPos(State state) => RandEmptyMapPos(state.Map);

        public static IEnumerable<MapPos> SearchMap(Map map, Func<Map.Cell, MapPos, bool> pred) =>
            map.AllPos().Where(p => pred(map[p], p));

        public static IEnumerable<MapPos> SearchMap(State state, Func<Map.Cell, MapPos, bool> pred) =>
            SearchMap(state.Map, pred);

        public static IEnumerable<MapPos> SearchMap(Map map, IItem item) =>
            SearchMap(map, (x,_) => x.Contents == item);

        public static IEnumerable<MapPos> SearchMap(State state, IItem item) =>
            SearchMap(state.Map, item);


    }
}