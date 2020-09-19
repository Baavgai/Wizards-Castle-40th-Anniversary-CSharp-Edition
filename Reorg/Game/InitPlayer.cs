using System.Linq;
using System.Collections.Generic;
using System;

namespace WizardCastle {
    internal static partial class Game {

        private static Abilities GetPlayerExtraPoints(Race race) {
            var result = new Abilities();
            var extraPoints = race.ExtraPoints;
            int i = 0;
            while (extraPoints > 0) {
                var ability = AbilityItem.All[(i++ % 3)];
                Util.Write($"\nYou have {extraPoints} points left, how many to add to {ability.Name}: ");
                var amt = Util.ReadDigit();
                if (amt > extraPoints) {
                    Util.WriteLine($"\n\tSorry, {race.Name}, you * DON'T * have that many points left to distribute");
                } else {
                    extraPoints -= amt;
                    ability.ApplyAmount(result, amt);
                }
            }
            return result;
        }

        public static T PurchaseMenu<T>(Player player, string itemType, IEnumerable<T> items, Func<T, int> getCost) where T : class, IItem =>
            Util.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of {itemType} do you want to purchase",
                items
                .Select(x => {
                    var cost = getCost(x);
                    return new Tuple<T, string>(x, $"{x.Name}, {cost} Gold Pieces");
                })
                .Append(new Tuple<T, string>(null, "None, 0 Gold Pieces")),
                (x, i) => $"{i}"[0]).Item2.Item1;

        public static T PurchaseMenu<T>(Player player, string itemType, IEnumerable<T> items, bool vendor) where T : VendorItem =>
            PurchaseMenu(player, itemType, items, x => x.Cost(vendor));

        

        public static void AddPlayerArmor(Player player, bool vendor = true) {
            var choice = PurchaseMenu(player, "Armor", Armor.All, vendor);
            if (choice == null) {

            } else if (choice.Cost(vendor) > player.Gold) {
                Util.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Armor = choice;
                player.Gold -= choice.Cost(vendor);
            }
            // Util.ClearScreen();
        }

        public static void AddPlayerWeapon(Player player, bool vendor = true) {
            Util.WriteLine();
            var items = Weapon.All
                .Select(x => new Tuple<Weapon, string>(x, $"{x.Name}, {x.Cost(vendor)} Gold Pieces"))
                .Append(new Tuple<Weapon, string>(null, "None, 0 Gold Pieces"))
                .ToList();
            var choice = Util.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of Weapon do you want to purchase", items, (x, i) => $"{i}"[0]).Item2.Item1;
            if (choice == null) {

            } else if (choice.Cost(vendor) > player.Gold) {
                Util.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Weapon = choice;
                player.Gold -= choice.Cost(vendor);
            }
            // Util.ClearScreen();
        }

        public static void AddPlayerLamp(Player player, bool vendor = true) {
            var item = Misc.Lamp;
            if (player.Gold >= item.Cost(vendor)) {
                var choice = Util.Menu("Would you like to purchase a lamp", new Dictionary<char, string>
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

        private static void AddPlayerFlares(Player player) {
            while (player.Gold > 0) {
                Util.Write($"\nOk, {player.Race}, you have {player.Gold} Gold Pieces left, how many flares do you want (1 Gold Piece each): ");
                var n = Util.ReadDigit();
                if (n == 0) {
                    break;
                } else if (n > player.Gold) {
                    Util.WriteLine($"\n\tSorry, {player.Race}, you * DON'T * have that many Gold Pieces left.");
                } else {
                    player.Flares += n;
                    player.Gold -= n;
                }
            }
            Util.ClearScreen();
        }



        private static Player InitPlayer() {
            var race = Util.Menu("Please choose your race", Race.All, (_, i) => $"{i}"[0]).Item2;
            Util.ClearScreen();
            var gender = Util.Menu("Please choose your gender", Gender.All).Item2;
            Util.ClearScreen();

            var ab = GetPlayerExtraPoints(race) + race;
            Player player = new Player() {
                Race = race,
                Gender = gender,
                Dexterity = ab.Dexterity, Intelligence = ab.Intelligence, Strength = ab.Strength
            };
            AddPlayerArmor(player, false);
            AddPlayerWeapon(player, false);
            AddPlayerLamp(player, false);
            AddPlayerFlares(player);
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
            Util.WriteLine();
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
                Util.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Armor = choice;
                player.Gold -= choice.Cost(vendor);
            }
            // Util.ClearScreen();
        }

*/
