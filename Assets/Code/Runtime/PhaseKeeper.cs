#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PhaseKeeper : MonoBehaviour, IPhaseKeeper
    {
        public event Action<IPhaseKeeper.PhaseChangedArgs>? PhaseChanged;


        private PlayerPhase? previousPhase = null;
        private PlayerPhase currentPhase = PlayerPhase.Menu;


        private static bool CanDoSwitch(PlayerPhase from, PlayerPhase to)
        {
            return (from, to) switch
            {
                (PlayerPhase.Menu, _) => true,
                (_, PlayerPhase.Menu) => true,
                (PlayerPhase.PropSelection, PlayerPhase.Setup) => true,
                (PlayerPhase.Setup, PlayerPhase.Performance) => true,
                (PlayerPhase.Performance, PlayerPhase.Setup) => true,
                _ => false
            };
        }

        public void TrySwitchPhase(PlayerPhase nextPhase)
        {
            if (!CanDoSwitch(currentPhase, nextPhase)) return;

            if (nextPhase == PlayerPhase.Setup) {
                if (Singletons.Require<IInventoryKeeper>().StoredInventory.Props.Count == 0) return;
            }

            previousPhase = currentPhase;
            currentPhase = nextPhase;
            PhaseChanged?.Invoke(new IPhaseKeeper.PhaseChangedArgs(currentPhase));
        }

        public void TrySwitchPhase(string nextPhaseName)
        {
            if (Enum.TryParse<PlayerPhase>(nextPhaseName, out var nextPhase))
                TrySwitchPhase(nextPhase);
            else Debug.LogError($"Unknown phase-name: {nextPhaseName}");
        }

        public void Unpause()
        {
            if (currentPhase != PlayerPhase.Menu) return;

            // Un-pausing switches to previous phase.
            // When the game was just started there is no previous phase.
            // In that case we go to prop-selection to start the game.
            TrySwitchPhase(previousPhase ?? PlayerPhase.PropSelection);
        }

        private void OnActChanged(IActKeeper.ActChangedArgs _)
        {
            TrySwitchPhase(PlayerPhase.PropSelection);
        }

        private void Start()
        {
            PhaseChanged?.Invoke(new IPhaseKeeper.PhaseChangedArgs(currentPhase));
        }

        private void Awake()
        {
            Singletons.Require<IActKeeper>().ActChanged += OnActChanged;

            var inputHandler = FindAnyObjectByType<InputHandler>()!;
            inputHandler.PauseInputPerformed +=
                () => TrySwitchPhase(PlayerPhase.Menu);
            inputHandler.SetupCompleteInputPerformed +=
                () => TrySwitchPhase(PlayerPhase.Performance);
            inputHandler.PropSelectionCompleteInputPerformed +=
                () => TrySwitchPhase(PlayerPhase.Setup);
        }
    }
}