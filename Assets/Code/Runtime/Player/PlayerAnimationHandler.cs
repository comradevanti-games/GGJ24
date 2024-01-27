using UnityEngine;

namespace Dev.ComradeVanti.GGJ24.Player {

	public class PlayerAnimationHandler : MonoBehaviour {

#region Constants

		private static readonly int IsMoving = Animator.StringToHash("IsMoving");

#endregion

#region Fields

		[SerializeField] private Animator playerAnimator;
		[SerializeField] private Movement playerMovement;

#endregion

#region Methods

		private void Awake() {
			playerMovement.MovementStateChanged += OnPlayerMovementStateChanged;
		}

		private void OnPlayerMovementStateChanged(bool isMoving) {
			playerAnimator.SetBool(IsMoving, isMoving);
		}

		private void OnDisable() {
			playerMovement.MovementStateChanged -= OnPlayerMovementStateChanged;
		}

#endregion

	}

}