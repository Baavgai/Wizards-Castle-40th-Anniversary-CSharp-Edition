using System;
using System.Linq;
using System.Collections.Generic;



namespace WizardCastle {
    public class Map {
        /*
        public class Cell : ICell {
            public IContent Contents { get; set; }
            public bool Known { get; set; } = false;

            public void Clear() => Contents = null;
            public bool IsEmpty => Contents == null;
        }
        */
        private class CellImpl : ICell {
            public CellImpl(MapPos loc) => Location = loc;
            public MapPos Location { get; }
            public ICellContent Content { get; set; }
            public bool Known { get; set; }
            public void Clear() => Content = null;
        }

        private readonly ICell[,,] state;
        public Map(bool random) {
            if (random) {
                Levels = Util.RandInt(8, 31);
                Rows = Util.RandInt(8, 31);
                Cols = Util.RandInt(8, 31);
            } else {
                Levels = Rows = Cols = 8;
            }
            state = new ICell[Levels, Rows, Cols];
            Traverse((_, p) => this[p] = new CellImpl(p));
        }

        public int Levels { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public ICell this[MapPos p] {
            get => ValidPos(p) ? state[p.Level, p.Row, p.Col] : new CellImpl(p);
            set {
                if (ValidPos(p)) {
                    state[p.Level, p.Row, p.Col] = value;
                }
            }
        }

        public ICell this[int level, int row, int col] {
            get => this[new MapPos(level, row, col)];
            set => this[new MapPos(level, row, col)] = value;
        }
        public IEnumerable<MapPos> AllPos() {
            var p = new MapPos();
            for (p.Level = 0; p.Level < Levels; p.Level++) {
                for (p.Row = 0; p.Row < Rows; p.Row++) {
                    for (p.Col = 0; p.Col < Cols; p.Col++) {
                        yield return p;
                    }
                }
            }
        }
        public IEnumerable<(ICell cell, MapPos pos)> AllCellPos() =>
            AllPos().Select(p => (this[p], p));

        public bool ValidPos(MapPos p) =>
            p != null && p.Level >= 0 && p.Level < Levels && p.Row >= 0 && p.Row < Rows && p.Col >= 0 && p.Col < Cols;

        public void Traverse(Action<Map, MapPos> action, IEnumerable<MapPos> pos) {
            foreach (var p in pos) {
                action(this, p);
            }
        }

        public void Traverse(Action<Map, MapPos> action, int level) =>
            Traverse(action, AllPos().Where(x => x.Level == level));

        public void Traverse(Action<Map, MapPos> action) =>
            Traverse(action, AllPos());


        public MapPos RandPos() => new MapPos() {
            Level = Util.RandInt(Levels),
            Row = Util.RandInt(Rows),
            Col = Util.RandInt(Cols),
        };


        public (ICell cell, MapPos pos)? RandCellPos(Func<ICell, MapPos, bool> good) {
            var found = Search(good);
            if (found.Count() != 0) { return Util.RandPick(found); }
            return null;
        }


        public (ICell cell, MapPos pos)? RandCellPos(IHasName item) =>
            RandCellPos((cell, _) => cell.Content == item);


        public (ICell cell, MapPos pos, T content)? RandCellPosContent<T>() where T : class {
            var found = RandCellPos((cell, _) => !cell.IsEmpty() && cell.Content is T);
            if (found.HasValue) {
                var (cell, pos) = found.Value;
                var x = cell.Content as T;
                return (cell, pos, x);
            }
            return null;
        }

        public (ICell cell, MapPos pos)? RandEmptyCellPos() =>
            RandCellPos((cell, _) => cell.IsEmpty());

        public IEnumerable<(ICell cell, MapPos pos)> Search(Func<ICell, MapPos, bool> pred) =>
           AllCellPos().Where(x => pred(x.cell, x.pos));

        public IEnumerable<(ICell cell, MapPos pos)> Search(IHasName item) =>
            Search((cell, _) => cell.Content == item);

    }
}

/*
public MapPos RandEmptyPos() => RandPos(p => this[p].IsEmpty); *         public MapPos RandPos(Func<MapPos, bool> good) {
            var x = RandPos();
            while (!good(x)) { x = RandPos(); }
            return x;
        }

        public MapPos RandEmptyPos() => RandPos(p => this[p].IsEmpty);

 * */
