using System.Collections.Generic;

namespace WizardCastle {
    public interface IMob : IAbilitiesMutable, IContent, IHasInventory {
        bool IsDead { get; }
        bool Mad { get; }
        int WebbedTurns { get; set; }
        void InitiateAttack(State state);
    }
}