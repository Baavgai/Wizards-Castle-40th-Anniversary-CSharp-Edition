namespace WizardCastle {
    public interface IVendorItem : IHasName {
        int Cost(bool vendor = true);
    }
}