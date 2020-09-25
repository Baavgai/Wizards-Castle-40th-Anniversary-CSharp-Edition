using System.Collections.Generic;

namespace WizardCastle {
    public interface IHasInventory {
        List<IInventoryItem> Inventory { get; }
    }
}