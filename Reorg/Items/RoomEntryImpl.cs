using System;

namespace WizardCastle {
    internal static partial class Items {

        private class RoomEntryImpl : Item, IHasOnEntry {
            private readonly Action<State> onEntry;
            public RoomEntryImpl(string name, ItemType itemType, Action<State> onEntry = null) : base(name, itemType) {
                this.onEntry = onEntry;
            }
            public void OnEntry(State state) {
                if (onEntry != null) {
                    onEntry(state);
                } else {
                    Game.DefaultItemMessage(this);
                }
            }
        }
    }
}
