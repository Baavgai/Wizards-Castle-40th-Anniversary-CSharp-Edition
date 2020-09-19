using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    class GameAction : IHasExec, IHasName {
        private readonly static List<GameAction> all = new List<GameAction>();
        public static GameAction[] All => all.ToArray();
        private static GameAction Create(char cmd, string name, Action<State> action, Func<State, bool> isAvailable = null) =>
            all.Register(new GameAction(cmd, name, action, isAvailable));

        public static readonly GameAction Map = Create('M', "Show map",
            state => Game.DisplayLevel(state));

        public static readonly GameAction Open = Create('O', "Open book or chest",
        state => {
            if (state.CurrentCell.Contents is IHasOpen item) {
                item.Open(state);
            } else {
                Util.WriteLine($"\n{Game.RandErrorMsg()}\n");
            }
        }, s => s.CurrentCell.Contents is IHasOpen
            );

        public static readonly GameAction PoolDrink = Create('P', "Drink from pool",
            state => (state.CurrentCell.Contents as Pool).Drink(state),
            s => s.CurrentCell.Contents is Pool
            );

        public static readonly GameAction Teleport = Create('T', "Use the Runestaff to teleport",
            state => (state.CurrentCell.Contents as RuneStaff).Exec(state),
            s => s.Player.HasItem(RuneStaff.Instance));

        public static readonly GameAction Up = Create('U', "Up stairs",
                s => s.Player.Location += new MapPos(level: -1),
                s => s.CurrentCell.Contents == Content.UpStairs && s.Player.Location.Level > 0
                );

        public static readonly GameAction Down = Create('D', "Down stairs",
                s => s.Player.Location += new MapPos(level: 1),
                s => s.CurrentCell.Contents == Content.DownStairs && s.Player.Location.Level < s.Map.Levels - 1
                );

        public static readonly GameAction North = Create('N', "North",
            state => {
                if (state.CurrentCell == Content.Exit) {
                    state.Done = true;
                } else {
                    Direction.North.Exec(state);
                }
            });

        public static readonly GameAction South = Create('S', "South", Direction.South.Exec);

        public static readonly GameAction East = Create('E', "East", Direction.East.Exec);

        public static readonly GameAction West = Create('W', "West", Direction.West.Exec);

        public static readonly GameAction Gaze = Create('G', "Gaze into crystal orb",
            state => (state.CurrentCell.Contents as Orb).Gaze(state),
            s => s.CurrentCell.Contents is Orb
            );

        public static readonly GameAction Flare = Create('F', "Light a flare",
                state => {
                    if (state.Player.IsBlind) {
                        Util.WriteLine("Lighting a flare won't do you any good since you are BLIND!");
                    } else {
                        Game.RevealMapArea(state, state.Player.Location);
                        state.Player.Flares -= 1;
                        Game.DisplayLevel(state);
                        Util.WriteLine("The flare pierces the darkness.");
                    }
                }, s => s.Player.Flares > 0);

        public static readonly GameAction ShineLamp = Create('L', "Shine lamp into adjacent room",
            Lamp.Instance.Exec, s => s.Player.HasLamp
            );

        public static readonly GameAction Quit = Create('Q', "Quit the game", s => s.Done = true);

        public static readonly GameAction Attack = Create('A', "Attack monster or vendor", 
            state => (state.CurrentCell.Contents as Mob).InitiateAttack(state),
            s => s.CurrentCell.Contents is Mob
            );

        // won't happen from main menu
        // public static readonly GameAction Retreat = Create('R', "Retreat from battle", "(R)ETREAT from battle.");
        // public static readonly GameAction Bribe = Create('B', "Bribe monster or vendor", "(B)RIBE a monster or a angry vendor.");

        public static readonly GameAction ViewInstructions = Create('V', "View Instructions", action: _ => Game.ShowInstructions());

        // public static readonly GameAction Trade = Create('Z', "Trade with Vendor");




        private readonly Action<State> action;
        private readonly Func<State, bool> isAvailable;

        public GameAction(char cmd, string name, Action<State> action, Func<State, bool> isAvailable = null) {
            Cmd = cmd;
            Name = name;
            this.action = action;
            this.isAvailable = s => isAvailable == null ? true : isAvailable(s);
        }
        public GameAction(string name, Action<State> action, Func<State, bool> isAvailable = null) : this(name[0], name, action, isAvailable) { }
        public char Cmd { get; }
        public string Name { get; }
        public void Exec(State state) {
            if (isAvailable(state)) {
                action(state);
            } else {
                // Util.WaitForKey($"\n{Game.RandErrorMsg()}\n");
                Util.WriteLine($"\n{Game.RandErrorMsg()}\n");
            }
        }
        public bool IsAvailable(State state) => isAvailable(state);

        public override string ToString() => Name;

    }
}
