﻿using System;
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

        private int dexterity = 0;
        private int intelligence = 0;
        private int strength = 0;

        public int lampBurn = 0;
        public int Flares { get; set; } = 2;
        public Armor Armor { get; set; } = null;
        public Weapon Weapon { get; set; } = null;
        public Gender Gender { get; set; } = null;
        public Race Race { get; set; }

        public bool HasItem(IItem item) => Inventory.Any(x => x == item);
        public bool IsBlind => HasItem(Curse.Blind);

        public GameAction LastAction { get; set; }

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
