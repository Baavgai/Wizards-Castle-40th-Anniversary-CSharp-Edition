using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace WizardCastle {
    interface IHasOnEntry : IItem {
        public void OnEntry(State state);
    }

}
