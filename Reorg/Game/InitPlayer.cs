using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace WizardCastle {
    internal static partial class Game {

        private static Abilities GetPlayerExtraPoints(IView view, Race race) {
            var result = new Abilities();
            var extraPoints = race.ExtraPoints;
            int i = 0;
            while (extraPoints > 0) {
                var ability = AbilityItem.All[(i++ % 3)];
                view.Write($"\nYou have {extraPoints} points left, how many to add to {ability.Name}: ");
                var amt = view.ReadDigit().Result;
                if (amt > extraPoints) {
                    view.WriteLine($"\n\tSorry, {race.Name}, you * DON'T * have that many points left to distribute");
                } else {
                    extraPoints -= amt;
                    ability.ApplyAmount(result, amt);
                }
            }
            return result;
        }

        public static T PurchaseMenu<T>(IView view, Player player, string itemType, IEnumerable<T> items, Func<T, int> getCost) where T : class, IHasName =>
            view.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of {itemType} do you want to purchase",
                items
                .Select(x => {
                    var cost = getCost(x);
                    return new Tuple<T, string>(x, $"{x.Name}, {cost} Gold Pieces");
                })
                .Append(new Tuple<T, string>(null, "None, 0 Gold Pieces")),
                (x, i) => $"{i}"[0]).Item2.Item1;

        public static T PurchaseMenu<T>(IView view, Player player, string itemType, IEnumerable<T> items, bool vendor) where T : VendorItem =>
            PurchaseMenu(view, player, itemType, items, x => x.Cost(vendor));

        

        public static void AddPlayerArmor(IView view, Player player, bool vendor = true) {
            var choice = PurchaseMenu(view, player, "Armor", Armor.All, vendor);
            if (choice == null) {

            } else if (choice.Cost(vendor) > player.Gold) {
                view.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Armor = choice;
                player.Gold -= choice.Cost(vendor);
            }
            // Util.ClearScreen();
        }

        public static void AddPlayerWeapon(IView view, Player player, bool vendor = true) {
            view.WriteLine();
            var items = Weapon.All
                .Select(x => new Tuple<Weapon, string>(x, $"{x.Name}, {x.Cost(vendor)} Gold Pieces"))
                .Append(new Tuple<Weapon, string>(null, "None, 0 Gold Pieces"))
                .ToList();
            var choice = view.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of Weapon do you want to purchase", items, (x, i) => $"{i}"[0]).Item2.Item1;
            if (choice == null) {

            } else if (choice.Cost(vendor) > player.Gold) {
                view.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Weapon = choice;
                player.Gold -= choice.Cost(vendor);
            }
            // Util.ClearScreen();
        }

        public static void AddPlayerLamp(IView view, Player player, bool vendor = true) {
            var item = Lamp.Instance;
            if (player.Gold >= item.Cost(vendor)) {
                var choice = view.Menu("Would you like to purchase a lamp", new Dictionary<char, string>
                {
                {'Y', $"Purchase lamp ({item.Cost(vendor)} Gold Pieces)"},
                {'N', "Don't purchase"}
            }).Item1 == 'Y';
                if (choice) {
                    player.Gold -= item.Cost(vendor);
                    // player.lamp = true;
                }
                // Util.ClearScreen();
            }
        }

        private static void AddPlayerFlares(IView view, Player player) {
            while (player.Gold > 0) {
                view.Write($"\nOk, {player.Race}, you have {player.Gold} Gold Pieces left, how many flares do you want (1 Gold Piece each): ");
                var n = view.ReadDigit().Result;
                if (n == 0) {
                    break;
                } else if (n > player.Gold) {
                    view.WriteLine($"\n\tSorry, {player.Race}, you * DON'T * have that many Gold Pieces left.");
                } else {
                    player.Flares += n;
                    player.Gold -= n;
                }
            }
            view.Clear();
        }



        private static Player InitPlayer(IView view) {
            var race = view.Menu("Please choose your race", Race.All, (_, i) => $"{i}"[0]).Item2;
            view.Clear();
            var gender = view.Menu("Please choose your gender", Gender.All).Item2;
            view.Clear();

            var ab = GetPlayerExtraPoints(view, race) + race;
            Player player = new Player() {
                Race = race,
                Gender = gender,
                Dexterity = ab.Dexterity, Intelligence = ab.Intelligence, Strength = ab.Strength
            };
            AddPlayerArmor(view, player, false);
            AddPlayerWeapon(view, player, false);
            AddPlayerLamp(view, player, false);
            AddPlayerFlares(view, player);
            return player;
        }


    }
}
/*
 *         public static T PurchaseMenu<T>(Player player, string itemType, IEnumerable<T> items, Func<T, int> getCost) where T : class, IItem {
            var menuItems = items
                .Select(x => {
                    var cost = getCost(x);
                    return new Tuple<T, string>(x, $"{x.Name}, {cost} Gold Pieces");
                })
                .Append(new Tuple<T, string>(null, "None, 0 Gold Pieces"))
                .ToList();
            return Util.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of {itemType} do you want to purchase",
                menuItems, 
                (x, i) => $"{i}"[0]).Item2.Item1;

        }

        public static void AddPlayerArmor(Player player, bool vendor = true) {
            state.WriteLine();
            // var items = new List<Tuple<A>>
            var items = Armor.All
                .Select(x => {
                    return new Tuple<Armor, string>(x, $"{x.Name}, {x.Cost(vendor)} Gold Pieces");
                    })
                .Append(new Tuple<Armor, string>(null, "None, 0 Gold Pieces"))
                .ToList();
            var choice = Util.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of Armor do you want to purchase", items, (x, i) => $"{i}"[0]).Item2.Item1;
            if (choice == null) {

            } else if (choice.Cost(vendor) > player.Gold) {
                state.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Armor = choice;
                player.Gold -= choice.Cost(vendor);
            }
            // Util.ClearScreen();
        }

*/
