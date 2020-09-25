using System;
using System.Collections.Generic;

namespace WizardCastle {
    abstract class VendorItem : Item, IVendorItem {
        public int Cost(bool vendor = true) => vendor ? vendorCost : initCost;
        private readonly int vendorCost;
        private readonly int initCost;
        public VendorItem(string name, int initCost, int vendorCost) : base(name) {
            this.initCost = initCost;
            this.vendorCost = vendorCost;
        }
    }
}
