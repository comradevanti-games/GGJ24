#nullable enable

using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24 {

	public class PlayerPropSetupHandler : MonoBehaviour {

#region Properties

		private IStageKeeper StageKeeper { get; set; }
		private ILiveStageKeeper LiveStageKeeper { get; set; }
		private IInventoryKeeper InventoryKeeper { get; set; }

#endregion

#region Methods

		private void Awake() {
			StageKeeper = Singletons.Require<IStageKeeper>();
			LiveStageKeeper = Singletons.Require<LiveStageKeeper>();
			InventoryKeeper = Singletons.Require<IInventoryKeeper>();
			FindAnyObjectByType<InputHandler>().SetupInteractionInputPerformed += OnSetupInteractionReceived;
		}

		private void OnSetupInteractionReceived(SetupInteractionEventArgs e) {

			int? currentPlayerSlotIndex = LiveStageKeeper.TryGetSlotFor(e.PlayerPosition.x);

			if (currentPlayerSlotIndex == null) {
				return;
			}

			Stage? newStage = Stage.TryPlaceProp(StageKeeper.Stage, currentPlayerSlotIndex.Value,
				InventoryKeeper.StoredInventory.Props.First());

			if (newStage != null) {
				StageKeeper.Stage = newStage;
			}

		}

#endregion

	}

}