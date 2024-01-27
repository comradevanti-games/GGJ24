#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class LiveStageKeeper : MonoBehaviour, ILiveStageKeeper
    {
        /// <summary>
        /// How many units/meters are in one slot.
        /// </summary>
        private const float UnitsPerSlot = 1;

        /// <summary>
        /// The direction in which the slots are laid out on the stage.
        /// </summary>
        private readonly Vector3 stageDirection = Vector3.right;


        [SerializeField] private Transform firstSlotTransform;

        private IPropBuilder propBuilder = null!;
        private GameObject?[] liveProps = new GameObject?[Stage.SlotsPerStage];


        public Vector3? TryGetPositionForSlot(int slotIndex) =>
            firstSlotTransform.position + stageDirection * UnitsPerSlot;


        private void ClearSlot(int slotIndex)
        {
            var liveProp = liveProps[slotIndex];
            if (liveProp != null) Destroy(liveProp);
        }

        private void BuildSlot(int slotIndex, IProp prop)
        {
            var liveProp = propBuilder.BuildProp(prop, slotIndex);
            liveProps[slotIndex] = liveProp;
        }

        private void BuildStage(Stage stage)
        {
            for (var slotIndex = 0; slotIndex < Stage.SlotsPerStage; slotIndex++)
            {
                ClearSlot(slotIndex);
                var prop = stage.Props[slotIndex];
                if (prop == null) continue;

                BuildSlot(slotIndex, prop);
            }
        }

        private void OnStageChanged(IStageKeeper.StageChangedArgs args)
        {
            BuildStage(args.NewStage);
        }

        private void Awake()
        {
            propBuilder = Singletons.Require<IPropBuilder>();
            Singletons.Require<IStageKeeper>().StageChanged += OnStageChanged;
        }
    }
}