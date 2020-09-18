using System;
using System.Collections.Generic;

namespace WizardCastle {
    class VendorItem : Item {
        private readonly static List<VendorItem> all = new List<VendorItem>();
        public static readonly VendorItem Lamp = all.Register(new VendorItem("Lamp", 20, 1000));

        public static VendorItem[] All => all.ToArray();
        public int Cost(bool vendor = true) => vendor ? vendorCost : initCost;
        private readonly int vendorCost;
        private readonly int initCost;
        protected VendorItem(string name, int initCost, int vendorCost) : base(name) {
            this.initCost = initCost;
            this.vendorCost = vendorCost;
        }
    }
}
