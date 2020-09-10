using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    class MapPos : IEquatable<MapPos> {
        public int Level { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public MapPos() => (Level, Row, Col) = (0, 0, 0);
        public MapPos(int level, int row, int col) => (Level, Row, Col) = (level, row, col);

        public void Deconstruct(out int level, out int row, out int col) => (level, row, col) = (Level, Row, Col);
        public bool Equals(MapPos x) => x != null && x.Level == Level && x.Row == Row && x.Col == Col;

        public static bool operator ==(MapPos a, MapPos b) =>
            (a == null && b == null) || ((a == null || b == null) ? false : a.Equals(b));

        public static bool operator !=(MapPos a, MapPos b) => !(a == b);

        public override bool Equals(object obj) => this.Equals(obj as MapPos);
        public override string ToString() => $"({Level},{Row},{Col})";

        public override int GetHashCode() => ToString().GetHashCode();

        public static readonly MapPos Void = new MapPos(-1, -1, -1);
        public static readonly MapPos PlayerInventory = new MapPos(-2, -1, -1);
    }
}
