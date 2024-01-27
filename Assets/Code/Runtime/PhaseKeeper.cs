#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PhaseKeeper : MonoBehaviour, IPhaseKeeper
    {
        public event Action<IPhaseKeeper.PhaseChangedArgs>? PhaseChanged;


        private void EnterSetupMode()
        {
            PhaseChanged?.Invoke(
                new IPhaseKeeper.PhaseChangedArgs(PlayerPhase.Setup));
        }

        private void OnActChanged(IActKeeper.ActChangedArgs _)
        {
            EnterSetupMode();
        }

        private void Awake()
        {
            Singletons.Require<IActKeeper>().ActChanged += OnActChanged;
        }
    }
}