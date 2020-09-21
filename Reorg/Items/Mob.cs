using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    // interface IMob : IAbilitiesMutable, IItem {    }

    abstract class Mob : Abilities, IMob {
        public string Name { get; }
        public int WebbedTurns { get; set; } = 0;
        public bool Mad { get; protected set; } = true;

        public List<IInventoryItem> Inventory { get; } = new List<IInventoryItem>();

        protected List<IInventoryItem> inventory = new List<IInventoryItem>();

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


        protected virtual IEnumerable<GameAction> PlayerChoices(State state, bool firstAttackRound) {
            yield return new GameAction("Attack", Attack);
            yield return new GameAction("Retreat", Retreat);
            if (firstAttackRound) { yield return new GameAction("Bribe", Bribe); }
            if (state.Player.Intelligence > 14) { yield return new GameAction("Cast", Cast); }
        }

        protected virtual void OnDeath(State state) {
            var gold = Util.RandInt(1, 1001);
            state.WriteLine($"\nYou killed the evil {Name}");
            state.WriteLine($"You get his hoard of {gold} Gold Pieces");
            state.Player.Gold += gold;
            foreach (var item in inventory) {
                item.OnFound(state);
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
            state.WriteLine($"\n{Util.RandPick(MadMessages.Value)(this)}\n");
            Battle(state);
        }


        protected virtual void Battle(State state) {
            ResetStats(state);
            bool firstAttackRound = true;
            while (Mad && !IsDead && !state.Player.IsDead) {
                state.WriteLine($"\nYou are facing a {Name}!");
                if ((((Util.RandInt(101) + Dexterity) > 75) || state.Player.HasItem(Curse.Lethargy)) && firstAttackRound) {
                    MobAttack(state);
                    firstAttackRound = false;
                }
                if (!state.Player.IsDead) {
                    state.Menu("What would you like to do", PlayerChoices(state, firstAttackRound), (x, _) => x.Cmd).Item2.Exec(state);
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
                state.WriteLine($"The {Name} says I want the {treasure.Name}. Will you give it to me?");
                var choice = state.Menu($"Give the {Name} the {treasure}", new string[] { "Yes", "No" }).Item1 == 'Y';
                if (choice) {
                    state.WriteLine($"\nThe {Name} says, ok, just don't tell anyone.");
                    state.Player.Remove(treasure);
                    inventory.Add(treasure);
                    Mad = false;
                }
            }
            if (Mad) {
                state.WriteLine($"\nThe {Name} says, all I want is your life!");
            }
        }

        protected virtual void Cast(State state) {
            var choice = state.Menu("What spell do you want to cast", Spell.All).Item2;
            choice.Cast(state, this);
        }

        protected virtual void Retreat(State state) {
            if ((Util.RandInt(0, 101) - state.Player.Dexterity) > 50) {
                MobAttack(state);
            }
            if (state.Player.Strength > 0) {
                var choice = state.Menu("Retreat which way", Direction.All).Item2;
                choice.Exec(state);
                state.WriteLine($"\nYou retreat to the {choice.Name}!");
            }
        }

        protected virtual void Attack(State state) {
            if (state.Player.HasItem(Curse.BookStuck)) {
                state.WriteLine($"\nYou can't beat the {Name} to death with a book.");
            } else if (state.Player.Weapon == null) {
                state.WriteLine($"\nStupid {state.Player.Race}! You have no weapon and pounding on the {Name} won't do any good.");
            } else {
                state.WriteLine($"\nYou attack the {Name}!");
                if (Util.RandInt(1, 11) > 5) {
                    state.WriteLine("You hit it!");
                    int damage = state.Player.Weapon.CalcDamage();
                    Strength -= damage;
                    if (CheckWeaponBreak()) {
                        state.WriteLine($"Oh No! Your {state.Player.Weapon.Name} just broke!");
                        state.Player.Weapon = null;
                    }
                } else {
                    state.WriteLine("Drats! You Missed!");
                }
            }
        }

        protected virtual void MobAttack(State state) {
            if (WebbedTurns > 0) {
                state.WriteLine($"\nThe {Name} is caught in a web and can't attack.");
                WebbedTurns -= 1;
                if (WebbedTurns == 0) {
                    state.WriteLine($"\nThe web breaks!");
                }
            } else {
                state.WriteLine($"\nThe {Name} attacks!");
                if (Util.RandInt(1, 11) > 5) {
                    state.WriteLine("It hit you!");
                    int damage = Util.RandInt(1, 6) - state.Player.Armor.DamageAbsorb;
                    if (damage > 0) {
                        state.Player.Strength -= damage;
                    }
                    if (Util.RandInt(1, 11) > 9) {
                        state.WriteLine($"\nOh No! Your {state.Player.Armor.Name} armor is destroyed!");
                        state.Player.Armor = null;
                    }
                } else {
                    state.WriteLine("It Missed!");
                }
            }
        }

    }
}
