using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.ComradeVanti.GGJ24.Player {

	public class Movement : MonoBehaviour {

#region Events

		public event Action<bool> MovementStateChanged;

#endregion

#region Fields

		[SerializeField] private CharacterController charController;
		[SerializeField] private float movementSpeed;

		private bool isMoving;

#endregion

#region Properties

		public Vector3 MovementDirection { get; private set; }

		public bool IsMoving {
			get => isMoving;
			set {
				isMoving = value;
				MovementStateChanged?.Invoke(IsMoving);
			}
		}

#endregion

#region Methods

		public void Update() {

			if (IsMoving) {
				charController.Move(MovementDirection * (movementSpeed * Time.fixedDeltaTime));
			}

		}

		public void OnDirectionalInputReceived(InputAction.CallbackContext ctx) {

			if (ctx.performed) {
				MovementDirection = new Vector3(ctx.ReadValue<float>(), 0, 0);
			}

			if (ctx.canceled) {
				MovementDirection = Vector3.zero;
			}

			IsMoving = MovementDirection != Vector3.zero;

		}

#endregion

	}

}