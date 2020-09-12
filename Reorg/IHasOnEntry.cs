using System;
using System.Collections.Generic;
using System.Linq;


namespace WizardCastle {
    interface IHasOnEntry : IItem {
        public void OnEntry(State state);
    }

}
