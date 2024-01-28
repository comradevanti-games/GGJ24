#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PerformanceOrchestrator : MonoBehaviour, IPerformanceOrchestrator
    {
        public event Action? PerformanceCompleted;


        private bool isPaused;
        private Movement playerMover = null!;
        private ILiveStageKeeper liveStageKeeper = null!;
        private ILiveCrowdKeeper crowdKeeper = null!;
        private PlayerAnimationHandler playerAnimationHandler = null!;
        private CancellationTokenSource? performanceCancellationTokenSource;
        private bool isDoingProgressStoppingAnimation;


        private bool IsPerforming => performanceCancellationTokenSource != null;

        public bool IsDoingProgressStoppingAnimation
        {
            get => isDoingProgressStoppingAnimation;
            set
            {
                playerMover.enabled = !value;
                isDoingProgressStoppingAnimation = value;
            }
        }

        private bool ShouldProgressPerformance =>
            !isPaused && !IsDoingProgressStoppingAnimation;


        private PerformanceState ProgressPerformance(PerformanceState state)
        {
            // Stop tripping
            state = state with {IsTripped = false};

            var targetPlayerPosition =
                liveStageKeeper.TryGetPositionForSlot(state.TargetSlot);
            var playerHasReachedTarget = Vector3.Distance(
                targetPlayerPosition!.Value,
                playerMover.Position) < 0.05f;
            if (!playerHasReachedTarget) return state;

            // When reaching target we stop falling
            state = state with {IsInAir = false};

            if (state.TargetSlot >= Stage.SlotsPerStage - 1) return state;

            var prop = liveStageKeeper.TryGetLivePropAtSlot(state.TargetSlot);

            state = state with {TargetSlot = state.TargetSlot + 1};
            if (!prop) return state;

            var interactables = prop!.GetComponents<IPropInteractable>();
            var humorEffects = new HashSet<HumorEffect>();

            foreach (var interactable in interactables)
            {
                var currentPlayerSlot =
                    liveStageKeeper.TryGetSlotFor(playerMover.Position.x)!.Value;

                var interaction = interactable.TryInteraction(currentPlayerSlot, state);
                if (interaction == null) continue;

                state = interaction.NewPerformanceState;
                foreach (var effect in interaction.Effects) humorEffects.Add(effect);
            }

            crowdKeeper.Register(humorEffects);
            return state;
        }

        private void ApplyState(PerformanceState state)
        {
            var targetPlayerPosition =
                liveStageKeeper.TryGetPositionForSlot(state.TargetSlot);
            if (targetPlayerPosition == null)
                throw new Exception("Player tried to move to slot out of stage!");

            playerMover.To(targetPlayerPosition.Value);
            playerAnimationHandler.SetPerformanceState(state);
        }

        private void PrepareForPerformance()
        {
            var firstSlotPosition = liveStageKeeper.TryGetPositionForSlot(0);
            var initialPosition = firstSlotPosition! - Vector3.right * 10;
            playerMover.ToInstantaneous(initialPosition!.Value);
        }

        private async void Perform(CancellationToken ct)
        {
            PrepareForPerformance();

            var currentState = PerformanceState.initial;
            ApplyState(currentState);

            while (!ct.IsCancellationRequested)
            {
                if (ShouldProgressPerformance)
                {
                    var newState = ProgressPerformance(currentState);
                    if (newState != currentState)
                    {
                        currentState = newState;
                        ApplyState(currentState);
                    }

                    if (newState.TargetSlot == Stage.SlotsPerStage - 1)
                    {
                        PerformanceCompleted?.Invoke();
                        break;
                    }
                }

                await Task.Yield();
            }
        }

        private void StartPerformance()
        {
            performanceCancellationTokenSource = new CancellationTokenSource();
            var merged = CancellationTokenSource.CreateLinkedTokenSource(
                performanceCancellationTokenSource.Token,
                destroyCancellationToken);
            Perform(merged.Token);
        }

        private void StopPerformance()
        {
            performanceCancellationTokenSource?.Cancel();
            performanceCancellationTokenSource = null;
        }

        private void PausePerformance()
        {
            isPaused = true;
        }

        private void ResumePerformance()
        {
            isPaused = false;
        }

        private void OnPhaseChanged(PlayerPhase phase)
        {
            switch (phase)
            {
                case PlayerPhase.Performance:
                {
                    if (!IsPerforming)
                        StartPerformance();
                    if (isPaused)
                        ResumePerformance();
                    break;
                }
                case PlayerPhase.Menu:
                {
                    if (IsPerforming) PausePerformance();
                    break;
                }
                case PlayerPhase.Idle:
                {
                    if (IsPerforming)
                        StopPerformance();
                    break;
                }
            }
        }

        private void Awake()
        {
            playerMover = FindFirstObjectByType<Movement>()!;
            liveStageKeeper = Singletons.Require<ILiveStageKeeper>();
            playerAnimationHandler = FindFirstObjectByType<PlayerAnimationHandler>()!;
            crowdKeeper = Singletons.Require<ILiveCrowdKeeper>();
            Singletons.Require<IPhaseKeeper>().PhaseChanged += args =>
                OnPhaseChanged(args.NewPhase);
        }
    }
}