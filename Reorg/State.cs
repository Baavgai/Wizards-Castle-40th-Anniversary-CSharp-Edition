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

}
