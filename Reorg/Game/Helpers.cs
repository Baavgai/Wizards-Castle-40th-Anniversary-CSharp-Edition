﻿using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Game {
        public static void DefaultItemMessage(IHasName item) => Util.WriteLine($"\nHere you find '{item.Name}'");

        public static string RandRace() => Util.RandPick(Race.AllRaces).Name;

        public static string RandErrorMsg() => Util.RandPick(ErrorMesssages);

    }
}