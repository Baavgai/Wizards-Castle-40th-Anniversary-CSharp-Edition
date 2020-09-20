using System.Collections.Generic;

namespace WizardCastle {
    interface IMob : IAbilitiesMutable, IContent, IHasInventory {
        bool IsDead { get; }
        bool Mad { get; }
        int WebbedTurns { get; set; }
        void InitiateAttack(State state);
    }
}