using System.Collections.Generic;

namespace WizardCastle {

    internal static partial class Items {

        public static readonly IArmor Leather = new ArmorImpl("Leather", 1500, 1);
        public static readonly IArmor ChainMail = new ArmorImpl("ChainMail", 2000, 2);
        public static readonly IArmor Plate = new ArmorImpl("Plate", 2500, 3);

        public static IArmor[] AllArmor = new IArmor[] {
            Leather, ChainMail, Plate
        };
        // { 10, 20, 30 }

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
                state.Player.Location = state.Map.RandPos();
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