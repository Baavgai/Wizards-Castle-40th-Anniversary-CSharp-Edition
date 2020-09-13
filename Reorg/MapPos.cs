using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace WizardCastle {
    class MapPos : IEquatable<MapPos> {
        public int Level { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        // public MapPos() => (Level, Row, Col) = (0, 0, 0);
        public MapPos(int level = 0, int row = 0, int col = 0) => (Level, Row, Col) = (level, row, col);

        public void Deconstruct(out int level, out int row, out int col) => (level, row, col) = (Level, Row, Col);
        public bool Equals(MapPos x) => x != null && Equals(this, x);

        // neither of these will be null
        private static bool Equals(MapPos a, MapPos b) =>
            a.Level == b.Level && a.Row == b.Row && a.Col == b.Col;

        public static bool operator ==(MapPos a, MapPos b) { 
            // a == null ? b == null : (b == null ? false : Equals(a, b));
            // Check for null on left side.
            if (Object.ReferenceEquals(a, null)) {
                if (Object.ReferenceEquals(b, null)) {
                    return true;
                }
                return false;
            }
            // Equals handles case of null on right side.
            return !Object.ReferenceEquals(b, null) && Equals(a,b);
        }

        public static bool operator !=(MapPos a, MapPos b) => !(a == b);

        public override bool Equals(object obj) => this.Equals(obj as MapPos);
        public override string ToString() => $"({Level},{Row},{Col})";
        public string Display => $"({Level + 1}, {Row + 1}, {Col + 1})";
        public string DisplayFull => $"Level {Level + 1} Row {Row + 1} Column {Col + 1}";

        public override int GetHashCode() => ToString().GetHashCode();

        public static MapPos operator +(MapPos a, MapPos b) =>
            new MapPos() {
                Level = a.Level + b.Level,
                Row = a.Row + b.Row,
                Col = a.Col + b.Col
            };

        // Example: For Level 3, Row 5, Column 2: 3,5,2)
        public static MapPos Parse(string s) {
            var xs = s.Split(',');
            if (xs.Length == 3 && xs.All(x => int.TryParse(x, out int n))) {
                var ys = xs.Select(int.Parse).ToArray();
                return new MapPos(ys[0], ys[1], ys[2]);
            }
            return null;
        }


        
        // public static readonly MapPos Void = new MapPos(-1, -1, -1);
        // public static readonly MapPos PlayerInventory = new MapPos(-2, -1, -1);
    }
}
