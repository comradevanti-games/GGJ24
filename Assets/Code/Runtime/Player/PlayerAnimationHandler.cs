using UnityEngine;

namespace Dev.ComradeVanti.GGJ24.Player {

	public class PlayerAnimationHandler : MonoBehaviour {

#region Constants

		private static readonly int IsMoving = Animator.StringToHash("IsMoving");
		private static readonly int IsAirborne = Animator.StringToHash("IsAirborne");
		private static readonly int Slipped = Animator.StringToHash("Slipped");

#endregion

#region Fields

		[SerializeField] private Animator playerAnimator;
		[SerializeField] private Movement playerMovement;

#endregion

#region Methods

		private void Awake() {
			playerMovement.MovementStateChanged += OnPlayerMovementStateChanged;
		}

		private void OnPlayerMovementStateChanged(bool isMoving, Vector3 movementDirection) {

			if (movementDirection.x > 0) {
				transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
			}
			else if (movementDirection.x < 0) {
				transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
			}
			else {
				transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
			}

			playerAnimator.SetBool(IsMoving, isMoving);
		}

		public void SetPerformanceState(PerformanceState newState) {

			if (newState.IsInAir) {
				playerAnimator.SetBool(IsAirborne, true);
			}

		}

		private void OnDisable() {
			playerMovement.MovementStateChanged -= OnPlayerMovementStateChanged;
		}

#endregion

	}

}