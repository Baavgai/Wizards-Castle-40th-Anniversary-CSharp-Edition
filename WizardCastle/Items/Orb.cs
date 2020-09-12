using System;

namespace WizardCastle {
    class Orb : Item, IHasOnEntry {
        public Orb() : base("Orb", ItemType.Content) { }

        public void OnEntry(State state) {
        }
    }
}
