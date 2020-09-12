using System;
using System.Threading;

namespace WizardCastle {
    class SinkHole : Item, IHasOnEntry {
        public SinkHole() : base("SinkHole", ItemType.Content) { }

        public void OnEntry(State state) {
            state.Player.Location.Level += 1;
            Util.Sleep();
        }
    }
}
