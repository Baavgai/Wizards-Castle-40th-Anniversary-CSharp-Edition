using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    public interface IInventoryItem : IHasName {
        void OnFound(State state);
    }

}
