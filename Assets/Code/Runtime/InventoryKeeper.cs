#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class InventoryKeeper : MonoBehaviour, IInventoryKeeper
    {
        public event Action<IInventoryKeeper.InventoryChangedArgs>? InventoryChanged;


        public Inventory Inventory { get; private set; } = Inventory.Empty;


        public void ModifyInventory(Func<Inventory, Inventory> updateF)
        {
            Inventory = updateF(Inventory);
        }
    }
}