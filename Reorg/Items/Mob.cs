using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    // interface IMob : IAbilitiesMutable, IItem {    }
     
    abstract class Mob : Abilities, IAbilitiesMutable, IItem, IContent {
        public string Name { get; }
        public int WebbedTurns { get; set; } = 0;
        public bool Mad { get; protected set; } = true;

        public List<IItem> Inventory { get; } = new List<IItem>();

        protected List<IItem> inventory = new List<IItem>();

        public Mob(string name, int dexterity, int intelligence, int strength) : base(dexterity, intelligence, strength) {
            Name = name;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Strength = strength;
        }
        public Mob(string name) { Name = name; }

        public bool IsDead => Strength < 1;
        public override string ToString() => Name;

        protected abstract bool CheckWeaponBreak();
        protected abstract Abilities Mods { get; }
        public abstract void OnEntry(State state);

        protected static Lazy<List<Func<Mob, string>>> MadMessages = new Lazy<List<Func<Mob, string>>>(() => new List<Func<Mob, string>>() {
            m => $"The {m.Name} sees you, snarls and lunges towards you!",
            m => $"The {m.Name} looks angrily at you moves in your direction!",
            m => $"The {m.Name} stops what it's doing and focuses its attention on you!",
            m => $"The {m.Name} looks at you agitatedly!",
            m => $"The {m.Name} says, you've come seeking treasure and instead have found death!",
            m => $"The {m.Name} growls and prepares for battle!",
            m => $"The {m.Name} says, you will be a small meal for a me!",
            m => $"The {m.Name} says, welcome to your death pitiful!"
            });


        protected virtual IEnumerable<SimplGameAction> PlayerChoices(State state, bool firstAttackRound) {
            yield return new SimplGameAction("Attack", Attack);
            yield return new SimplGameAction("Retreat", Retreat);
            if (firstAttackRound) { yield return new SimplGameAction("Bribe", Bribe); }
            if (state.Player.Intelligence > 14) { yield return new SimplGameAction("Cast", Cast); }
        }

        protected virtual void OnDeath(State state) {
            var gold = Util.RandInt(1, 1001);
            Util.WriteLine($"\nYou killed the evil {Name}");
            Util.WriteLine($"You get his hoard of {gold} Gold Pieces");
            state.Player.Gold += gold;
            foreach (var item in inventory) {
                Util.WriteLine($"You've recoverd the {item}");
                if (item == Treasure.RuneStaff) {
                    Util.WriteLine("You've found the RuneStaff!");
                }
                state.Player.Add(item);
            }
            state.CurrentCell.Clear();
        }

        protected void ResetStats(State state) {
            var ma = Mods + state.Player;
            Dexterity = ma.Dexterity;
            Intelligence = ma.Intelligence;
            Strength = ma.Strength;
        }


        public virtual void InitiateAttack(State state) {
            Mad = true;
            Util.WriteLine($"\n{Util.RandPick(MadMessages.Value)(this)}\n");
            Battle(state);
        }


        protected virtual void Battle(State state) {
            ResetStats(state);
            bool firstAttackRound = true;
            while (Mad && !IsDead && !state.Player.IsDead) {
                Util.WriteLine($"\nYou are facing a {Name}!");
                if ((((Util.RandInt(101) + Dexterity) > 75) || state.Player.HasItem(Curse.Lethargy)) && firstAttackRound) {
                    MobAttack(state);
                    firstAttackRound = false;
                }
                if (!state.Player.IsDead) {
                    Util.Menu("What would you like to do", PlayerChoices(state, firstAttackRound), (x, _) => x.Cmd).Item2.Exec(state);
                    if (!IsDead && Mad) {
                        MobAttack(state);
                    }
                }
                firstAttackRound = false;
            }
            if (IsDead) {
                OnDeath(state);
            }
        }

        protected virtual void Bribe(State state) {
            var treasures = state.Player.Inventory.Where(x => x is Treasure).ToList();
            if (treasures.Count > 0) {
                var treasure = Util.RandPick(treasures);
                Util.WriteLine($"The {Name} says I want the {treasure.Name}. Will you give it to me?");
                var choice = Util.Menu($"Give the {Name} the {treasure}", new string[] { "Yes", "No" }).Item1 == 'Y';
                if (choice) {
                    Util.WriteLine($"\nThe {Name} says, ok, just don't tell anyone.");
                    state.Player.Remove(treasure);
                    inventory.Add(treasure);
                    Mad = false;
                }
            }
            if (Mad) {
                Util.WriteLine($"\nThe {Name} says, all I want is your life!");
            }
        }

        protected virtual void Cast(State state) {
            var choice = Util.Menu("What spell do you want to cast", Spell.All).Item2;
            choice.Cast(state, this);
        }

        protected virtual void Retreat(State state) {
            if ((Util.RandInt(0, 101) - state.Player.Dexterity) > 50) {
                MobAttack(state);
            }
            if (state.Player.Strength > 0) {
                var choice = Util.Menu("Retreat which way", Direction.All).Item2;
                choice.Exec(state);
                Util.WriteLine($"\nYou retreat to the {choice.Name}!");
            }
        }

        protected virtual void Attack(State state) {
            if (state.Player.HasItem(Curse.BookStuck)) {
                Util.WriteLine($"\nYou can't beat the {Name} to death with a book.");
            } else if (state.Player.Weapon == null) {
                Util.WriteLine($"\nStupid {state.Player.Race}! You have no weapon and pounding on the {Name} won't do any good.");
            } else {
                Util.WriteLine($"\nYou attack the {Name}!");
                if (Util.RandInt(1, 11) > 5) {
                    Util.WriteLine("You hit it!");
                    int damage = state.Player.Weapon.CalcDamage();
                    Strength -= damage;
                    if (CheckWeaponBreak()) {
                        Util.WriteLine($"Oh No! Your {state.Player.Weapon.Name} just broke!");
                        state.Player.Weapon = null;
                    }
                } else {
                    Util.WriteLine("Drats! You Missed!");
                }
            }
        }

        protected virtual void MobAttack(State state) {
            if (WebbedTurns > 0) {
                Util.WriteLine($"\nThe {Name} is caught in a web and can't attack.");
                WebbedTurns -= 1;
                if (WebbedTurns == 0) {
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

