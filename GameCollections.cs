using System.Collections.Generic;

namespace The_Wizard_s_Castle
{
    class Race
    {
        public string RaceName { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }
    }
    class GameCollections
    {
        public static int ExitCode = 999;
        public static int[] runestaffLocation;
        public static Dictionary<string, Monster> monstersDict = new Dictionary<string, Monster>();
        public static Dictionary<string, Vendor> vendorsDict = new Dictionary<string, Vendor>();
        public static List<string> Weapons = new List<string>
        {
            "Dagger",
            "Mace",
            "Sword"
        };
        public static List<string> Armor = new List<string>
        {
            "Leather",
            "ChainMail",
            "Plate"
        };
        public static List<string> Abilities = new List<string>
        {
            "Dexterity",
            "Intelligence",
            "Strength"
        };
        public static List<string> Curses = new List<string>
        {
            "Forgetfulness",
            "Leech",
            "Lethargy"
        };
        public static List<string> Monsters = new List<string>
        {
            "Balrog",
            "Bear",
            "Chimera",
            "Dragon",
            "Gargoyle",
            "Goblin",
            "Kobold",
            "Minotaur",
            "Ogre",
            "Orc",
            "Troll",
            "Wolf"
        };
        public static List<string> Treasures = new List<string>
        {
            "The Blue Flame",
            "The Green Gem",
            "The Norn Stone",
            "The Opal Eye",
            "The Palantir",
            "The Pale Pearl",
            "The Ruby Red",
            "The Silmaril"
        };
        public static List<string> RoomContents = new List<string>
        {
            "Book",
            "Chest",
            "DownStairs",
            "UpStairs",
            "Flares",
            "Gold",
            "Orb",
            "Pool",
            "SinkHole",
            "Vendor",
            "Warp",
            "x",
            "Entrance/Exit"
        };
        public static List<Race> Races = new List<Race>
        {
            new Race { RaceName = "Dwarf", Dexterity = 6, Intelligence = 8, Strength = 10},
            new Race { RaceName = "Elf", Dexterity = 10, Intelligence = 8, Strength = 6},
            new Race { RaceName = "Hobbit", Dexterity = 12, Intelligence = 8, Strength = 4},
            new Race { RaceName = "Homo-Sapien", Dexterity = 8, Intelligence = 8, Strength = 8}
        };
        public static Dictionary<char, string> availableActions = new Dictionary<char, string>
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
        public static List<string> ErrorMesssages = new List<string>
        {
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
        public static List<string> GameMessages = new List<string>
        {
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
        public static List<string> Directions = new List<string>
        {
            "North",
            "South",
            "East",
            "West"
        };
    }
}
