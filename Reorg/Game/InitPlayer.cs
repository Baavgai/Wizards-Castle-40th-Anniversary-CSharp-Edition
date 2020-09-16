using System.Linq;
using System.Collections.Generic;
using System;

namespace WizardCastle {
    internal static partial class Game {

        private static Abilities GetPlayerExtraPoints(Race race) {
            var result = new Abilities();
            var extraPoints = race.ExtraPoints;
            int attr = 0;
            while (extraPoints > 0) {
                var aName = attr == 0 ? "Dexterity" : (attr == 1 ? "Intelligence" : "Strength");
                Util.Write($"\nYou have {extraPoints} points left, how many to add to {aName}: ");
                var amt = Util.ReadDigit();
                if (amt > extraPoints) {
                    Util.WriteLine($"\n\tSorry, {race.Name}, you * DON'T * have that many points left to distribute");
                } else {
                    extraPoints -= amt;
                    if (attr == 0) {
                        result.Dexterity += amt;
                    } else if (attr == 1) {
                        result.Intelligence += amt;
                    } else {
                        result.Strength += amt;
                    }
                    attr = (attr + 1) % 3;
                }
            }
            return result;
        }

        private static void AddPlayerArmor(Player player) {
            Util.WriteLine();
            // var items = new List<Tuple<A>>
            var items = Armor.All
                .Select(x => new Tuple<Armor, string>(x, $"{x.Name}, {x.IntialCost} Gold Pieces"))
                .Append(new Tuple<Armor, string>(null, "None, 0 Gold Pieces"))
                .ToList();
            var choice = Util.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of Armor do you want to purchase", items, (x, i) => $"{i}"[0]).Item2.Item1;
            if (choice == null) {

            } else if (choice.IntialCost > player.Gold) {
                Util.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Armor = choice;
                player.Gold -= choice.IntialCost;
            }
            Util.ClearScreen();
        }

        private static void AddPlayerWeapon(Player player) {
            Util.WriteLine();
            var items = Weapon.All
                .Select(x => new Tuple<Weapon, string>(x, $"{x.Name}, {x.IntialCost} Gold Pieces"))
                .Append(new Tuple<Weapon, string>(null, "None, 0 Gold Pieces"))
                .ToList();
            var choice = Util.Menu($"You have {player.Gold} Gold Pieces to buy items, what type of Weapon do you want to purchase", items, (x, i) => $"{i}"[0]).Item2.Item1;
            if (choice == null) {

            } else if (choice.IntialCost > player.Gold) {
                Util.WriteLine($"\n\tSorry, {player.Race}, you don't have that much gold left.");
            } else {
                player.Weapon = choice;
                player.Gold -= choice.IntialCost;
            }
            Util.ClearScreen();
        }

        private static void AddPlayerLamp(Player player) {
            const int cost = 20;
            if (player.Gold >= cost) {
                var choice = Util.Menu("Would you like to purchase a lamp", new Dictionary<char, string>
                {
                {'Y', $"Purchase lamp ({cost} Gold Pieces)"},
                {'N', "Don't purchase"}
            }).Item1 == 'Y';
                if (choice) {
                    player.Gold -= cost;
                    // player.lamp = true;
                }
                Util.ClearScreen();
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
            AddPlayerArmor(player);
            AddPlayerWeapon(player);
            AddPlayerLamp(player);
            AddPlayerFlares(player);
            return player;
        }


    }
}