using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace WizardCastle {
    interface IHasOnEntry : IHasName {
        public void OnEntry(State state);
    }

}
