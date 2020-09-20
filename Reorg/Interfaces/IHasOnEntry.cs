using System;
using System.Collections.Generic;
using System.Linq;


namespace WizardCastle {
    interface IHasOnEntry : IHasName {
        public void OnEntry(State state);
    }

}
