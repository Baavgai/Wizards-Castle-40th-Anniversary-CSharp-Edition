using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    class GameAction : IHasExec, IHasName {
        private readonly static List<GameAction> all = new List<GameAction>();
        public static GameAction[] All => all.ToArray();
        private static GameAction Create(char cmd, string name, string desc = null, Action<State> action = null, Func<State, bool> isAvailable = null) =>
            all.Register(new GameAction(cmd, name, desc, action, isAvailable));

        public static readonly GameAction Map = Create('M',
            "Show map", "(M)AP causes a map of the level you are currently on to be printed.",
            state => {
                Game.DisplayLevel(state);
                // Util.WaitForKey();
            });

        public static readonly GameAction Open = Create('O', "Open book or chest", @"(O)PEN causes you to open the book or chest in the room you are in. This command will only work if you are in a room with a chest or book.",
        state => {
            if (state.CurrentCell.Contents is IHasOpen item) {
                item.Open(state);
            } else {
                Util.WriteLine($"\n{Game.RandErrorMsg()}\n");
            }
        }, s => s.CurrentCell.Contents is IHasOpen
            );

        public static readonly GameAction PoolDrink = Create('P', "Drink from pool", "(P) POOL drink causes you to take a drink from a magic pool. You may repeat this command as often as you wish, but you must be in a room with a magic pool."
            );

        public static readonly GameAction Teleport = Create('T', "Use the Runestaff to teleport", "(T)ELEPORT allows you to teleport directly to a room. This is the only way to enter the room containing the Orb of Zot. You must have the Runestaff to teleport.",
            state => {
                var (player, _) = state;
                if (player.HasItem(Treasure.RuneStaff)) {
                    MapPos location = null;
                    while (location == null) {
                        // Util.ClearScreen();
                        Util.Write("\nTeleport where (Example: For Level 3, Row 5, Column 2 type: 3,5,2): ");
                        location = MapPos.Parse(Util.ReadLine());
                        if (!state.Map.ValidPos(location)) {
                            Util.WriteLine("* Invalid * Coordinates");
                            location = null;
                        }
                    }
                    player.Location = location;
                    Util.WriteLine($"\n\tTeleporting to: {location}");
                    Util.Sleep();
                }
            },
            s => s.Player.HasItem(Treasure.RuneStaff));

        public static readonly GameAction Up = Create('U', "Up stairs", "(U)P causes you to ascend stairs going up (you must be in a room with stairs going up).",
                s => {
                    s.Player.Location += new MapPos(level: -1);
                },
                s => s.CurrentCell.Contents == Content.UpStairs && s.Player.Location.Level > 0
                );

        public static readonly GameAction Down = Create('D', "Down stairs", "(D)OWN causes you to descend stairs going down (you must be in a room with stairs going down).",
                s => {
                    s.Player.Location += new MapPos(level: 1);
                },
                s => s.CurrentCell.Contents == Content.DownStairs && s.Player.Location.Level < s.Map.Levels - 1
                );

        public static readonly GameAction North = Create('N', "North", @"(N)ORTH moves you to the room north of your present position. WHEN YOU GO NORTH FROM THE ENTRANCE THE GAME ENDS (In all other cases the north edge wraps to the south).",
            state => {
                if (state.CurrentCell == Content.Exit) {
                    state.Done = true;
                } else {
                    Direction.North.Exec(state);
                }
            });

        public static readonly GameAction South = Create('S', "South", @"(S)OUTH moves you to the room south of your present position (In all cases the south edge wraps to the north edge).",
            Direction.South.Exec);

        public static readonly GameAction East = Create('E', "East", @"(E)AST moves you to the room east of your present position (In all cases the east edge wraps to the west edge).",
            Direction.East.Exec);

        public static readonly GameAction West = Create('W', "West", @"(W)EST moves you to the room west of your present position (In all cases the west edge wraps to the east edge).",
            Direction.West.Exec);

        public static readonly GameAction Gaze = Create('G', "Gaze into crystal orb", "(G)AZE causes you to gaze into a crystal orb and see things."
            );

        public static readonly GameAction Flare = Create('F', "Light a flare", "(F)LARE causes one of your flares to be lit, revealing the contents of all the rooms around your current position.",
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

        public static readonly GameAction Lamp = Create('L', "Shine lamp into adjacent room", "(L)AMP will shine into any one of the rooms north, south, east, or west of your current position, revealing that room's contents."
            );

        public static readonly GameAction Quit = Create('Q', "Quit the game", "(Q)UIT allows you to end the game while still in the castle. If you quit, you will lose the game.",
                s => s.Done = true);


        public static readonly GameAction Attack = Create('A', "Attack monster or vendor", "(A)TTACK monster or vendor."
            );

        public static readonly GameAction Retreat = Create('R', "Retreat from battle", "(R)ETREAT from battle."
            );

        public static readonly GameAction Bribe = Create('B', "Bribe monster or vendor", "(B)RIBE a monster or a angry vendor."
            );


        public static readonly GameAction ViewInstructions = Create('V', "View Instructions",
            action: _ => Game.ShowInstructions());

        public static readonly GameAction Trade = Create('Z', "Trade with Vendor"
            );




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
                // Util.WaitForKey($"\n{Game.RandErrorMsg()}\n");
                Util.WriteLine($"\n{Game.RandErrorMsg()}\n");
            }
        }
        public bool IsAvailable(State state) => isAvailable(state);

        public override string ToString() => Name;

    }
}
