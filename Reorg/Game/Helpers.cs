using System.Collections.Generic;
using System.Threading.Tasks;

namespace WizardCastle {
    internal static partial class Game {
        public static void DefaultItemMessage(IView view, IHasName item) =>
            view.WriteLine($"\nHere you find '{item.Name}'");

        public static void DefaultOnFoundMessage(IView view, IInventoryItem item) =>
            view.WriteLine($"You've recoverd the {item}");

        public static void DefaultOnFound(State state, IInventoryItem item) {
            DefaultOnFoundMessage(state, item);
            state.Player.Add(item);
        }


        public static string RandRace() => Util.RandPick(Race.All).Name;

        public static string RandErrorMsg() => Util.RandPick(ErrorMesssages);

    }
}