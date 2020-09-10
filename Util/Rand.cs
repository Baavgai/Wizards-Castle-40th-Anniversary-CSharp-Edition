﻿using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace The_Wizard_s_Castle {
    internal static partial class Util {
        public static readonly Random Rand = new Random();

        public static int RandInt(int maxValue) => Rand.Next(maxValue);
        public static int RandInt(int minValue, int maxValue) => Rand.Next(minValue, maxValue);

        public static T RandPick<T>(IEnumerable<T> xs) => xs.Skip(Rand.Next(xs.Count())).First();

    }
}