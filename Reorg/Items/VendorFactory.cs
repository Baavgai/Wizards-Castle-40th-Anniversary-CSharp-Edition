using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    interface IVendor : IContent, IAbilities {

    }

    static class VendorFactory {

        public static IContent Create() => new Vendor(Util.RandPick(Race.All).Name);
        
        private static int RndAttr() => Util.RandInt(Game.MaxAttrib) + 1;

        private class Vendor : Mob, IVendor {
            public bool mad = true;
            public int webbedTurns = 0;
            public bool runeStaff = false;
            public string Race { get; }
            public Vendor(string race) : base("Vendor", RndAttr(), RndAttr(), RndAttr()) {
                Race = race;
            }
            

            public void OnEntry(State state) => Game.DefaultItemMessage(this);
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
