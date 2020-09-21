using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    interface IInventoryItem : IHasName {
        void OnFound(State state);
    }

}
