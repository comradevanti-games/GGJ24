#nullable enable

using System;
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
    }
}