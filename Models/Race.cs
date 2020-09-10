using System.Collections.Generic;

namespace The_Wizard_s_Castle {
    class Race : Abilities {
        public string RaceName { get; set; }
        public override string ToString() => RaceName;
    }
}
