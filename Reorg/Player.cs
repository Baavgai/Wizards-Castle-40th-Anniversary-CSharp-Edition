using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    class Player : IAbilities {
        
        private readonly List<IItem> inventory = new List<IItem>();
        public IItem[] Inventory => inventory.ToArray();
        public void Add(IItem item) {
            inventory.Add(item);
        }
        public void Remove(IItem item) {
            inventory.Remove(item);
        }
        // public List<IItem> Inventory { get; } = new List<IItem>();

        private int dexterity = 0;
        private int intelligence = 0;
        private int strength = 0;
        // public int turns = 0;
        public int lampBurn = 0;
        public int Flares { get; set; } = 2;
        public Armor Armor { get; set; } = null;
        public Weapon Weapon { get; set; } = null;
        // public bool blind = false;
        // public bool bookStuck = false;
        // public bool forgetfulness = false;
        // public bool leech = false;
        // public bool lethargy = false;
        // public bool lamp = false;
        // public bool orbOfZot = false;
        // public bool runeStaff = false;

        public bool HasItem(IItem item) => Inventory.Any(x => x == item);
        public bool IsBlind => HasItem(Curse.Blind);

        public string Race { get; set; }
        public string Sex { get; set; }
        public int Gold { get; set; } = 60;

        public MapPos Location { get; set; } = Game.StartingLocation;
        public int Turn { get; set; } = 0;


        private int MaxCap(int attr) =>
            attr > Game.MaxAttrib ? Game.MaxAttrib : attr;

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
