using System.Collections.Generic;

namespace WizardCastle {

    internal static partial class Items {

        public static readonly IHasOnEntry Gold = new RoomEntryImpl("Gold", ItemType.Content,
            state => {
                var goldFound = Util.RandInt(1, 1001);
                Util.WriteLine($"You've found {goldFound} Gold Pieces");
                state.Player.Gold += goldFound;
                state.CurrentCell.Clear();
            });

        public static readonly IBook Book = new BookImpl();
        public static readonly IChest Chest = new ChestImpl();
        public static readonly IHasOnEntry DownStairs = new RoomEntryImpl("DownStairs", ItemType.Content);
        public static readonly IHasOnEntry UpStairs = new RoomEntryImpl("UpStairs", ItemType.Content);
        public static readonly IHasOnEntry Exit = new RoomEntryImpl("Entrance/Exit", ItemType.Content);
        public static readonly IHasOnEntry SinkHole = new RoomEntryImpl("SinkHole", ItemType.Content,
            state => {
                state.Player.Location.Level += 1;
                Util.Sleep();
            });
        public static readonly IHasOnEntry Warp = new RoomEntryImpl("Warp", ItemType.Content,
            state => {
                state.Player.Location = state.RandLocation();
                Util.Sleep();
            });

        public static readonly IHasOnEntry Flares = new RoomEntryImpl("Flares", ItemType.Content,
        state => {
            int flaresFound = Util.RandInt(1, 11);
            Util.WriteLine($"You've found {flaresFound} flares");
            state.Player.flares += flaresFound;
            state.CurrentCell.Clear();
        });

    }
}