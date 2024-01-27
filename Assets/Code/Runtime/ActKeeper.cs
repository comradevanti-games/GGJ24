#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class ActKeeper : MonoBehaviour, IActKeeper
    {
        public event Action<IActKeeper.ActChangedArgs>? ActChanged;
    }
}