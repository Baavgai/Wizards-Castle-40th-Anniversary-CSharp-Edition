using System;
using System.Collections.Generic;

using The_Wizard_s_Castle.Models;

namespace The_Wizard_s_Castle {
    class Book : RoomContentImpl, IHasOpen {

        public Book() : base("Book") { }

        public void Open(State state) {
            if (!state.Player.blind) {
                state.CurrentCell.Clear();
                Util.WriteLine($"\nYou open the book and {Util.RandPick(AllHandlers)(state)}");
            } else {
                Util.WriteLine($"Sorry, {state.Player.Race}, it's not written in Braille!");
            }
            Util.WaitForKey();
        }

        private static readonly Func<State, string>[] AllHandlers = new Func<State, string>[] {
            s => "it's another volume of Zot's poetry. Yeech!",
            s => $"it's an old copy of play {Util.RandRace()}.",
            s => $"it's a {Util.RandPick(GameCollections.Monsters)} cook book.",
            s => $"it's a self-improvement book on how to be a better {Util.RandRace()}.",
            s => {
                s.Player.Dexterity = s.Player.MaxAttrib;
                return "it's a manual of dexterity!";
            },
            s => {
                s.Player.Intelligence = s.Player.MaxAttrib;
                return "it's a manual of intelligence!";
            },
            s => {
                s.Player.Strength = s.Player.MaxAttrib;
                return "it's a manual of strength!";
            },
            s => $"it's a treasure map leading to a pile of gold at ({s.Map.FindGold().Display}).",
            s => {
                s.Player.blind = true;
                return $"FLASH! OH NO! YOU ARE NOW A BLIND {s.Player.Race}!";
            },
            s => {
                s.Player.bookStuck = true;
                return $"it sticks to your hands. Now you can't grab your {s.Player.weapon}!";
            }
        };

    }
}
