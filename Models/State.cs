using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    class State {
        public Player Player { get; }
        public Map Map { get; }
        public State(Map map, Player player) {
            Map = map;
            Player = player;
        }

        public Map.Cell CurrentCell => Map[Player.Location];

    }

    static class ExtState {

        public static void RevealMap(this State state, MapPos loc) =>
            state.Map[loc].Known = true;

        public static MapPos RandLocation(this State state) => new MapPos() {
            Level = Util.RandInt(state.Map.Levels),
            Row = Util.RandInt(state.Map.Rows),
            Col = Util.RandInt(state.Map.Cols),
        };

        public static void GoNorth(this State state) {
            if (state.Player.Location.Row == 0) {
                state.Player.Location.Row = state.Map.Rows - 1;
            } else {
                state.Player.Location.Row -= 1;
            }
        }

        public static void GoSouth(this State state) {
            if (state.Player.Location.Row == state.Map.Rows - 1) {
                state.Player.Location.Row = 0;
            } else {
                state.Player.Location.Row += 1;
            }
        }
        public static void GoEast(this State state) {
            if (state.Player.Location.Col == state.Map.Cols - 1) {
                state.Player.Location.Col = 0;
            } else {
                state.Player.Location.Col += 1;
            }
        }
        public static void GoWest(this State state) {
            if (state.Player.Location.Col == 0) {
                state.Player.Location.Col = state.Map.Cols - 1;
            } else {
                state.Player.Location.Col -= 1;
            }
        }

    }

}
