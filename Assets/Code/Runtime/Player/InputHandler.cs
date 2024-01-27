#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.ComradeVanti.GGJ24 {

	public class InputHandler : MonoBehaviour {

#region Events

		public event Action<InteractionInputEventArgs> SetupInteractionInputPerformed;
		public event Action SetupCompleteInputPerformed;

#endregion

#region Fields

		[SerializeField] private PlayerInput playerInput = null!;

#endregion

#region Properties

		private IPhaseKeeper PhaseKeeper { get; set; } = null!;

#endregion

#region Methods

		private void Awake() {
			PhaseKeeper = Singletons.Require<IPhaseKeeper>();
			PhaseKeeper.PhaseChanged += OnPhaseChanged;
		}

		private void OnPhaseChanged(IPhaseKeeper.PhaseChangedArgs e) {

			switch (e.NewPhase) {
				case PlayerPhase.Menu:
					playerInput.SwitchCurrentActionMap("Menu");
					break;
				case PlayerPhase.Idle:
					playerInput.SwitchCurrentActionMap("Idle");
					break;
				case PlayerPhase.Setup:
					playerInput.SwitchCurrentActionMap("Setup");
					break;
				case PlayerPhase.Performance:
					playerInput.SwitchCurrentActionMap("Performance");
					break;
				default:
					playerInput.SwitchCurrentActionMap("Idle");
					break;
			}

		}

		public void OnInteractionInputReceived(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {
				SetupInteractionInputPerformed?.Invoke(new InteractionInputEventArgs(transform.position));
			}

		}

		public void OnCompleteSetupInputReceived(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {
				SetupCompleteInputPerformed?.Invoke();
			}

		}

#endregion

	}

}