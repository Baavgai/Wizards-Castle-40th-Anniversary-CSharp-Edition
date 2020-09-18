using System;
using System.Collections.Generic;

namespace WizardCastle {
    class AbilityItem : Item {
        private readonly static List<AbilityItem> all = new List<AbilityItem>();
        public static AbilityItem[] All => all.ToArray();
        public static readonly AbilityItem Dexterity = all.Register(new AbilityItem("Dexterity", 
            (attr, amt) => attr.Dexterity += amt,
            attr => attr.Dexterity
            ));
        public static readonly AbilityItem Intelligence = all.Register(new AbilityItem("Intelligence", 
            (attr, amt) => attr.Intelligence += amt,
            attr => attr.Dexterity
            ));
        public static readonly AbilityItem Strength = all.Register(new AbilityItem("Strength", 
            (attr, amt) => attr.Strength += amt,
            attr => attr.Dexterity
            ));

        private readonly Action<IAbilitiesMutable, int> applyAmount;
        private readonly Func<IAbilitiesMutable, int> getValue;

        private AbilityItem(string name, Action<IAbilitiesMutable, int> applyAmount, Func<IAbilitiesMutable, int> getValue) : base(name) {
            this.applyAmount = applyAmount;
            this.getValue = getValue;
        }
        public int Value(IAbilitiesMutable entity) => getValue(entity);

        public int ApplyAmount(IAbilitiesMutable entity, int amount) {
            applyAmount(entity, amount);
            return getValue(entity);
        }

    }
}
