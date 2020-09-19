using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    static class Extentions {
        public static T Register<T>(this List<T> xs, T x) {
            xs.Add(x);
            return x;
        }
        public static void Register<T>(this List<T> xs, List<T> ys) {
            xs.AddRange(ys);
        }
        // public static bool HasItem(this List<object> xs, object item) => xs.Any(x => x == item);
        // public static bool HasItem(this List<IItem> xs, IItem item) => xs.Any(x => x == item);
    }
}
