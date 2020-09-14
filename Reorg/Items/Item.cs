using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    
    abstract class Item : IItem {
        public Item(string name, ItemType itemType) {
            this.Name = name;
            this.ItemType = itemType;
        }
        public ItemType ItemType { get; }
        public string Name { get; }
        
        public override string ToString() => Name;
    }
}

/*
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    
    abstract class Item : IEquatable<Item>, IItem {
        public Item(string name, ItemType itemType) {
            this.Name = name;
            this.ItemType = itemType;
        }
        public ItemType ItemType { get; }
        public string Name { get; }
        
        public override string ToString() => Name;
        public bool Equals(Item other) => other != null && other.Name == Name && other.ItemType == ItemType;

        public static bool operator ==(Item a, Item b) =>
            (a == null && b == null) || ((a == null || b == null) ? false : a.Equals(b));

        public static bool operator !=(Item a, Item b) => !(a == b);

        public override bool Equals(object obj) => this.Equals(obj as Item);

        public override int GetHashCode() => $"~{ItemType}~{Name}".GetHashCode();
    }
}

*/
