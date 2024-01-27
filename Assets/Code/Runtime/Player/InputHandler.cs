using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.ComradeVanti.GGJ24 {

	public class InputHandler : MonoBehaviour {

#region Fields

		[SerializeField] private PlayerInput playerInput;

#endregion

#region Properties

		private IPhaseKeeper PhaseKeeper { get; set; }

#endregion

#region Methods

		private void Awake() {
			PhaseKeeper = Singletons.TryFind<IPhaseKeeper>();
			PhaseKeeper.PhaseChanged += OnPhaseChanged;
		}

		private void OnPhaseChanged(IPhaseKeeper.PhaseChangedArgs e) {

			switch (e.NewPhase) {
				case PlayerPhase.Idle:
					break;
				case PlayerPhase.Setup:
					break;
				case PlayerPhase.Performance:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

		}

#endregion

	}

}