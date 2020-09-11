using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    class State {
        public Player Player { get; }
        public Map Map { get; }
        public State(Map map, Player player) {
            this.Map = map;
            this.Player = player;
        }


        public void East() {
            if (this.location[2] == (map.GetLength(2) - 1)) {
                this.location[2] = 0;
            } else {
                this.location[2] += 1;
            }
        }

        public void West(string[,,] map) {
            if (this.location[2] == 0) {
                this.location[2] = (map.GetLength(2) - 1);
            } else {
                this.location[2] -= 1;
            }
        }

        public void North(string[,,] map) {
            if (this.location[1] == 0) {
                this.location[1] = (map.GetLength(1) - 1);
            } else {
                this.location[1] -= 1;
            }
        }

        public void South(string[,,] map) {
            if (this.location[1] == (map.GetLength(1) - 1)) {
                this.location[1] = 0;
            } else {
                this.location[1] += 1;
            }
        }

        public void Down() {
            this.location[0] += 1;
        }

        public void Up() {
            this.location[0] -= 1;
        }

        public void Sink() {
            this.location[0] += 1;
        }

        public void Warp(string[,,] map) {
            int level = rand.Next(0, map.GetLength(0));
            int row = rand.Next(0, map.GetLength(1));
            int column = rand.Next(0, map.GetLength(2));
            this.location = (new int[] { level, row, column });
        }
    }

}
