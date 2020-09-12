using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Direction : IHasName {
        public string Name { get; }
        public Action<State> Exec { get; }
        private Direction(string name, Action<State> exec) {
            Name = name;
            Exec = exec;
        }
        public override string ToString() => Name;

        public static Direction North = new Direction("North", state => {
            if (state.Player.Location.Row == 0) {
                state.Player.Location.Row = state.Map.Rows - 1;
            } else {
                state.Player.Location.Row -= 1;
            }
        });
        public static Direction South = new Direction("South", state => {
            if (state.Player.Location.Row == state.Map.Rows - 1) {
                state.Player.Location.Row = 0;
            } else {
                state.Player.Location.Row += 1;
            }
        });
        public static Direction East = new Direction("East", state => {
            if (state.Player.Location.Col == state.Map.Cols - 1) {
                state.Player.Location.Col = 0;
            } else {
                state.Player.Location.Col += 1;
            }
        });
        public static Direction West = new Direction("West", state => {
            if (state.Player.Location.Col == 0) {
                state.Player.Location.Col = state.Map.Cols - 1;
            } else {
                state.Player.Location.Col -= 1;
            }
        });

        public static Direction[] AllDirections = new Direction[] {
            North, South, East, West
        };

    }
}
