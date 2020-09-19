using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Game {
        public static void DefaultItemMessage(IItem item) => Util.WriteLine($"\nHere you find '{item.Name}'");

        public static void DefaultOnFoundMessage(IInventoryItem item) => Util.WriteLine($"You've recoverd the {item}");

        public static void DefaultOnFound(State state, IInventoryItem item) {
            DefaultOnFoundMessage(item);
            state.Player.Add(item);
        }


        public static string RandRace() => Util.RandPick(Race.All).Name;

        public static string RandErrorMsg() => Util.RandPick(ErrorMesssages);

    }
}