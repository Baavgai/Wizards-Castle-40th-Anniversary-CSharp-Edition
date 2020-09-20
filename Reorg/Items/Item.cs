using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    abstract class Item : IHasName {
        public Item(string name) {
            this.Name = name;
        }
        public string Name { get; }
        public override string ToString() => Name;

    }
}
