using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WizardCastle {
    public class State : IView {
        public Player Player { get; }
        public Map Map { get; }
        private IView View { get; }
        public int Turn { get; set; } = 1;
        public bool Done { get; set; } = false;

        public State(IView view, Map map, Player player) {
            View = view;
            Map = map;
            Player = player;

        }

        public ICell CurrentCell => Map[Player.Location];

        public int Width => View.Width;

        public int Height => View.Height;

        public void Deconstruct(out Player player, out Map map) {
            player = Player;
            map = Map;
        }

        public IView WriteIndent() => View.WriteIndent();
        public IView Write(string s = "") => View.Write(s);
        public IView WriteNewLine() => View.WriteNewLine();
        public IView WriteLine(string s = "") => View.WriteLine(s);
        public IView SetBgColor(ConsoleColor color) => View.SetBgColor(color);
        public IView SetColor(ConsoleColor color) => View.SetColor(color);
        public IView ResetColors() => View.ResetColors();
        public Task<string> ReadLine(string prompt = "") => View.ReadLine(prompt);
        public Task<char> ReadChar(string prompt = "") => View.ReadChar(prompt);
        public void Clear() => View.Clear();
        public string ErrorMessage() => View.ErrorMessage();
    }

}
