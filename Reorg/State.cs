using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace WizardCastle {
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
    }

}
