using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;
using The_Wizard_s_Castle.Models;

namespace The_Wizard_s_Castle {
    class RoomContent : Item {
        private readonly Func<RoomContent, Player, bool> onEntry;
        private readonly Func<RoomContent, Player, char, bool> onCommand;
        private RoomContent(string name, Func<RoomContent, Player, bool> onEntry = null, Func<RoomContent, Player, char, bool> onCommand = null) : base(name) {
            this.onEntry = onEntry;
            this.onCommand = onCommand;
        }
        public override ItemType ItemType => ItemType.Content;

        public MapPos Location { get; set; } = MapPos.Void;

        public bool OnEntry(Player player) {
            Util.WriteLine($"\nHere you find '{Name}'");
            return onEntry?.Invoke(this, player) ?? false;
        }
        public bool OnCommand(Player player, char command) => onCommand?.Invoke(this, player, command) ?? false;


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
