namespace WizardCastle {
    public interface ICell {
        MapPos Location { get; }
        bool Known { get; set; }
        ICellContent Content { get; }
    }
    public static class ExtICell {
        public static bool IsEmpty(this ICell cell) => cell.Content == null;
        public static void OnEntry(this ICell cell, State state) => cell.Content?.OnEntry(state);
    }
}