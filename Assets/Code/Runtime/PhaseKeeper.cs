#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PhaseKeeper : MonoBehaviour, IPhaseKeeper
    {
        public event Action<IPhaseKeeper.PhaseChangedArgs>? PhaseChanged;
    }
}