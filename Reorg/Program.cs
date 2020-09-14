using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace WizardCastle {
    class Program {

        private static State InitTestState() {
            var ab = new Abilities() + Race.Elf;
            return new State(new Map(false), new Player() {
                Race = Race.Elf.Name,
                Sex = "Female",
                Dexterity = ab.Dexterity, Intelligence = ab.Intelligence, Strength = ab.Strength
            });
        }

        private static void GameLoop(State state) {
            while (!state.Done) {
                state.Player.Turn += 1;
                var availActions = Actions.AllActions.Where(x => x.IsAvailable(state)).ToList();
                var playerAction = Util.Menu("Your action", availActions, (x, _) => x.Cmd).Item2;
                playerAction.Exec(state);
                // CheckCurses(ref player, ref knownMap);
                // CheckIfDead(player, ref fallThrough);
            }
        }

        static int Main() {
            var state = InitTestState();
            Actions.AllActions.First(x => x.Cmd == 'F').Exec(state);
            Game.DisplayLevel(state);
            // var state = Game.Startup();
            // Game.GoodBye();
            return 0;
        }
    }
}

/*
         interface IFoo {
            public int Num { get; }
        }
        class Foo : IFoo {
            public int Num { get; set; }
        }
        interface IBar {
            public int Num { get; }
        }
        class Bar : IBar {
            public int Num { get; set; }
        }
        class Baz : IBar {
            public int Num { get; set; }
        }

            {
                var x = new Foo() { Num = 2 };
                Debug.WriteLine($"foo {x is IFoo} {x is Foo} {x is IBar} {x is Bar} {x is Baz}");
            }
            {
                var x = new Bar() { Num = 3 };
                Debug.WriteLine($"foo {x is IFoo} {x is Foo} {x is IBar} {x is Bar} {x is Baz}");
            }
            {
                var x = new Baz() { Num = 3 };
                Debug.WriteLine($"foo {x is IFoo} {x is Foo} {x is IBar} {x is Bar} {x is Baz}");
            }
 
 */
