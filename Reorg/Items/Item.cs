using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    public abstract class Item : IHasName, IHasSymbol {
        public Item(string name) {
            this.Name = name;
        }
        public string Name { get; }
        public override string ToString() => Name;
        public virtual char Symbol => Name[0];
    }
}
