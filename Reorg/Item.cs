using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    interface IItem : IHasName {
    }

    abstract class Item : IItem {
        public Item(string name) {
            this.Name = name;
        }
        public string Name { get; }
        public override string ToString() => Name;

    }
}

/*
 *     // enum ItemType { Treasure, Monster, Curse, Content, UniqueItem, StackItem, Weapon, Armor }

    interface IHasName {
        public string Name { get; }
    }

    interface IItem : IHasName, IHasItemType {

    }

 * */
