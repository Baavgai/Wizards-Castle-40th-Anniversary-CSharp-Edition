using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    class StackItem : Item {
        public StackItem(string name) : base(name) { }
        public override ItemType ItemType => Models.ItemType.StackItem;
        public int Quantity { get; set; }
    }
}
