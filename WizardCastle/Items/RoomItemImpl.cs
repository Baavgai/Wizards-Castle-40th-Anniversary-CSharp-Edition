using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace WizardCastle {
    class RoomItemImpl : RoomItem {
        private readonly Action<State> onEntry;

        public RoomItemImpl(string name, ItemType itemType, Action<State> onEntry): base(name, itemType) {
            this.onEntry = onEntry;
        }

        public override void OnEntry(State state) => onEntry(state);
    }
}
