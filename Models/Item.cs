using System;
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
    }
}
