using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Direction : IItem, IHasExec {
        public string Name { get; }
        // private readonly Action<State> exec;
        private readonly Func<State, MapPos, MapPos> translate;
            private Direction(string name, Func<State, MapPos, MapPos> translate) {
            Name = name;
            this.translate = translate;
        }
        public void Exec(State state) {
            state.Player.Location = translate(state, state.Player.Location);
            state.CurrentCell.Known = true;
        }

        public MapPos Translate(State state, MapPos pos) => translate(state, pos);

        public override string ToString() => Name;

        public static Direction North = new Direction("North", (state,pos) => {
            var np = new MapPos(pos);
            if (np.Row == 0) {
                np.Row = state.Map.Rows - 1;
            } else {
                np.Row -= 1;
            }
            return np;
        });
        public static Direction South = new Direction("South", (state, pos) => {
            var np = new MapPos(pos);
            if (np.Row == 0) {
                np.Row = state.Map.Rows - 1;
            } else {
                np.Row -= 1;
            }
            return np;
        });
        state => {
            if (state.Player.Location.Row == state.Map.Rows - 1) {
                state.Player.Location.Row = 0;
            } else {
                state.Player.Location.Row += 1;
            }
            state.CurrentCell.Known = true;
        });
        public static Direction East = new Direction("East", state => {
            if (state.Player.Location.Col == state.Map.Cols - 1) {
                state.Player.Location.Col = 0;
            } else {
                state.Player.Location.Col += 1;
            }
            state.CurrentCell.Known = true;
        });
        public static Direction West = new Direction("West", state => {
            if (state.Player.Location.Col == 0) {
                state.Player.Location.Col = state.Map.Cols - 1;
            } else {
                state.Player.Location.Col -= 1;
            }
            state.CurrentCell.Known = true;
        });

        public static Direction[] AllDirections = new Direction[] {
            North, South, East, West
        };

    }
}
