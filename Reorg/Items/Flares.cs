using System;

namespace WizardCastle {
    class Flares : Item, IHasOnEntry {
        public Flares() : base("Flares", ItemType.Content) { }

        public void OnEntry(State state) {
            int flaresFound = Util.RandInt(1, 11);
            Util.WriteLine($"You've found {flaresFound} flares");
            state.Player.flares += flaresFound;
            state.CurrentCell.Clear();
        }
    }
}
