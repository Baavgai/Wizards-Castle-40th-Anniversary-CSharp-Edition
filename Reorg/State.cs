using System;
using System.Collections.Generic;
using System.Linq;


namespace WizardCastle {
    class State {
        public Player Player { get; }
        public Map Map { get; }
        public int Turn { get; set; } = 1;
        public bool Done { get; set; } = false;

        public State(Map map, Player player) {
            Map = map;
            Player = player;
        }

        public Map.Cell CurrentCell => Map[Player.Location];

        public void Deconstruct(out Player player, out Map map) {
            player = Player;
            map = Map;
        }

    }

}
