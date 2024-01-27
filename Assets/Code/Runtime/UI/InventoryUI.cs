using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24 {

	public class InventoryUI : MonoBehaviour {

#region Fields

		[SerializeField] private GameObject[] inventorySlots;

#endregion

#region Properties

		private IInventoryKeeper InventoryKeeper { get; set; }

#endregion

#region Methods

		private void Awake() {
			InventoryKeeper = Singletons.Require<IInventoryKeeper>();
			InventoryKeeper.StoredInventoryChanged += OnInventoryChanged;
		}

		private void OnInventoryChanged(IInventoryKeeper.InventoryChangedArgs e) {
			UpdateAvailableItems(e.Inventory.Props.ToList());
		}

		private void UpdateAvailableItems(List<IProp> inventoryProps) {

			for (int i = 0; i <= inventoryProps.Count; i++) {

				PropUI inventoryProp = inventorySlots[i].GetComponent<PropUI>();
				inventoryProp.PropData = inventoryProps[i];
				inventoryProp.PropSpriteRenderer.sprite = inventoryProps[i].Thumbnail;

			}

		}

#endregion

	}

}