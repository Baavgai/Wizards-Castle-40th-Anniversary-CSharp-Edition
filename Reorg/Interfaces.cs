using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WizardCastle {
    public interface IHasName {
        string Name { get; }
    }

    public interface IWorld {
        List<IInventoryItem> Items { get; }
    }

    public interface IHasOnEntry {
        void OnEntry(State state);
    }
    public interface IHasSymbol {
        char Symbol { get; }
    }

    public interface ICellContent : IHasName, IHasOnEntry, IHasSymbol { }

    public interface ICell {
        MapPos Location { get; }
        bool Known { get; set; }
        ICellContent Content { get; }
        void Clear();
    }
    public static class ExtICell {
        public static bool IsEmpty(this ICell cell) => cell.Content == null;
        public static void OnEntry(this ICell cell, State state) => cell.Content?.OnEntry(state);
    }

    public interface IVendorItem : IHasName {
        int Cost(bool vendor = true);
    }

    public interface IInventoryItem : IHasName {
        void OnFound(State state);
    }

    public interface IArmor : IVendorItem, IInventoryItem {
        int DamageAbsorb { get; }
    }

    public interface IWeapon : IVendorItem, IInventoryItem {
        int BaseDamage { get; }
        int CalcDamage();
    }

    public interface IHasOpen {
        void Open(State state);
    }

    public interface IBook : ICellContent, IHasOpen { }

    public interface IChest : ICellContent, IHasOpen { }
    public interface IOrb : ICellContent { void Gaze(State state); }
    public interface IPool : ICellContent { void Drink(State state); }
    public interface IZot : ICellContent, IInventoryItem { }
    public interface ICanAttack {
        void InitiateAttack(State state);
    }

    public interface IFactory<T> : IHasName {
        T Create();
    }

    public interface IHasExec {
        void Exec(State state);
    }

    public interface IHasInventory {
        List<IInventoryItem> Inventory { get; }
    }


    public interface IStackable : IInventoryItem {
        int Quantity { get; set; }
    }
}
