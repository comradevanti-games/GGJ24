﻿#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class CurtainManager : MonoBehaviour
    {
        [SerializeField] private Transform leftCurtainTransform = null!;
        [SerializeField] private Transform rightCurtainTransform = null!;
        [SerializeField] private float closeTime;
        [SerializeField] private float closedScale;
        [SerializeField] private float openScale;
        [SerializeField] private float initialClosedness;

        private float currentClosedness;
        private float targetClosedness;


        private Vector3 CurtainScale
        {
            get => leftCurtainTransform.localScale;
            set
            {
                leftCurtainTransform.localScale = value;
                rightCurtainTransform.localScale = value;
            }
        }


        private void UpdateCurtainTransforms()
        {
            var yScale = Mathf.Lerp(openScale, closedScale, currentClosedness);
            var oldScale = CurtainScale;
            var newScale = new Vector3(oldScale.x, yScale, oldScale.z);

            CurtainScale = newScale;
        }

        private void Update()
        {
            var delta = 1 / closeTime * Time.deltaTime;
            currentClosedness =
                Mathf.MoveTowards(currentClosedness, targetClosedness, delta);
            UpdateCurtainTransforms();
        }

        private void OnPhaseChanged(PlayerPhase phase)
        {
            var shouldCurtainsBeClosed =
                phase is PlayerPhase.Menu or PlayerPhase.Idle or PlayerPhase.Eval;
            targetClosedness = shouldCurtainsBeClosed ? 1 : 0;
        }

        private void Start()
        {
            currentClosedness = initialClosedness;
            targetClosedness = initialClosedness;
        }

        private void Awake()
        {
            Singletons.Require<IPhaseKeeper>()
                      .PhaseChanged += args => OnPhaseChanged(args.NewPhase);
        }
    }
}