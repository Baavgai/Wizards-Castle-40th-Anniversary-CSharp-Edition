using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    class Zot : Item, IContent, IInventoryItem {
        private static Lazy<Zot> instance = new Lazy<Zot>(() => new Zot());
        public static Zot Instance => instance.Value;

        private Zot() : base("Orb of Zot") { }

        public void OnEntry(State state) {
            if (state.Player.LastAction != GameAction.Teleport) {
                state.Player.LastAction.Exec(state);
            } else {
                state.WriteNewLine().WriteIndent()
                    .SetColor(ConsoleColor.DarkGray)
                    .WriteLine("YOU'VE FOUND THE ORB OF ZOT")
                    .ResetColors()
                    .Sleep()
                    .WriteNewLine();
                state.Player.Add(this);
                state.CurrentCell.Clear();
            }
        }
        public void OnFound(State state) => Game.DefaultOnFound(state, this);

    }
}