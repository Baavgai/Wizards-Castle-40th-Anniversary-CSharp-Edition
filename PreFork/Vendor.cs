using System;
using System.Collections.Generic;
using System.Linq;

namespace The_Wizard_s_Castle
{
    class Vendor : Character
    {
        public bool mad = true;
        public int webbedTurns = 0;
        public bool runeStaff = false;
        public Vendor (string race, int dexterity, int intelligence, int strength)
        {
            this.race = race;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
            this.strength = strength;
        }
        public static Vendor GetOrCreateVendor(string[,,] theMap, Player player, string locationStr)
        {
            Random rand = new Random();
            if (! GameCollections.vendorsDict.ContainsKey(locationStr))
            {
                Vendor vendor = new Vendor(theMap[player.location[0], player.location[1], player.location[2]], rand.Next(1, player.maxAttrib + 1), rand.Next(1, player.maxAttrib + 1), rand.Next(1, player.maxAttrib + 1));
                vendor.location[0] = player.location[0];
                vendor.location[1] = player.location[1];
                vendor.location[2] = player.location[2];
                vendor.dexterity += 8;
                vendor.intelligence += 8;
                vendor.strength += 8;
                GameCollections.vendorsDict.Add(locationStr, vendor);
            }
            return GameCollections.vendorsDict[locationStr];
        }
        public static string VendorMadMessage(Vendor vendor)
        {
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
    }
}
