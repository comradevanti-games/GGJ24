using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;
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

		private async void Awake() {
			AllProps = (await PropIO.LoadAllAsync()).ToList();
			PhaseKeeper = Singletons.Require<PhaseKeeper>();
			PhaseKeeper.PhaseChanged += OnPhaseChanged;
		}

		private void OnPhaseChanged(IPhaseKeeper.PhaseChangedArgs e) {

			if (e.NewPhase == PlayerPhase.PropSelection) {
				DisplayProps();
			}

		}

		private void DisplayProps() {

			foreach (IProp prop in AllProps) { }

		}

#endregion

	}

}