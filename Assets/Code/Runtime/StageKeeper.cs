#nullable enable

using System;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class StageKeeper : MonoBehaviour, IStageKeeper
    {
        public event Action<IStageKeeper.StageChangedArgs>? StageChanged;

        private Stage initialStage = null!;
        private Stage currentStage = Stage.Empty;


        public Stage Stage
        {
            get => currentStage;
            set
            {
                currentStage = value;
                StageChanged?.Invoke(new IStageKeeper.StageChangedArgs(Stage));
            }
        }

        
        public bool CanPlaceAt(int slotIndex) =>
            Stage.Props.ElementAtOrDefault(slotIndex) == null;
        
        private void SwitchStage(Stage stage)
        {
            initialStage = stage;
            Stage = stage;
        }

        private void OnActChanged(IActKeeper.ActChangedArgs args)
        {
            SwitchStage(args.Act.InitialStage);
        }

        private void Awake()
        {
            Singletons.Require<IActKeeper>().ActChanged += OnActChanged;
        }
    }
}