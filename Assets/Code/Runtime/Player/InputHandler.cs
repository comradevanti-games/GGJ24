#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.ComradeVanti.GGJ24 {

	public class InputHandler : MonoBehaviour {

#region Events

		public event Action<SetupInteractionEventArgs>? SetupInteractionInputPerformed;
		public event Action? PropSelectionInputPerformed;
		public event Action<Vector2>? PropChoosingInputPerformed;
		public event Action? SetupCompleteInputPerformed;
		public event Action? PerformanceStartInputPerformed;
		public event Action? PauseInputPerformed;

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
				case PlayerPhase.PropSelection:
					playerInput.SwitchCurrentActionMap("PropSelection");
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

		public void OnPropSelectionInputReceived(InputAction.CallbackContext ctx) {
			if (ctx.canceled) {
				PropSelectionInputPerformed?.Invoke();
			}
		}

		public void OnPropChoosingInputReceived(InputAction.CallbackContext ctx) {
			if (ctx.canceled) {
				PropChoosingInputPerformed?.Invoke(ctx.ReadValue<Vector2>());
			}
		}

		public void OnSetupInteractionInputReceived(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {
				SetupInteractionInputPerformed?.Invoke(new SetupInteractionEventArgs(transform.position));
			}

		}

		public void OnCompleteSetupInputReceived(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {
				SetupCompleteInputPerformed?.Invoke();
			}

		}

		public void OnPerformanceStartInputReceived(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {
				PerformanceStartInputPerformed?.Invoke();
			}

		}

		public void OnPauseInputReceived(InputAction.CallbackContext ctx) {

			if (ctx.canceled) {
				PauseInputPerformed?.Invoke();
			}

		}

#endregion

	}

}