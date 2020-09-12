using System;
using System.Collections.Generic;
using System.Linq;
using YWMenuNS;

namespace WizardCastle {
    class Player : IAbilities {
        public int MaxAttrib => 18;
        public List<Item> Inventory { get; } = new List<Item>();
        
        private int dexterity = 0;
        private int intelligence = 0;
        private int strength = 0;
        public int turns = 0;
        public int lampBurn = 0;
        public int flares = 0;
        public string armor = "";
        public Weapon Weapon { get; set; } = null;
        public bool blind = false;
        public bool bookStuck = false;
        public bool forgetfulness = false;
        public bool leech = false;
        public bool lethargy = false;
        public bool lamp = false;
        public bool orbOfZot = false;
        public bool runeStaff = false;

        public string Race { get; set; }
        public string Sex { get; set; }
        public string Gold { get; set; }

        public MapPos Location { get; set; }


        private int MaxCap(int attr) =>
            attr > MaxAttrib ? MaxAttrib : attr;

        public int Dexterity {
            get => dexterity;
            set { dexterity = MaxCap(dexterity + value); }
        }
        public int Intelligence {
            get => intelligence;
            set { intelligence = MaxCap(intelligence + value); }
        }
        public int Strength {
            get => strength;
            set { strength = MaxCap(strength + value); }
        }



    }
}
