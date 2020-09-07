using System.Collections.Generic;

namespace The_Wizard_s_Castle
{
    class Character
    {
        public int maxAttrib = 18;
        public int dexterity = 0;
        public int intelligence = 0;
        public int strength = 0;
        public int gold = 60;
        public string race = "";
        public List<string> treasures = new List<string>();
        public int[] location = { 0, 0, 3 };

        public void IncDexterity (int change)
        {
            this.dexterity += change;
            if (this.dexterity > this.maxAttrib) {
                this.dexterity = this.maxAttrib;
            }
        }

        public void IncIntelligence (int change)
        {
            this.intelligence += change;
            if (this.intelligence > this.maxAttrib) {
                this.intelligence = this.maxAttrib;
            }
        }

        public void IncStrength (int change)
        {
            this.strength += change;
            if (this.strength > this.maxAttrib) {
                this.strength = this.maxAttrib;
            }
        }

        public void DecDexterity(int change)
        {
            this.dexterity -= change;
        }

        public void DecIntelligence(int change)
        {
            this.intelligence -= change;
        }

        public void DecStrength(int change)
        {
            this.strength -= change;
        }
    }
}
