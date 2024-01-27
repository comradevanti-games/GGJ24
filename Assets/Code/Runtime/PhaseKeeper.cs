#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PhaseKeeper : MonoBehaviour, IPhaseKeeper
    {
        public event Action<IPhaseKeeper.PhaseChangedArgs>? PhaseChanged;


        private void EnterPropSelection()
        {
            PhaseChanged?.Invoke(
                new IPhaseKeeper.PhaseChangedArgs(PlayerPhase.PropSelection));
        }

        private void OnActChanged(IActKeeper.ActChangedArgs _)
        {
            EnterPropSelection();
        }

        private void Awake()
        {
            Singletons.Require<IActKeeper>().ActChanged += OnActChanged;
        }
    }
}