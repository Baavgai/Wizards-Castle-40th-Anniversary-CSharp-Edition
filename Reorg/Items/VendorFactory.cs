using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    interface IVendor : IContent, IAbilities {

    }

    static class VendorFactory {

        public static IContent Create() => new Vendor();
        
        private static int RndAttr() => Util.RandInt(Game.MaxAttrib) + 1;

        private class Vendor : Mob, IVendor {
            public bool mad = true;
            public List<IItem> Inventory { get; } = new List<IItem>();
            public Race Race { get; }
            public Vendor() : base("Vendor", RndAttr(), RndAttr(), RndAttr()) {
                Race = Util.RandPick(WizardCastle.Race.All);
            }

            public void OnEntry(State state) => Game.DefaultItemMessage(this);

            public void Trade(State state) {
                var (player, _) = state;
                var treasures = player.Inventory.Where(x => x is Treasure).ToList();
                if (treasures.Count < 1 && player.Gold < 1000) {
                    Console.WriteLine($"Sorry, {player.Race}. You are too poor to trade.");
                    // SharedMethods.WaitForKey();
                } else {
                    if (treasures.Count > 0) {
                        foreach (var item in treasures) {
                            var offerAmount = Util.RandInt(1, 5001);
                            if (Util.Menu($"Do you want to sell {item} for {offerAmount}", new string[] { "Yes", "No" }).Item1 == 'Y') {
                                Console.WriteLine($"\nYou have accepted the Vendor's offer for {item}.");
                                Inventory.Add(item);
                                player.Remove(item);
                                player.Gold += offerAmount;
                            }
                        }
                        if (player.Gold < 1000) {
                            Console.WriteLine($"\nSorry, {player.Race}. You are too poor to trade.");
                            // SharedMethods.WaitForKey();
                        }
                    }
                    if ((player.Gold >= Armor.Leather.Cost()) && (player.Armor != Armor.Plate)) {
                        Game.AddPlayerArmor(state.Player);
                    }
                    if ((player.Gold > Weapon.Dagger.Cost()) && (player.Weapon != Weapon.Sword)) {
                        Game.AddPlayerWeapon(state.Player);
                    }
                    if ((player.Gold > VendorItem.Lamp.Cost()) && (!player.HasItem(VendorItem.Lamp))) {
                        Game.AddPlayerLamp(state.Player);
                    }
                    if (player.Gold > 999) {
                        AddPlayerPotion(player);
                    }
                }
            }
            private static void AddPlayerPotion(Player player) {
                const int cost = 1000;
                var done = player.Gold < cost;
                while (!done) {
                    var choice = Game.PurchaseMenu(player, "Potion", AbilityItem.All, _ => cost);
                    if (choice == null) {
                        done = true;
                    } else {
                        Util.WriteLine($"\n{choice}={choice.ApplyAmount(player, Util.RandInt(1, 6))}");
                        player.Gold -= cost;
                        done = player.Gold < cost;
                    }
                }
            }
        }

    }






    /*
     * static class MonsterFactory {
    public static string VendorMadMessage(Vendor vendor) {
        List<string> messageList = new List<string>
        {
            $"The {vendor.race} sees you, snarls and lunges towards you!",
            $"The {vendor.race} looks angrily at you moves in your direction!",
            $"The {vendor.race} stops what it's doing and focuses its attention on you!",
            $"The {vendor.race} looks at you agitatedly!",
            $"The {vendor.race} says, you've come seeking treasure and instead have found death!",
            $"The {vendor.race} growls and prepares for battle!",
            $"The {vendor.race} says, you will be a small meal for a me!",
            $"The {vendor.race} says, welcome to your death pitiful!"
        };
        return messageList[new Random().Next(0, messageList.Count)];
    }
    */
}
}
