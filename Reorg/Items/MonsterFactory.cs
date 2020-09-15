using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    // monster must provide an instance of itself, so it can have an inventory, fight, etc.
    interface IMonster : IContent {
        public List<IItem> Inventory { get; }
    }

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
        private static readonly Func<IMonster, string>[] MadMessages = new Func<IMonster, string>[] {
                m => $"The {m.Name} sees you, snarls and lunges towards you!",
                m => $"The {m.Name} looks angrily at you moves in your direction!",
                m => $"The {m.Name} stops what it's doing and focuses its attention on you!",
                m => $"The {m.Name} looks at you agitatedly!",
                m => $"The {m.Name} says, you've come seeking treasure and instead have found death!",
                m => $"The {m.Name} growls and prepares for battle!",
                m => $"The {m.Name} says, you will be a small meal for a me!",
                m => $"The {m.Name} says, welcome to your death pitiful!"
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
            public IContent Create() => new MonsterImpl(Name, mods, weaponBreakChance);

        }

        private class MonsterImpl : Item, IMonster {


            public bool mad = true;
            public int webbedTurns = 0;
            public bool runeStaff = false;

            private Abilities Mods { get; }
            public List<IItem> Inventory { get; } = new List<IItem>();
            private readonly bool weaponBreakChance;

            public MonsterImpl(string name, Abilities mods, bool weaponBreakChance) : base(name) {
                Mods = mods;
                this.weaponBreakChance = weaponBreakChance;
            }


            public void OnEntry(State state) {
                Util.WriteLine($"\nHere you find '{Name}'");
                if (mad) {
                    Util.WriteLine($"\n{Util.RandPick(MadMessages)(this)}\n");
                    BattleSequence(state);
                } else {
                    Console.WriteLine($"\nThe {Name} doesn't seem to notice you.\n");
                }
                // if (monster.strength < 1) {                theMap[player.location[0], player.location[1], player.location[2]] = "-";            }
                // if (!player.location.SequenceEqual(monster.location)) {                return true;            }
            }

            private IEnumerable<string> PlayerChoices(State state, bool firstAttackRound) {
                yield return "Attack";
                yield return "Retreat";
                if (firstAttackRound) { yield return "Bribe"; }
                if (state.Player.Intelligence > 14) { yield return "Cast"; }
            }

            private void BattleSequence(State state) {
                var ma = Mods + state.Player;
                bool firstAttackRound = true;
                while (mad && ma.Strength > 0 && state.Player.Strength > 0) {
                    Util.WriteLine($"\nYou are facing a {Name}!");
                    if ((((Util.RandInt(101) + ma.Dexterity) > 75) || state.Player.HasItem(Curse.Lethargy)) && firstAttackRound) {
                        MonsterAttack(state);
                        firstAttackRound = false;
                    }
                    if (state.Player.Strength > 0) {
                        switch (Util.Menu("What would you like to do", PlayerChoices(state, firstAttackRound)).Item2) {
                            case "Attack":
                                PlayerAttack(state, ma);
                                break;
                            case "Bribe":
                                PlayerBribe(state);
                                break;
                            case "Cast":
                                PlayerCast(state);
                                break;
                            case "Retreat":
                                PlayerRetreat(state);
                                break;
                        }
                        if (ma.Strength > 0 && mad) { 
                            MonsterAttack(state);
                        }
                    }
                    firstAttackRound = false;
                }
                if (ma.Strength < 1) {
                    var gold = Util.RandInt(1, 1001);
                    Util.WriteLine($"\nYou killed the evil {Name}");
                    Util.WriteLine($"You get his hoard of {gold} Gold Pieces");
                    state.Player.Gold += gold;
                    /*
                    if (monster.runeStaff) {
                        Console.WriteLine("You've found the RuneStaff!");
                        player.runeStaff = true;
                    }
                    if (monster.treasures.Count > 0) {
                        Console.WriteLine($"You've recoverd the {monster.treasures[0]}");
                        player.treasures.Add(monster.treasures[0]);
                    }
                    */
                    state.CurrentCell.Clear();
                    Util.WriteLine();
                }
            }

            private void PlayerBribe(State state) {
                var treasures = state.Player.Inventory.Where(x => x is Treasure).ToList();
                if (treasures.Count > 0) {
                    var treasure = Util.RandPick(treasures);
                    Util.WriteLine($"The {Name} says I want the {treasure.Name}. Will you give it to me?");
                    var choice = Util.Menu($"Give the {Name} the {treasure}", new string[] { "Yes", "No" }).Item1 == 'Y';
                    if (choice) {
                        Util.WriteLine($"\nThe {Name} says, ok, just don't tell anyone.");
                        state.Player.Remove(treasure);
                        Inventory.Add(treasure);
                        mad = false;
                    }
                }
                if (mad) {
                    Util.WriteLine($"\nThe {Name} says, all I want is your life!");
                }
            }

            void PlayerCast(State state) {
                /*
                string question = $"What spell do you want to cast";
                Dictionary<char, string> choicesDict = new Dictionary<char, string>
                    {
                    { 'W', "Web" },
                    { 'F', "Fireball" },
                    { 'D', "Deathspell" }
                };
                string[] choice = battleMenu.Menu(question, choicesDict, GameCollections.ErrorMesssages);
                switch (choice[0]) {
                    case "W":
                        Console.WriteLine($"\nYou've caught the {monster.race} in a web, now it can't attack");
                        monster.webbedTurns = rand.Next(1, 11);
                        player.strength -= 1;
                        break;
                    case "F":
                        Console.WriteLine($"\nYou blast the {monster.race} with a fireball.");
                        monster.strength -= rand.Next(1, 11);
                        player.intelligence -= 1;
                        player.strength -= 1;
                        break;
                    case "D":
                        if ((player.intelligence > monster.intelligence) && (rand.Next(1, 9) < 7)) {
                            Console.WriteLine($"\nDEATH! The {monster.race} is dead.");
                            monster.strength = 0;
                        } else {
                            Console.WriteLine($"\nDEATH! The STUPID {player.race}'s death.");
                            player.strength = 0;
                            Util.WaitForKey();
                        }
                        break;
                }
                */
            }

            private void PlayerRetreat(State state) {
                if ((Util.RandInt(0, 101) - state.Player.Dexterity) > 50) {
                    MonsterAttack(state);
                }
                if (state.Player.Strength > 0) {
                    var choice = Util.Menu("Retreat which way", Direction.AllDirections).Item2;
                    choice.Exec(state);
                    Util.WriteLine($"\nYou retreat to the {choice.Name}!");
                    Util.WaitForKey();
                }
            }

            void PlayerAttack(State state, Abilities ma) {
                if (state.Player.HasItem(Curse.BookStuck)) {
                    Util.WriteLine($"\nYou can't beat the {Name} to death with a book.");
                } else if (state.Player.Weapon == null) {
                    Util.WriteLine($"\nStupid {state.Player.Race}! You have no weapon and pounding on the {Name} won't do any good.");
                } else {
                    Util.WriteLine($"\nYou attack the {Name}!");
                    if (Util.RandInt(1, 11) > 5) {
                        Util.WriteLine("You hit it!");
                        int damage = state.Player.Weapon.CalcDamage();
                        ma.Strength -= damage;
                        if (weaponBreakChance && Util.RandInt(1, 11) > 9) {
                            Util.WriteLine($"Oh No! Your {state.Player.Weapon.Name} just broke!");
                            state.Player.Weapon = null;
                        }
                    } else {
                        Util.WriteLine("Drats! You Missed!");
                    }
                }
            }

            private void MonsterAttack(State state) {
                if (webbedTurns > 0) {
                    Util.WriteLine($"\nThe {Name} is caught in a web and can't attack.");
                    webbedTurns -= 1;
                    if (webbedTurns == 0) {
                        Util.WriteLine($"\nThe web breaks!");
                    }
                } else {
                    Util.WriteLine($"\nThe {Name} attacks!");
                    if (Util.RandInt(1, 11) > 5) {
                        Util.WriteLine("It hit you!");
                        int damage = Util.RandInt(1, 6) - state.Player.Armor.DamageAbsorb;
                        if (damage > 0) {
                            state.Player.Strength -= damage;
                        }
                        if (Util.RandInt(1, 11) > 9) {
                            Util.WriteLine($"\nOh No! Your {state.Player.Armor.Name} armor is destroyed!");
                            state.Player.Armor = null;
                        }
                    } else {
                        Util.WriteLine("It Missed!");
                    }
                }
            }
        }

    }
}
