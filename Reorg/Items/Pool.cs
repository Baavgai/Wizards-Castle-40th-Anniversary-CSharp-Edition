using System;

namespace WizardCastle {
    class Pool : Item, IContent {
        private static Lazy<Pool> instance = new Lazy<Pool>(() => new Pool());
        public static Pool Instance => instance.Value;

        private Pool() : base("Pool") { }

        public void OnEntry(State state) {
        }
    }
}
