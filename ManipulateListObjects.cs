using System;
using System.Collections.Generic;

namespace The_Wizard_s_Castle
{
    class ManipulateListObjects
    {
        public static List<string> ReplaceRandomMonster(List<string> list)
        {
            Random rand = new Random();
            List<string> newList = new List<string>();
            foreach (string item in list)
            {
                newList.Add(item.Replace("//RandomMonster", GameCollections.Monsters[rand.Next(0, GameCollections.Monsters.Count)]));
            }
            return newList;
        }
    }
}
