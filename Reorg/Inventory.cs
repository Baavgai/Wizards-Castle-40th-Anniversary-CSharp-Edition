using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    class Inventory {
        private readonly List<IItem> items = new List<IItem>();
        public IItem[] Items => items.ToArray();
        public void Add(IItem item) => items.Add(item);
        public void Remove(IItem item) => items.Remove(item);
        public bool HasItem(IItem item) => items.Any(x => x == item);
    }
}
