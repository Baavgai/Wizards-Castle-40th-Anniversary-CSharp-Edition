using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Util {

        public static void Sleep() {
            for (int i = 0; i < 30; i++) {
                Util.Write(".");
                System.Threading.Thread.Sleep(100);
            }

        }
    }
}
