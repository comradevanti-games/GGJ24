#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class InventoryKeeper : MonoBehaviour, IInventoryKeeper
    {
        public event Action<IInventoryKeeper.StoredInventoryChangedArgs>? StoredInventoryChanged;
        public event Action<IInventoryKeeper.LiveInventoryChangedArgs>? LiveInventoryChanged;


        private int? liveSelectedPropIndex = null;
        private Inventory liveInventory = Inventory.Empty;


        public Inventory StoredInventory { get; private set; } = Inventory.Empty;

        private Inventory LiveInventory
        {
            get => liveInventory;
            set
            {
                liveInventory = value;
                LiveInventoryChanged?.Invoke(
                    new IInventoryKeeper.LiveInventoryChangedArgs(
                        LiveInventory, liveSelectedPropIndex));
            }
        }

        private void ResetLiveInventory()
        {
            LiveInventory = StoredInventory;
        }

        public void ModifyStoredInventory(Func<Inventory, Inventory> updateF)
        {
            StoredInventory = updateF(StoredInventory);
            StoredInventoryChanged?.Invoke(
                new IInventoryKeeper.StoredInventoryChangedArgs(StoredInventory));

            ResetLiveInventory();
        }


        private void Awake()
        {
            Singletons.Require<IPhaseKeeper>().PhaseChanged += args =>
            {
                if (args.NewPhase == PlayerPhase.Setup)
                {
                    liveSelectedPropIndex = 0;
                    ResetLiveInventory();
                }
                else
                    liveSelectedPropIndex = null;
            };

            var inputHandler = FindFirstObjectByType<InputHandler>()!;
        }
    }
}