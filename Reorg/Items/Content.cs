using System;
using System.Collections.Generic;

namespace WizardCastle {
    class Content : Item, IContent {
        private readonly static List<IContent> all = new List<IContent>();
        public static IContent Book = all.Register(WizardCastle.Book.Instance);
        public static IContent Chest = all.Register(WizardCastle.Chest.Instance);
        public static IContent Orb = all.Register(WizardCastle.Orb.Instance);
        public static IContent Pool = all.Register(WizardCastle.Pool.Instance);

        public static readonly IContent Gold = all.Register(new Content("Gold",
            state => {
                var goldFound = Util.RandInt(1, 1001);
                state.WriteLine($"You've found {goldFound} Gold Pieces");
                state.Player.Gold += goldFound;
                state.CurrentCell.Clear();
            }));
        public static readonly IContent DownStairs = all.Register(new Content("DownStairs"));
        public static readonly IContent UpStairs = all.Register(new Content("UpStairs"));
        public static readonly IContent Exit = all.Register(new Content("Entrance/Exit"));
        public static readonly IContent SinkHole = all.Register(new Content("SinkHole",
            state => {
                state.WriteLine("You are falling!");
                state.Player.Location.Level += 1;
                state.Sleep();
            }));
        public static readonly IContent Warp = all.Register(new Content("Warp", 
            state => {
                state.WriteLine("The world swirls chaotically around you.  You have entered some kind of warp!");
                state.Player.Location = state.Map.RandPos();
                state.Sleep();
            }));

        public static readonly IContent Flares = all.Register(new Content("Flares", 
        state => {
            int flaresFound = Util.RandInt(1, 11);
            state.WriteLine($"You've found {flaresFound} flares");
            state.Player.Flares += flaresFound;
            state.CurrentCell.Clear();
        }));


        public static IContent[] All => all.ToArray();

        public static void Register(IContent item) {
            all.Add(item);
        }

        private readonly Action<State> onEntry;
        public Content(string name, Action<State> onEntry = null) : base(name) {
            this.onEntry = onEntry;
        }
        public void OnEntry(State state) {
            if (onEntry != null) {
                onEntry(state);
            } else {
                Game.DefaultItemMessage(state, this);
            }
        }
    }
}