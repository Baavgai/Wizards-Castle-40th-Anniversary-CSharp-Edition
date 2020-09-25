using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    public class Player : IAbilitiesMutable {
        private readonly List<IInventoryItem> inventory = new List<IInventoryItem>();
        public IInventoryItem[] Inventory => inventory.ToArray();
        public void Add(IInventoryItem item) => inventory.Add(item);
        public void Remove(IInventoryItem item) => inventory.Remove(item);
        public bool HasItem(IInventoryItem item) => Inventory.Any(x => x == item);

        public string Name => "Player";

        private int dexterity = 0;
        private int intelligence = 0;
        private int strength = 0;

        public int Flares { get; set; } = 0;
        public IArmor Armor { get; set; } = null;
        public IWeapon Weapon { get; set; } = null;
        public Gender Gender { get; set; } = null;
        public Race Race { get; set; }


        public bool IsBlind => HasItem(Curse.Blind);
        public bool HasLamp => HasItem(Lamp.Instance);


        public IGameAction LastAction { get; set; }

        public int Gold { get; set; } = 60;

        public MapPos Location { get; set; } = Game.StartingLocation;

        public bool IsDead => Strength < 1;

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
