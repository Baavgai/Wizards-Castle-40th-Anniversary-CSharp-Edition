using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace WizardCastle {
    class Program {


        private static void GameLoop(State state) {
            while (!state.Done) {
                state.Turn += 1;
                Game.ShowStatus(state);
                Game.DisplayLevel(state);
                state.CurrentCell.Contents?.OnEntry(state);
                state.Player.LastAction = Util.Menu("Your action",
                    GameAction.All.Where(x => x.IsAvailable(state)),
                    (x, _) => x.Cmd).Item2;
                state.Player.LastAction.Exec(state);

                // var availActions = GameAction.All.Where(x => x.IsAvailable(state)).ToList();
                // var playerAction = Util.Menu("Your action", availActions, (x, _) => x.Cmd).Item2;
                // playerAction.Exec(state);
                // CheckCurses(ref player, ref knownMap);
                // CheckIfDead(player, ref fallThrough);
            }
        }

        static int Main() {
            GameLoop(Game.Startup());
            // GameLoop(Game.CreateTestState());
            Game.GoodBye();
            return 0;
        }
    }
}
