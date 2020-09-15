using System;

namespace WizardCastle {
    class Orb : Item, IContent {
        private static Lazy<Orb> instance = new Lazy<Orb>(() => new Orb());
        public static Orb Instance => instance.Value;

        private Orb() : base("Orb") { }

        public void OnEntry(State state) {
        }
    }
}
