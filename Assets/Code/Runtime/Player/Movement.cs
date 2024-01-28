using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dev.ComradeVanti.GGJ24.Player
{
    public class Movement : MonoBehaviour
    {
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

        public bool IsMoving
        {
            get => isMoving;
            set
            {
                isMoving = value;
                MovementStateChanged?.Invoke(IsMoving);
            }
        }

        public bool IsAutomated { get; set; }

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

            while (Vector3.Distance(targetPoint, transform.position) > 0.5f)
            {
                Vector3 dir = (targetPoint - transform.position).normalized;
                charController.Move(dir * (movementSpeed * Time.fixedDeltaTime));
                yield return new WaitForFixedUpdate();
            }

            IsAutomated = false;
        }

        #endregion
    }
}