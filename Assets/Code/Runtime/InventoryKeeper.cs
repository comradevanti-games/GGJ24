#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class InventoryKeeper : MonoBehaviour, IInventoryKeeper
    {
        public event Action<IInventoryKeeper.InventoryChangedArgs>? StoredInventoryChanged;
        public event Action<IInventoryKeeper.InventoryChangedArgs>? LiveInventoryChanged;


        public Inventory StoredInventory { get; private set; } = Inventory.Empty;

        public Inventory LiveInventory { get; private set; } = Inventory.Empty;


        public void ModifyStoredInventory(Func<Inventory, Inventory> updateF)
        {
            StoredInventory = updateF(StoredInventory);
        }

        private void ResetLiveInventory()
        {
            LiveInventory = StoredInventory;
            LiveInventoryChanged?.Invoke(
                new IInventoryKeeper.InventoryChangedArgs(LiveInventory));
        }

        private void Awake()
        {
            Singletons.Require<IPhaseKeeper>().PhaseChanged += args =>
            {
                if (args.NewPhase == PlayerPhase.Setup)
                    ResetLiveInventory();
            };
        }
    }
}