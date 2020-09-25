namespace WizardCastle {
    public interface IBook : IHasOpen {
        void Open(State state);
    }
}