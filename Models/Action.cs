using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using YWMenuNS;

namespace The_Wizard_s_Castle.Models {
    class Action : IEquatable<Action> {

        protected Action(string name) => Name = name;
        public override string ToString() => Name;
        public bool Equals(Item other) => other != null && other.Name == Name && other.ItemType == ItemType;

        public char Cmd { get; }
        public string Description { get; }

        public void Exec(State state) {

        }
    }
}

/*
 *             "•	(N)ORTH moves you to the room north of your present position. WHEN YOU GO NORTH FROM THE ENTRANCE THE GAME ENDS (In all other cases the north edge wraps to the south).",
            "•	(S)OUTH moves you to the room south of your present position (In all cases the south edge wraps to the north edge).",
            "•	(E)AST moves you to the room east of your present position (In all cases the east edge wraps to the west edge).",
            "•	(W)EST moves you to the room west of your present position (In all cases the west edge wraps to the east edge).",

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
