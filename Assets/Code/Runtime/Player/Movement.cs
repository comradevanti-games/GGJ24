using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.ComradeVanti.GGJ24.Player
{
    public class Movement : MonoBehaviour
    {
        #region Events

        public event Action<bool, Vector3> MovementStateChanged;
        public event Action<bool> AutoMovementStateChanged;

        #endregion

        #region Fields

        [SerializeField] private CharacterController charController;
        [SerializeField] private float movementSpeed;

        private bool isMoving;
        private bool isAutomated;

#endregion

        #region Properties

        public Vector3 MovementDirection { get; private set; }

        public bool IsMoving
        {
            get => isMoving;
            set
            {
                isMoving = value;
                MovementStateChanged?.Invoke(IsMoving, MovementDirection);
            }
        }

        public bool IsAutomated {
            get => isAutomated;
            set {
                isAutomated = value;
                AutoMovementStateChanged?.Invoke(IsAutomated);
            }
        }

        public Vector3 Position => charController.transform.position;

        #endregion

        #region Methods

        private void Awake()
        {
            Singletons.Require<IPhaseKeeper>().PhaseChanged += OnPhaseChanged;
        }

        public void FixedUpdate()
        {
            if (IsMoving)
            {
                charController.Move(MovementDirection * (movementSpeed * Time.fixedDeltaTime));
            }
        }

        private void OnPhaseChanged(IPhaseKeeper.PhaseChangedArgs e)
        {
            if (e.NewPhase == PlayerPhase.PropSelection)
            {
                StartCoroutine(MoveCharacterTo(new Vector3(0, transform.position.y, transform.position.z)));
            }
        }

        public void OnDirectionalInputReceived(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                MovementDirection = new Vector3(ctx.ReadValue<float>(), 0, 0);
            }

            if (ctx.canceled)
            {
                MovementDirection = Vector3.zero;
            }

            IsMoving = MovementDirection != Vector3.zero;
        }

        public void To(Vector3 targetPoint)
        {
            StopAllCoroutines();
            StartCoroutine(MoveCharacterTo(targetPoint));
        }

        public void ToInstantaneous(Vector3 targetPosition)
        {
            charController.enabled = false;
            charController.transform.position = targetPosition;
            charController.enabled = true;
        }

        private IEnumerator MoveCharacterTo(Vector3 targetPoint)
        {
            IsAutomated = true;
            transform.LookAt(targetPoint);

            while (Vector3.Distance(targetPoint, Position) > float.Epsilon)
            {
                var nextPosition = Vector3.MoveTowards(
                    Position, targetPoint,
                    movementSpeed * Time.fixedDeltaTime);
                var delta = nextPosition - Position;
                charController.Move(delta);

                yield return new WaitForFixedUpdate();
            }

            IsAutomated = false;
        }

        #endregion
    }
}