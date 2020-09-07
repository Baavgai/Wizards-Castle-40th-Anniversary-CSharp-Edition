using System;

namespace The_Wizard_s_Castle
{
    class Vendor : Character
    {
        public bool mad = false;
        public bool webbed = false;

        Vendor(string race, int dexterity, int intelligence, int strength)
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
    }
}
