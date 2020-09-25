using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    public interface IFactory<T> : IHasName {
        T Create();
    }

}
