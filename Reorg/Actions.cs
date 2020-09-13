using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    static class Actions {
        public interface IAction : IHasExec, IHasName {
            public char Cmd { get; }
            public string Description { get; }
            public bool IsAvailable(State state);
        }

        private class GameAction : IAction {
            private readonly Action<State> action;
            private readonly Func<State, bool> isAvailable;

            public GameAction(char cmd, string name, string desc = null, Action<State> action = null, Func<State, bool> isAvailable = null) {
                Cmd = cmd;
                Name = name;
                Description = desc ?? name;
                if (action == null) {
                    this.action = s => { };
                } else {
                    this.action = action;
                }
                if (isAvailable == null) {
                    this.isAvailable = s => action != null;
                } else {
                    this.isAvailable = s => action != null && isAvailable(s);
                }
            }
            public char Cmd { get; }
            public string Description { get; }
            public string Name { get; }
            public void Exec(State state) {
                if (isAvailable(state)) {
                    action(state);
                } else {
                    Util.WaitForKey($"\n{Util.RandErrorMsg()}\n");
                }
            }
            public bool IsAvailable(State state) => isAvailable(state);

        }

        public static readonly IAction[] AllActions = new IAction[] {
            new GameAction('M', "Show map", "(M)AP causes a map of the level you are currently on to be printed.", state => {
                Game.DisplayLevel(state);
                Util.WaitForKey();
            }),

            new GameAction('O', "Open book or chest", @"(O)PEN causes you to open the book or chest in the room you are in. This command will only work if you are in a room with a chest or book.",
            (state) => {
                if (state.CurrentCell.Contents is IHasOpen item) {
                    item.Open(state);
                } else {
                    Util.WaitForKey($"\n{Util.RandErrorMsg()}\n");
                }
            }, s => s.CurrentCell.Contents is IHasOpen
            ),

            new GameAction('P', "Drink from pool", "(P) POOL drink causes you to take a drink from a magic pool. You may repeat this command as often as you wish, but you must be in a room with a magic pool."),

            new GameAction('T', "Use the Runestaff to teleport", "(T)ELEPORT allows you to teleport directly to a room. This is the only way to enter the room containing the Orb of Zot. You must have the Runestaff to teleport.",
                Teleport),

            new GameAction('U', "Up stairs", "(U)P causes you to ascend stairs going up (you must be in a room with stairs going up).",
                s => {
                    s.Player.Location += new MapPos(level: -1);
                },
                s => s.CurrentCell.Contents == Items.UpStairs && s.Player.Location.Level>0
                ),

            new GameAction('D', "Down stairs", "(D)OWN causes you to descend stairs going down (you must be in a room with stairs going down).",
                s => {
                    s.Player.Location += new MapPos(level: 1);
                },
                s => s.CurrentCell.Contents == Items.DownStairs && s.Player.Location.Level<s.Map.Levels-1
                ),

            new GameAction('N', "North", @"(N)ORTH moves you to the room north of your present position. WHEN YOU GO NORTH FROM THE ENTRANCE THE GAME ENDS (In all other cases the north edge wraps to the south).",
            state => {
                if (state.CurrentCell == Items.Exit) {
                    state.Done = true;
                } else {
                    Direction.North.Exec(state);
                }
            }),

            new GameAction('S', "South", @"(S)OUTH moves you to the room south of your present position (In all cases the south edge wraps to the north edge).",
            Direction.South.Exec),

            new GameAction('E', "East", @"(E)AST moves you to the room east of your present position (In all cases the east edge wraps to the west edge).",
            Direction.East.Exec),

            new GameAction('W', "West", @"(W)EST moves you to the room west of your present position (In all cases the west edge wraps to the east edge).",
            Direction.West.Exec),

            new GameAction('G', "Gaze into crystal orb", "(G)AZE causes you to gaze into a crystal orb and see things."),

            new GameAction('F', "Light a flare", "(F)LARE causes one of your flares to be lit, revealing the contents of all the rooms around your current position.",
                state => {
                    if (! state.Player.IsBlind) {
                        Game.RevealMapArea(state, state.Player.Location);
                        state.Player.flares -= 1;
                        Game.DisplayLevel(state);
                    } else {
                        Util.WriteLine("Lighting a flare won't do you any good since you are BLIND!");
                    }
                }),

            new GameAction('L', "Shine lamp into adjacent room", "(L)AMP will shine into any one of the rooms north, south, east, or west of your current position, revealing that room's contents."),

            new GameAction('Q', "Quit the game", "(Q)UIT allows you to end the game while still in the castle. If you quit, you will lose the game.",
                s => s.Done = true),


            new GameAction('A', "Attack monster or vendor", "(A)TTACK monster or vendor."),

            new GameAction('R', "Retreat from battle", "(R)ETREAT from battle."),

            new GameAction('B', "Bribe monster or vendor", "(B)RIBE a monster or a angry vendor."),


            new GameAction('V', "View Instructions", action: _ => Game.ViewInstructions(false)),
            new GameAction('Z', "Trade with Vendor")
        };

        private static void Teleport(State state) {
            var (player, _) = state;
            if (player.HasItem(Items.RuneStaff)) {
                MapPos location = null;
                while (location == null) {
                    Util.ClearScreen();
                    Console.Write("\nTeleport where (Example: For Level 3, Row 5, Column 2 type: 3,5,2): ");
                    location = MapPos.Parse(Console.ReadLine());
                    if (!state.Map.ValidPos(location)) {
                        Util.WaitForKey("* Invalid * Coordinates");
                        location = null;
                    }
                }
                player.Location = location;
                Util.WriteLine($"\n\tTeleporting to: {location}");
                Util.Sleep();
            }
        }
    }
}

/*
 * 


 *                 case "N":
                    if (player.location[0] == 0 && player.location[1] == 0 && player.location[2] == 3) {
                        Program.PlayerExit(player);
                        break;
                    } else {
                        player.North(knownMap);
                        break;
                    }
                case "S":
                    player.South(knownMap);
                    break;
                case "E":
                    player.East(knownMap);
                    break;
                case "W":
                    player.West(knownMap);
                    break;

        private class GameAction : IAction, IEquatable<GameAction> {
            private readonly Action<State> action;
            
            public GameAction(char cmd, string name, string desc, Action<State> action) {
                Cmd = cmd;
                Name = name;
                Description = desc;
                this.action = action;
            }
            public char Cmd { get; }
            public string Description { get; }
            public string Name { get; }

            public void Exec(State state) => action(state);
            public bool Equals(IAction other) => other != null && other.Cmd == Cmd;
            public bool Equals(GameAction other) => other != null && other.Cmd == Cmd;

            public static bool operator ==(GameAction a, GameAction b) =>
                (a == null && b == null) || ((a == null || b == null) ? false : a.Equals(b));

            public static bool operator !=(GameAction a, GameAction b) => !(a == b);

            public override bool Equals(object obj) => this.Equals(obj as IAction);
            // public override string ToString() => $"({Level},{Row},{Col})";

            public override int GetHashCode() => Cmd.GetHashCode();
        }
        public static readonly IAction North = new GameAction('N', "North",
            @"(N)ORTH moves you to the room north of your present position. WHEN YOU GO NORTH FROM THE ENTRANCE THE GAME ENDS (In all other cases the north edge wraps to the south).",
            state => {
                if (state.CurrentCell == Items.Exit) {
                    state.Done = true;
                } else {
                    Direction.North.Exec(state);
                }
            });

        public static readonly IAction South = new GameAction('S', "South",
            @"(S)OUTH moves you to the room south of your present position (In all cases the south edge wraps to the north edge).",
            Direction.South.Exec);

        public static readonly IAction East = new GameAction('E', "East",
            @"(E)AST moves you to the room east of your present position (In all cases the east edge wraps to the west edge).",
            Direction.East.Exec);

        public static readonly IAction West = new GameAction('W', "West",
            @"(W)EST moves you to the room west of your present position (In all cases the west edge wraps to the east edge).",
            Direction.West.Exec);

        public static readonly IAction Open = new GameAction('O', "Open book or chest",
            @"(O)PEN causes you to open the book or chest in the room you are in. This command will only work if you are in a room with a chest or book.",
            (state) => {
                if (state.CurrentCell.Contents is IHasOpen item) {
                    item.Open(state);
                } else {
                    Util.WaitForKey($"\n{Util.RandErrorMsg()}\n");
                }
            });
    }

*/
