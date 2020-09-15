using System;

namespace WizardCastle {
    class Pool : Item, IContent {
        static Pool() {
            Content.Register(new Pool());
        }

        private Pool() : base("Pool") { }

        public void OnEntry(State state) {
        }
    }
}
