using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WizardCastle {
    public interface ICanAttack {
        void InitiateAttack(State state);
    }

}
