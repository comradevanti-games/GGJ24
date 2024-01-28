#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PerformanceOrchestrator : MonoBehaviour
    {
        private bool isPaused;
        private CancellationTokenSource? performanceCancellationTokenSource = null;


        private bool IsPerforming => performanceCancellationTokenSource != null;


        private void ProgressPerformance()
        {
            throw new NotImplementedException();
        }

        private async void Perform(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (!isPaused) ProgressPerformance();
                await Task.Yield();
            }
        }

        private void StartPerformance()
        {
            performanceCancellationTokenSource = new CancellationTokenSource();
            Perform(performanceCancellationTokenSource.Token);
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
            Singletons.Require<IPhaseKeeper>().PhaseChanged += args =>
                OnPhaseChanged(args.NewPhase);
        }
    }
}