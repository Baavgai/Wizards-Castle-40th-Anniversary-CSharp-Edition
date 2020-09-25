using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    class InventoryItem : Item, IInventoryItem {
        private readonly static List<IInventoryItem> all = new List<IInventoryItem>();
        public static IInventoryItem[] All => all.ToArray();

        public static readonly IInventoryItem Flares = all.Register(new InventoryItem("Flares",
        state => {
            int flaresFound = Util.RandInt(1, 11);
            state.WriteLine($"You've found {flaresFound} flares");
            state.Player.Flares += flaresFound;
            // state.CurrentCell.Clear();
        }));
        public static readonly IInventoryItem Gold = all.Register(new InventoryItem("Gold",
            state => {
                var goldFound = Util.RandInt(1, 1001);
                state.WriteLine($"You've found {goldFound} Gold Pieces");
                state.Player.Gold += goldFound;
                // state.CurrentCell.Clear();
            }));



        private readonly Action<State> onFound;
        private InventoryItem(string name, Action<State> onFound) : base(name) {
            this.onFound = onFound;
        }

        public void OnFound(State state) => onFound(state);
    }
}
