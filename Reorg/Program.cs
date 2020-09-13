using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    class Program {

        static int Main() {
            var state = Game.Startup();
            while(!state.Done) {
                state.Player.Turn += 1;
                var availActions = Actions.AllActions.Where(x => x.IsAvailable(state)).ToList();
                var playerAction = Util.Menu("Your action", availActions, (x, _) => x.Cmd).Item2;
                playerAction.Exec(state);
                // CheckCurses(ref player, ref knownMap);
                // CheckIfDead(player, ref fallThrough);
            }
            Game.GoodBye();
            return 0;
        }
    }
}
