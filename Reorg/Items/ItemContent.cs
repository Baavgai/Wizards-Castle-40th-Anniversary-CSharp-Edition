using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    class ItemContent : ICellContent {
        private readonly IInventoryItem item;
        public ItemContent(IInventoryItem item) => this.item = item;
        public string Name => item.Name;

        public char Symbol => throw new NotImplementedException();

        public void OnEntry(State state) {
            Game.DefaultItemMessage(state, this);
            item.OnFound(state);
            state.CurrentCell.Clear();
        }
        public override string ToString() => Name;
    }
}
