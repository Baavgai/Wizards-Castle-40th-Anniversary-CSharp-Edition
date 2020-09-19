using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    interface IFactory<T> : IHasName {
        public T Create();
    }

}
