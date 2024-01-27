#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class StageKeeper : MonoBehaviour, IStageKeeper
    {
        public event Action<IStageKeeper.StageChangedArgs>? StageChanged;


        public Stage Stage => Stage.Empty;
    }
}