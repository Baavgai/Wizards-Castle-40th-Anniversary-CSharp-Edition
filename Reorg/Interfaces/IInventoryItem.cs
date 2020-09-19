using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    interface IInventoryItem : IItem {
        public void OnFound(State state);
    }

}
