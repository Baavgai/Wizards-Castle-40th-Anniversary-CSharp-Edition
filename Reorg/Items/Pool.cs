using System;

namespace WizardCastle {
    class Pool : Item, IHasOnEntry {
        public Pool() : base("Pool", ItemType.Content) { }

        public void OnEntry(State state) {
        }
    }
}
