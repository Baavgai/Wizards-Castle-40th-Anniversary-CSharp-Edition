using System;

namespace WizardCastle {
    class Warp : Item, IHasOnEntry {
        public Warp() : base("Warp", ItemType.Content) { }

        public void OnEntry(State state) {
            state.Player.Location = state.RandLocation();
            Util.Sleep();
        }
    }
}
