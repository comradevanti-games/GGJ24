#nullable enable

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PerformanceOrchestrator : MonoBehaviour
    {
        private bool isPaused;
        private Movement playerMover = null!;
        private ILiveStageKeeper liveStageKeeper = null!;
        private IPlayerStateKeeper playerStateKeeper = null!;
        private CancellationTokenSource? performanceCancellationTokenSource;

        private bool IsPerforming => performanceCancellationTokenSource != null;


        private PerformanceState ProgressPerformance(PerformanceState state)
        {
            var targetPlayerPosition =
                liveStageKeeper.TryGetPositionForSlot(state.TargetSlot);
            var playerHasReachedTarget = Vector3.Distance(
                targetPlayerPosition!.Value,
                playerMover.Position) < 0.05f;
            if (!playerHasReachedTarget) return state;

            if (state.TargetSlot >= Stage.SlotsPerStage - 1) return state;

            var prop = liveStageKeeper.TryGetLivePropAtSlot(state.TargetSlot);

            var nextTarget = state.TargetSlot + 1;
            if (!prop)
                return state with {TargetSlot = nextTarget};

            var interactables = prop!.GetComponents<IPropInteractable>();
            foreach (var interactable in interactables)
            {
                var interaction = interactable.TryInteraction(playerStateKeeper.PlayerState);
                if (interaction == null) continue;

                playerStateKeeper.PlayerState = interaction.NewPlayerState;
            }

            return state with {TargetSlot = nextTarget};
        }

        private void ApplyState(PerformanceState state)
        {
            var targetPlayerPosition =
                liveStageKeeper.TryGetPositionForSlot(state.TargetSlot);
            if (targetPlayerPosition == null)
                throw new Exception("Player tried to move to slot out of stage!");

            playerMover.To(targetPlayerPosition.Value);
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
                if (!isPaused)
                {
                    var newState = ProgressPerformance(currentState);
                    if (newState != currentState)
                    {
                        currentState = newState;
                        ApplyState(currentState);
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
            playerStateKeeper = Singletons.Require<IPlayerStateKeeper>();
            Singletons.Require<IPhaseKeeper>().PhaseChanged += args =>
                OnPhaseChanged(args.NewPhase);
        }
    }
}