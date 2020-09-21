using System;
using System.Collections.Generic;
using System.Linq;


namespace WizardCastle {
    interface IHasOnEntry : IHasName {
        void OnEntry(State state);
    }

}
