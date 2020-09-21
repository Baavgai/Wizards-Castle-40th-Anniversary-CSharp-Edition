using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    interface IVendor : IMob {    }

    static class VendorFactory {

        public static IVendor Create() => new Vendor();

        private static int RndAttr() => Util.RandInt(Game.MaxAttrib) + 1;

        private class Vendor : Mob, IVendor {
            public Race Race { get; }
            // private static readonly Abilities Mods => new Abilities();

            protected override Abilities Mods => new Abilities();

            public Vendor() : base("Vendor", RndAttr(), RndAttr(), RndAttr()) {
                Race = Util.RandPick(WizardCastle.Race.All);
            }

            public override void OnEntry(State state) {
                if (Mad) {
                    state.WriteLine($"\n{Util.RandPick(MadMessages.Value)(this)}\n");
                    Battle(state);
                } else {
                    Game.DefaultItemMessage(state, this);
                }

            }

            public override void InitiateAttack(State state) {
                // GameCollections.AllVendorMad = true;
                var allVendors = state.Map
                    .Search((cell, pos) => !cell.IsEmpty && cell.Contents is Vendor)
                    .Select(x => x.cell.Contents)
                    .Cast<Vendor>();
                foreach (var x in allVendors) {
                    x.Mad = true;
                }
                state.WriteLine($"\n{Util.RandPick(MadMessages.Value)(this)}\n");
                Battle(state);
            }

            public void Trade(State state) {
                var (player, _) = state;
                var treasures = player.Inventory.Where(x => x is Treasure).ToList();
                if (treasures.Count < 1 && player.Gold < 1000) {
                    state.WriteLine($"Sorry, {player.Race}. You are too poor to trade.");
                    // SharedMethods.WaitForKey();
                } else {
                    if (treasures.Count > 0) {
                        foreach (var item in treasures) {
                            var offerAmount = Util.RandInt(1, 5001);
                            if (state.Menu($"Do you want to sell {item} for {offerAmount}", new string[] { "Yes", "No" }).Item1 == 'Y') {
                                state.WriteLine($"\nYou have accepted the Vendor's offer for {item}.");
                                Inventory.Add(item);
                                player.Remove(item);
                                player.Gold += offerAmount;
                            }
                        }
                        if (player.Gold < 1000) {
                            state.WriteLine($"\nSorry, {player.Race}. You are too poor to trade.");
                            // SharedMethods.WaitForKey();
                        }
                    }
                    if ((player.Gold >= Armor.Leather.Cost()) && (player.Armor != Armor.Plate)) {
                        Game.AddPlayerArmor(state, state.Player);
                    }
                    if ((player.Gold > Weapon.Dagger.Cost()) && (player.Weapon != Weapon.Sword)) {
                        Game.AddPlayerWeapon(state, state.Player);
                    }
                    if ((player.Gold > Lamp.Instance.Cost()) && (!player.HasItem(Lamp.Instance))) {
                        Game.AddPlayerLamp(state, state.Player);
                    }
                    if (player.Gold > 999) {
                        AddPlayerPotion(state);
                    }
                }
            }
            private static void AddPlayerPotion(State state) {
                const int cost = 1000;
                var done = state.Player.Gold < cost;
                while (!done) {
                    var choice = Game.PurchaseMenu(state, state.Player, "Potion", AbilityItem.All, _ => cost);
                    if (choice == null) {
                        done = true;
                    } else {
                        state.WriteLine($"\n{choice}={choice.ApplyAmount(state.Player, Util.RandInt(1, 6))}");
                        state.Player.Gold -= cost;
                        done = state.Player.Gold < cost;
                    }
                }
            }

            protected override bool CheckWeaponBreak() => throw new NotImplementedException();
        }

    }
}

