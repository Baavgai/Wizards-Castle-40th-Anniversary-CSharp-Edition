using System;
using System.Collections.Generic;

namespace WizardCastle {
    static class Misc {
        private readonly static List<IItem> all = new List<IItem>();
        public static IItem[] All => all.ToArray();
        private static T Register<T>(T item) where T : IItem {
            all.Add(item);
            return item;
        }

        public static readonly VendorItem Lamp = Register(new VendorItem("Lamp", 20, 1000));
        // public static readonly Orb Orb = Orb.Instance;
    }
}
