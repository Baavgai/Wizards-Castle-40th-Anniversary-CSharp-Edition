using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    static class Extentions {
        public static T Register<T>(this List<T> xs, T x) {
            xs.Add(x);
            return x;
        }
        // public static void Register<T>(this List<T> xs, List<T> ys) {            xs.AddRange(ys);        }
    }
}
