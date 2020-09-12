using System;

namespace WizardCastle {
    class RoomGold : Item, IHasOnEntry {
        public RoomGold(): base("Gold", ItemType.Content) { }

        public void OnEntry(State state) {
            var goldFound = Util.RandInt(1, 1001);
            Util.WriteLine($"You've found {goldFound} Gold Pieces");
            state.Player.Gold += goldFound;
            state.CurrentCell.Clear();
        }
    }

    /*
        public static readonly List<RoomContent> AllContent = new List<RoomContent>() {
            new RoomContent("Book"),
            new RoomContent("Chest"),
            new RoomContent("DownStairs"),
            new RoomContent("UpStairs"),
            new RoomContent("Flares", onEntry: (item, player) => {
                int flaresFound = Util.RandInt(1, 11);
                Util.WriteLine($"You've found {flaresFound} flares");
                player.flares += flaresFound;
                return true;
                // theMap[player.location[0], player.location[1], player.location[2]] = "-";
            }),
            new RoomContent("Gold", onEntry: (item, player) => {
                var goldFound = Util.RandInt(1, 1001);
                Util.WriteLine($"You've found {goldFound} Gold Pieces");
                player.gold += goldFound;
                return true;
            }),
            new RoomContent("Orb"),
            new RoomContent("Pool"),
            new RoomContent("SinkHole"),
            new RoomContent("Vendor"),
            new RoomContent("Warp"),
            new RoomContent("X"),
            new RoomContent("Entrance/Exit")
        };

        /*
         *                 case "Flares":
                    int flaresFound = rand.Next(1, 11);
                    Console.WriteLine("You've found {0} flares", flaresFound);
                    player.flares += flaresFound;
                    theMap[player.location[0], player.location[1], player.location[2]] = "-";
                    break;

                case "Gold":
                    int goldFound = rand.Next(1, 1001);
                    Console.WriteLine("You've found {0} Gold Pieces", goldFound);
                    player.gold += goldFound;
                    theMap[player.location[0], player.location[1], player.location[2]] = "-";
                    break;

         * Console.WriteLine($"\nHere you find '{theMap[player.location[0], player.location[1], player.location[2]]}'");
            "Book",
            "Chest",
            "DownStairs",
            "UpStairs",
            "Flares",
            "Gold",
            "Orb",
            "Pool",
            "SinkHole",
            "Vendor",
            "Warp",
            "x",
            "Entrance/Exit"

        */


    }
}
