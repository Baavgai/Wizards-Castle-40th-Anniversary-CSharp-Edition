using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    // monster must provide an instance of itself, so it can have an inventory, fight, etc.

    static class MonsterFactory {
        public static IContentFactory[] AllMonsters = new IContentFactory[] {
            new MonsterFactoryImpl("Balrog", strength: 5),
            new MonsterFactoryImpl("Bear", strength: 2),
            new MonsterFactoryImpl("Chimera", strength: 4),
            new MonsterFactoryImpl("Dragon", strength: 8, weaponBreakChance: true),
            new MonsterFactoryImpl("Gargoyle", strength: 3, weaponBreakChance: true),
            new MonsterFactoryImpl("Goblin", strength: 1),
            new MonsterFactoryImpl("Kobold", strength: 1),
            new MonsterFactoryImpl("Minotaur", strength: 4),
            new MonsterFactoryImpl("Ogre", strength: 4),
            new MonsterFactoryImpl("Orc", strength: 3),
            new MonsterFactoryImpl("Troll", strength: 3),
            new MonsterFactoryImpl("Wolf", strength: 1)
        };

        private class MonsterFactoryImpl : IContentFactory {
            private readonly bool weaponBreakChance;
            private readonly Abilities mods;
            public MonsterFactoryImpl(string name, int dexterity = 0, int intelligence = 0, int strength = 0, bool weaponBreakChance = false) {
                Name = name;
                mods = new Abilities() {
                    Strength = strength, Dexterity = dexterity, Intelligence = intelligence
                };
                this.weaponBreakChance = weaponBreakChance;
            }
            public string Name { get; }
            public override string ToString() => Name;
            public IContent Create() => new MonsterImpl(Name, mods, weaponBreakChance);

        }

        private class MonsterImpl : Mob {

            protected override Abilities Mods { get; }
            private readonly bool weaponBreakChance;

            public MonsterImpl(string name, Abilities mods, bool weaponBreakChance) : base(name) {
                Mods = mods;
                this.weaponBreakChance = weaponBreakChance;
            }


            public override void OnEntry(State state) {
                ResetStats(state);
                Game.DefaultItemMessage(this);
                if (Mad) {
                    Util.WriteLine($"\n{Util.RandPick(MadMessages.Value)(this)}\n");
                    Battle(state);
                } else {
                    Util.WriteLine($"\nThe {Name} doesn't seem to notice you.\n");
                }
            }

            protected override bool CheckWeaponBreak() => weaponBreakChance ? Util.RandInt(1, 11) > 9 : false;
        }

    }
}
