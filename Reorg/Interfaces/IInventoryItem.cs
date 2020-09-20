using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    interface IInventoryItem : IHasName {
        public void OnFound(State state);
    }

}
