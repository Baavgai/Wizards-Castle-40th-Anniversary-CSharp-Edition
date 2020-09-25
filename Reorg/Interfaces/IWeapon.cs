namespace WizardCastle {
    interface IWeapon : IVendorItem {
        int BaseDamage { get; }

        int CalcDamage();
    }
}