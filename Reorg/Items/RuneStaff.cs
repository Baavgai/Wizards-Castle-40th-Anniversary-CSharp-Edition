using System;
using System.Collections.Generic;

namespace WizardCastle {
    class RuneStaff : Item, IHasExec {
        public static readonly RuneStaff Instance = new RuneStaff();
        // public static readonly Treasure RuneStaff = all.Register(new Treasure("Rune Staff",    "gives te power of teleporation", _ => { }));

        private RuneStaff() : base("RuneStaff") { }
        public void Exec(State state) {
            var (player, _) = state;
            MapPos location = null;
            while (location == null) {
                // Util.ClearScreen();
                Util.Write("\nTeleport where (Example: For Level 3, Row 5, Column 2 type: 3,5,2): ");
                location = MapPos.Parse(Util.ReadLine());
                if (!state.Map.ValidPos(location)) {
                    Util.WriteLine("* Invalid * Coordinates");
                    location = null;
                }
            }
            player.Location = location;
            Util.WriteLine($"\n\tTeleporting to: {location}");
            Util.Sleep();
        }
    }
}
