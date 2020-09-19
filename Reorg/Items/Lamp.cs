using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Lamp : VendorItem, IHasExec, IInventoryItem {
        public static readonly Lamp Instance = new Lamp();

        private Lamp() : base("Lamp", 20, 1000) { }
        public void Exec(State state) {
            if (state.Player.IsBlind) {
                Util.WriteLine($"You're BLIND and can't see anything, silly {state.Player.Race}.");
            } else {
                var choice = Util.Menu("Shine lamp which direction", Direction.All).Item2;
                Game.RevealMapCell(state, choice.Translate(state.Map, state.Player.Location));
            }
        }
        public void OnFound(State state) => Game.DefaultOnFound(state, this);

    }
}
