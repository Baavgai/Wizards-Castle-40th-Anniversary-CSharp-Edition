using System;
using System.Collections.Generic;

namespace WizardCastle {
    public interface IStackable : IInventoryItem {
        int Quantity { get; set; }
    }
}