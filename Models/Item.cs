using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    enum ItemType { Treasure, Monster, Curse, Content, UniqueItem, StackItem    }
    

    class Item : IEquatable<Item> {
        public delegate void EntryHandler(State state);
        private readonly EntryHandler onEntry;

        public Item(string name, ItemType itemType, EntryHandler onEntry) {
            this.Name = name;
            this.ItemType = itemType;
            this.onEntry = onEntry;
        }
        public ItemType ItemType { get; }
        public string Name { get; }

        public void OnEntry(State state) => onEntry(state);
        public override string ToString() => Name;
        public bool Equals(Item other) => other != null && other.Name == Name && other.ItemType == ItemType;

        public static bool operator ==(Item a, Item b) =>
            (a == null && b == null) || ((a == null || b == null) ? false : a.Equals(b));

        public static bool operator !=(Item a, Item b) => !(a == b);

        public override bool Equals(object obj) => this.Equals(obj as Item);
        // public override string ToString() => $"({Level},{Row},{Col})";

        public override int GetHashCode() => $"~{ItemType}~{Name}".GetHashCode();


    }
}

/*
 * using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    enum ItemType {
        Treasure, Monster, Curse, Content, UniqueItem, StackItem
    }
    

    abstract class Item : IEquatable<Item> {

        protected Item(string name) => Name = name;
        public override string ToString() => Name;
        public bool Equals(Item other) => other != null && other.Name == Name && other.ItemType == ItemType;

        public string Name { get; }
        public abstract ItemType ItemType { get; }
        public abstract void OnEntry(State state);
    }

    class ItemImpl : Item {
        // private readonly ItemType itemType;
        public delegate void EntryHandler(State state);
        private readonly EntryHandler onEntry;

        public ItemImpl(string name, ItemType itemType, EntryHandler onEntry) : base(name) {
            this.ItemType = itemType;
            this.onEntry = onEntry;
        }
        public override ItemType ItemType { get; }

        public override void OnEntry(State state) => onEntry(state);
    }
}

 * */
