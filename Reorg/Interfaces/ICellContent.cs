using System;
using System.Collections.Generic;

namespace WizardCastle {
    public interface ICellContent {
        void OnEntry(State state);
        char Symbol { get; }
    }
}