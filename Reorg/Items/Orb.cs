using System;

namespace WizardCastle {
    class Orb : Item, IContent {
        static Orb() {
            Content.Register(new Orb());
        }

        private Orb() : base("Orb") { }

        public void OnEntry(State state) {
        }
    }
}
