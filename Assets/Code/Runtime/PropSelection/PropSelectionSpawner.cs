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

#endregion

#region Properties

		private List<IProp> AllProps { get; set; }
		private IPhaseKeeper PhaseKeeper { get; set; }
		private int HoveredPropID { get; set; }

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
				DisplayProps();
			}

		}

		private void DisplayProps() {

			for (int i = 0; i <= AllProps.Count; i++) {

				Instantiate(propSelectionPrefab,
					new Vector3((propSelectionOrigin.x + (1 * i)), propSelectionOrigin.y, propSelectionOrigin.z), Quaternion.identity);

			}

		}

		private void OnPropChoosingInputPerformed(Vector2 choosingDirection) {
			Debug.Log(choosingDirection);
		}

		private void OnPropSelectionInputPerformed() {
			Debug.Log("Props Selection Confirmed");
		}

#endregion

	}

}