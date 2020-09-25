using System;
using System.Collections.Generic;

namespace WizardCastle {
    public class Gender : Item {
        public static readonly Gender Male = new Gender("Male");
        public static readonly Gender Female = new Gender("Female");
        // public static readonly Gender Other = new Gender("Other");

        public static Gender[] All = new Gender[] { Male, Female };
        private Gender(string name) : base(name) { }

    }
}
