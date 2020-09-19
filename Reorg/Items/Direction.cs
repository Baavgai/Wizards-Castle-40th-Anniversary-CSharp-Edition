using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Direction : IItem, IHasExec {
        private readonly static List<Direction> all = new List<Direction>();
        public static Direction[] All => all.ToArray();
        public static Direction North = all.Register(new Direction("North",
            (map, pos) => new MapPos(level: pos.Level, col: pos.Col, row: pos.Row == 0 ? map.Rows - 1 : pos.Row - 1)));
        public static Direction South = all.Register(new Direction("South",
            (map, pos) => new MapPos(level: pos.Level, col: pos.Col, row: pos.Row == map.Rows - 1 ? 0 : pos.Row + 1)));
        public static Direction East = all.Register(new Direction("East",
            (map, pos) => new MapPos(level: pos.Level, row: pos.Row, col: pos.Col == map.Cols - 1 ? 0 : pos.Col + 1)));
        public static Direction West = all.Register(new Direction("West",
            (map, pos) => new MapPos(level: pos.Level, row: pos.Row, col: pos.Col == 0 ? map.Cols - 1 : pos.Col - 1)));

        public string Name { get; }
        // private readonly Action<State> exec;
        private readonly Func<Map, MapPos, MapPos> translate;
        private Direction(string name, Func<Map, MapPos, MapPos> translate) {
            Name = name;
            this.translate = translate;
        }
        public void Exec(State state) {
            state.Player.Location = translate(state.Map, state.Player.Location);
            state.CurrentCell.Known = true;
        }

        public MapPos Translate(Map map, MapPos pos) => translate(map, pos);

        public override string ToString() => Name;

    }
}
