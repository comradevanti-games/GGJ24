#nullable enable

using System;
using System.Linq;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class InventoryKeeper : MonoBehaviour, IInventoryKeeper
    {
        public event Action<IInventoryKeeper.StoredInventoryChangedArgs>? StoredInventoryChanged;
        public event Action<IInventoryKeeper.LiveInventoryChangedArgs>? LiveInventoryChanged;


        private int? liveSelectedPropIndex = null;


        public Inventory StoredInventory { get; private set; } = Inventory.Empty;

        public IProp? LiveSelectedProp =>
            liveSelectedPropIndex == null
                ? null
                : LiveInventory.Props.ElementAtOrDefault(liveSelectedPropIndex.Value);

        private Inventory LiveInventory { get; set; } = Inventory.Empty;

        private void SendLiveInventoryChangeEvent()
        {
            LiveInventoryChanged?.Invoke(
                new IInventoryKeeper.LiveInventoryChangedArgs(
                    LiveInventory, liveSelectedPropIndex));
        }

        private void ResetLiveInventory()
        {
            LiveInventory = StoredInventory;
            SendLiveInventoryChangeEvent();
        }

        public void ModifyStoredInventory(Func<Inventory, Inventory> updateF)
        {
            StoredInventory = updateF(StoredInventory);
            StoredInventoryChanged?.Invoke(
                new IInventoryKeeper.StoredInventoryChangedArgs(StoredInventory));

            ResetLiveInventory();
        }

        public void TryUseSelectedProp()
        {
            var selectedProp = LiveSelectedProp;
            if (selectedProp == null) return;

            LiveInventory = Inventory.Remove(LiveInventory, selectedProp);
            TryChangeSelectedInventoryItem(-1);
            SendLiveInventoryChangeEvent();
        }

        private void TryChangeSelectedInventoryItem(int changeDirection)
        {
            if (changeDirection == 0) return;
            if (liveSelectedPropIndex == null) return;
            if (LiveInventory.Props.Count == 0) return;
            
            liveSelectedPropIndex = (int) Mathf.Repeat(
                liveSelectedPropIndex.Value - changeDirection, LiveInventory.Props.Count);
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
            inputHandler.SetupInventoryChoosingInputPerformed += direction =>
            {
                TryChangeSelectedInventoryItem(direction);
                SendLiveInventoryChangeEvent();
            };
        }
    }
}