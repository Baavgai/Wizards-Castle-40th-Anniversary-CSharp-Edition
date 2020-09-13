using System.Collections.Generic;
using System.Linq;

namespace WizardCastle {
    internal static partial class Game {
        private static IEnumerable<string> ReplaceRandomMonster(IEnumerable<string> list) =>
            list.Select(x => x.Replace("//RandomMonster", Util.RandPick(Monster.AllMonsters).Name));

        private static readonly string[] errorMesssages = new string[] {
            "How very original, now try again.",
            "Even a //RandomMonster could do better than that.",
            "While you're messing around a //RandomMonster is going hungry.",
            "With skills like that, you'll be no challenge for a //RandomMonster.",
            "You'll need to be smarter than a //RandomMonster to find Zot's orb.",
            "You bumbling buffoon, that's not a choice, please read the question and answer correctly!",
            "Yawn, hurry up you buffoon.",
            "Please make a valid selection.",
            "Ha! Ha! I peed myself with laughter.",
            "You're wearing my patience thin.",
            "You really are a stupid one aren't you.",
            "Stop that now, it's really annoying.",
            "I guess following directions is hard for you.",
            "Have you always been this difficult?",
            "Chop chop! Let's get on with it.",
            "Would you please just pick one.",
            "Maybe next time you should play solitaire."
        };

        public static List<string> ErrorMesssages =>
            ReplaceRandomMonster(errorMesssages).ToList();
        public static string RandErrorMesssage() =>
            Util.RandPick(ErrorMesssages);

        private static readonly string[] gameMessages = {
            "You smell a //RandomMonster frying.",
            "You feel like you are being watched.",
            "You stepped on a frog.",
            "You stepped in //RandomMonster shit.",
            "You hear a //RandomMonster snoring.",
            "You get the strange feeling that you're playing The Wizard's Castle.",
            "You see messages written in //RandomMonster on the wall.",
            "You think you hear Zot laughing at you.",
            "You suddenly have the feeling of deja vu.",
            "You start to wonder if you will ever make it out of here.",
            "You hear your stomach growling and feel hungry.",
            "You have a bad feeling about this.",
            "You hear a //RandomMonster talking.",
            "You belched loudly.",
            "You farted loudly.",
            "You sneezed loudly.",
            "You yawned loudly.",
            "You coughed loudly."
        };

        public static List<string> GameMessages =>
            ReplaceRandomMonster(gameMessages).ToList();

        public static string RandGameMessages() =>
            Util.RandPick(GameMessages);



    }
}
