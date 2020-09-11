using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    class UniqueItem : Item {
        public UniqueItem(string name) : base(name) { }
        public override ItemType ItemType => Models.ItemType.UniqueItem;
    }
}
