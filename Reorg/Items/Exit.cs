using System;

namespace WizardCastle {
    class Exit : Item, IHasOnEntry {
        public Exit(): base("Entrance/Exit", ItemType.Content) { }

        public void OnEntry(State state) {
        }
    }
}
