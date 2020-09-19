using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WizardCastle {
    class SimplGameAction : IHasExec, IHasName {
        private readonly Action<State> action;

        public SimplGameAction(char cmd, string name, Action<State> action) {
            Cmd = cmd;
            Name = name;
            this.action = action;
        }
        public SimplGameAction(string name, Action<State> action) : this(name[0], name, action) { }
        public char Cmd { get; }
        public string Description { get; }
        public string Name { get; }
        public void Exec(State state) => action(state);
        public override string ToString() => Name;

    }
}
