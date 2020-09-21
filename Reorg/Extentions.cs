using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WizardCastle {
    static class Extentions {
        public static T Register<T>(this List<T> xs, T x) {
            xs.Add(x);
            return x;
        }
        // public static void Register<T>(this List<T> xs, List<T> ys) {            xs.AddRange(ys);        }

        public static Func<State, Task> ToTask(this Action<State> action) =>
            s => { action(s); return Task.CompletedTask; };
    }
}
