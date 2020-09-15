using System;
using System.Collections.Generic;

namespace WizardCastle {
    public static class Extentions {
        public static T Register<T>(this List<T> xs, T x) {
            xs.Add(x);
            return x;
        }
    }
}
