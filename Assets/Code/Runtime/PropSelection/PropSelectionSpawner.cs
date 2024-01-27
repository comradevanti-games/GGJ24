using System.Collections.Generic;
using System.Linq;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Dev.ComradeVanti.GGJ24.PropSelection {

	public class PropSelectionSpawner : MonoBehaviour {

#region Fields

		[SerializeField] private Vector3 propSelectionOrigin = Vector3.zero;
		[SerializeField] private GameObject propSelectionPrefab;
		[SerializeField] private GameObject selectionArrow;
		[SerializeField] private float arrowOffset = 5f;

		private int hoveredPropID;

#endregion

#region Properties

		private List<IProp> AllProps { get; set; }
		private IPhaseKeeper PhaseKeeper { get; set; }
		private int HoveredPropID {
			get => hoveredPropID;
			set {
				hoveredPropID = value;
				SetPropSelection(HoveredPropID);
			}
		}
		private List<Vector3> PropPositions { get; set; }

#endregion

#region Methods

		private void Awake() {
			AllProps = PropIO.LoadAll().ToList();
			PhaseKeeper = Singletons.Require<PhaseKeeper>();
			PhaseKeeper.PhaseChanged += OnPhaseChanged;
			FindAnyObjectByType<InputHandler>().PropChoosingInputPerformed += OnPropChoosingInputPerformed;
			FindAnyObjectByType<InputHandler>().PropSelectionInputPerformed += OnPropSelectionInputPerformed;
		}

		private void OnPhaseChanged(IPhaseKeeper.PhaseChangedArgs e) {

			if (e.NewPhase == PlayerPhase.PropSelection) {
				gameObject.SetActive(true);
				DisplayProps();
			}
			else {
				gameObject.SetActive(false);
			}

		}

		private void DisplayProps() {

			PropPositions = new List<Vector3>();

			for (int i = 0; i <= AllProps.Count - 1; i++) {

				var newProp = Instantiate(propSelectionPrefab,
					new Vector3((propSelectionOrigin.x + (8.5f * i)), propSelectionOrigin.y, propSelectionOrigin.z), Quaternion.identity,
					transform);

				newProp.GetComponent<SpriteRenderer>().sprite = AllProps[i].Thumbnail;
				PropPositions.Add(newProp.transform.position);

			}

			HoveredPropID = 0;
			selectionArrow.gameObject.SetActive(true);

		}

		public void SetPropSelection(int selectionId) {

			selectionArrow.transform.position = new Vector3(PropPositions[selectionId].x, PropPositions[selectionId].y + arrowOffset,
				PropPositions[selectionId].z);

		}

		private void OnPropChoosingInputPerformed(Vector2 choosingDirection) {

			if (choosingDirection.x > 0) {

				if (AllProps.Count - 1 == HoveredPropID) {
					return;
				}

				HoveredPropID++;
			}

			if (choosingDirection.x < 0) {

				if (HoveredPropID == 0) {
					return;
				}

				HoveredPropID--;

			}

		}

		private void OnPropSelectionInputPerformed() {
			IInventoryKeeper inventoryKeeper = Singletons.Require<IInventoryKeeper>();
			inventoryKeeper.ModifyStoredInventory(inventory => Inventory.Add(inventory, AllProps[HoveredPropID]));
		}

#endregion

	}

}