using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WizardCastle {
    public interface IView {
        int Width { get; }
        int Height { get; }

        string ErrorMessage();

        IView WriteIndent();
        IView Write(string s = "");
        IView WriteNewLine();
        IView WriteLine(string s = "");

        IView SetBgColor(ConsoleColor color);
        IView SetColor(ConsoleColor color);
        IView ResetColors();

        Task<string> ReadLine(string prompt = "");
        Task<char> ReadChar(string prompt = "");
        void Clear();
    }
}


