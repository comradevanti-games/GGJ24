#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class StageKeeper : MonoBehaviour, IStageKeeper
    {
        public event Action<IStageKeeper.StageChangedArgs>? StageChanged;


        public Stage Stage { get; private set; } = Stage.Empty;


        private void SwitchChange(Stage stage)
        {
            Stage = stage;
            StageChanged?.Invoke(new IStageKeeper.StageChangedArgs(Stage));
        }

        private void OnActChanged(IActKeeper.ActChangedArgs args)
        {
            SwitchChange(args.Act.InitialStage);
        }

        private void Awake()
        {
            Singletons.Require<IActKeeper>().ActChanged += OnActChanged;
        }
    }
}