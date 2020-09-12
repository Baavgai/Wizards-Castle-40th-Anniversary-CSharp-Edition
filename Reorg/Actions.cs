using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    internal static class Actions {
        public interface IAction : IEquatable<IAction>, IHasExec {
            public char Cmd { get; }
            public string Description { get; }
            // public void Exec(State state);
        }

        private class GameAction : IAction, IEquatable<GameAction> {
            private readonly Action<State> action;
            public GameAction(char cmd, string desc, Action<State> action) {
                Cmd = cmd;
                Description = desc;
                this.action = action;
            }
            public char Cmd { get; }
            public string Description { get; }

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


        public static readonly IAction North = new GameAction('N',
            @"(N)ORTH moves you to the room north of your present position. WHEN YOU GO NORTH FROM THE ENTRANCE THE GAME ENDS (In all other cases the north edge wraps to the south).",
            state => {
                if (state.CurrentCell == Items.Exit) {
                    state.Done = true;
                } else {
                    Direction.North.Exec(state);
                }
            });

        public static readonly IAction South = new GameAction('S',
            @"(S)OUTH moves you to the room south of your present position (In all cases the south edge wraps to the north edge).",
            Direction.South.Exec);

        public static readonly IAction East = new GameAction('E',
            @"(E)AST moves you to the room east of your present position (In all cases the east edge wraps to the west edge).",
            Direction.East.Exec);

        public static readonly IAction West = new GameAction('W',
            @"(W)EST moves you to the room west of your present position (In all cases the west edge wraps to the east edge).",
            Direction.West.Exec);

        public static readonly IAction Open = new GameAction('O',
            @"(O)PEN causes you to open the book or chest in the room you are in. This command will only work if you are in a room with a chest or book.",
            (state) => {
                if (state.CurrentCell.Contents is IHasOpen item) {
                    item.Open(state);
                } else {
                    Util.WaitForKey($"\n{Util.RandErrorMsg()}\n");
                }
            });
    }
}

/*
 * 
 *         public static Dictionary<char, string> availableActions = new Dictionary<char, string>
        {
            {'A', "Attack monster or vendor"},
            {'B', "Bribe monster or vendor"},
            {'D', "Down stairs"},
            {'E', "East"},
            {'F', "Light a flare"},
            {'G', "Gaze into crystal orb"},
            {'L', "Shine lamp into adjacent room"},
            {'M', "Show map"},
            {'N', "North" },
            {'O', "Open book or chest"},
            {'P', "Drink from pool"},
            {'Q', "Quit the game"},
            {'R', "Retreat from battle"},
            {'S', "South"},
            {'T', "Use the Runestaff to teleport"},
            {'U', "Up stairs"},
            {'V', "View Instructions"},
            {'W', "West"},
            {'Z', "Trade with Vendor" }
        };

             "•	(M)AP causes a map of the level you are currently on to be printed.",
            "•	(O)PEN causes you to open the book or chest in the room you are in. This command will only work if you are in a room with a chest or book.",
            "•	(P) POOL drink causes you to take a drink from a magic pool. You may repeat this command as often as you wish, but you must be in a room with a magic pool.",
            "•	(T)ELEPORT allows you to teleport directly to a room. This is the only way to enter the room containing the Orb of Zot. You must have the Runestaff to teleport.",
            "•	(U)P causes you to ascend stairs going up (you must be in a room with stairs going up).",
            "•	(D)OWN causes you to descend stairs going down (you must be in a room with stairs going down).",
            "•	(N)ORTH moves you to the room north of your present position. WHEN YOU GO NORTH FROM THE ENTRANCE THE GAME ENDS (In all other cases the north edge wraps to the south).",
            "•	(S)OUTH moves you to the room south of your present position (In all cases the south edge wraps to the north edge).",
            "•	(E)AST moves you to the room east of your present position (In all cases the east edge wraps to the west edge).",
            "•	(W)EST moves you to the room west of your present position (In all cases the west edge wraps to the east edge).",
            "•	(G)AZE causes you to gaze into a crystal orb and see things.",
            "	Note: When you see the location of the Orb of Zot, there is only a 50% chance that the location is correct.",
            "•	(F)LARE causes one of your flares to be lit, revealing the contents of all the rooms around your current position.",
            "•	(L)AMP will shine into any one of the rooms north, south, east, or west of your current position, revealing that room's contents.",
            "	Note: Your LAMP has limited uses and you must purchase more LAMP oil from a vendor to get more uses.",
            "•	(Q)UIT allows you to end the game while still in the castle. If you quit, you will lose the game.",
            "•	(A)TTACK monster or vendor.",
            "•	(R)ETREAT from battle.",
            "•	(B)RIBE a monster or a angry vendor.",

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

*/
