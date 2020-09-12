using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace WizardCastle {
    
    abstract class RoomItem : Item {
        public RoomItem(string name, ItemType itemType): base(name, itemType) {
        }
        public abstract void OnEntry(State state);
    }
}
