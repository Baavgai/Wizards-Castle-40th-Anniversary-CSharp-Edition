using System.Collections.Generic;

namespace WizardCastle {
    interface IHasInventory {
        List<IInventoryItem> Inventory { get; }
    }
}