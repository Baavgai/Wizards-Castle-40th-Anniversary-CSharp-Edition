using System;
using System.Collections.Generic;
using System.Linq;

namespace The_Wizard_s_Castle
{
    class Monster : Character
    {
        public bool mad = true;
        public int webbedTurns = 0;
        public bool runeStaff = false;
        public Monster (string race, int dexterity, int intelligence, int strength)
        {
            this.race = race;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
            this.strength = strength;
        }
        public static Monster GetOrCreateMonster(string[,,] theMap, Player player, string locationStr)
        {
            Random rand = new Random();
            if (! GameCollections.monstersDict.ContainsKey(locationStr))
            {
                Monster monster = new Monster(theMap[player.location[0], player.location[1], player.location[2]], rand.Next(1, player.maxAttrib + 1), rand.Next(1, player.maxAttrib + 1), rand.Next(1, player.maxAttrib + 1));
                monster.location[0] = player.location[0];
                monster.location[1] = player.location[1];
                monster.location[2] = player.location[2];
                if (monster.race == "Goblin" || monster.race == "Kobold" || monster.race == "Wolf") { monster.strength += 1; }
                else if (monster.race == "Bear") { monster.strength += 2; }
                else if (monster.race == "Gargoyle" || monster.race == "Orc" || monster.race == "Troll") { monster.strength += 3; }
                else if (monster.race == "Chimera" || monster.race == "Minotaur" || monster.race == "Ogre") { monster.strength += 4; }
                else if (monster.race == "Balrog") { monster.strength += 5; } 
                else if (monster.race == "Dragon") { monster.strength += 8; }
                if (monster.location.SequenceEqual(GameCollections.runestaffLocation))
                {
                    monster.runeStaff = true;
                }
                GameCollections.monstersDict.Add(locationStr, monster);
            }
            return GameCollections.monstersDict[locationStr];
        }
        public static string MonsterMadMessage(Monster monster)
        {
            List<string> messageList = new List<string>
            {
                $"The {monster.race} sees you, snarls and lunges towards you!",
                $"The {monster.race} looks angrily at you moves in your direction!",
                $"The {monster.race} stops what it's doing and focuses its attention on you!",
                $"The {monster.race} looks at you agitatedly!",
                $"The {monster.race} says, you've come seeking treasure and instead have found death!",
                $"The {monster.race} growls and prepares for battle!",
                $"The {monster.race} says, you will be a small meal for a me!",
                $"The {monster.race} says, welcome to your death pitiful!"
            };
            return messageList[new Random().Next(0, messageList.Count)];
        }
    }
}
