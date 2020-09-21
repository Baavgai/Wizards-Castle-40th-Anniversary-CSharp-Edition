using System;
using System.Collections.Generic;

namespace WizardCastle {
    class RuneStaff : Item, IHasExec, IInventoryItem {
        public static readonly RuneStaff Instance = new RuneStaff();
        // public static readonly Treasure RuneStaff = all.Register(new Treasure("Rune Staff",    "gives te power of teleporation", _ => { }));

        private RuneStaff() : base("RuneStaff") { }
        public void Exec(State state) {
            var (player, _) = state;
            MapPos location = null;
            while (location == null) {
                // Util.ClearScreen();
                state.Write("\nTeleport where (Example: For Level 3, Row 5, Column 2 type: 3,5,2): ");
                location = MapPos.Parse(state.ReadLine().Result);
                if (!state.Map.ValidPos(location)) {
                    state.WriteLine("* Invalid * Coordinates");
                    location = null;
                }
            }
            player.Location = location;
            state.WriteLine($"\n\tTeleporting to: {location}");
            state.Sleep();
        }

        public void OnFound(State state) {
            Game.DefaultOnFoundMessage(state, this);
            state.WriteLine("You've found the RuneStaff!");
            state.Player.Add(this);
        }
}
}
