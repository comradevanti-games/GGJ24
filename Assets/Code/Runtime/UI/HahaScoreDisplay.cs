#nullable enable

using System;
using Dev.ComradeVanti.GGJ24.Player;
using TMPro;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class HahaScoreDisplay : MonoBehaviour
    {
        private TextMeshProUGUI displayText = null!;


        private void Awake()
        {
            displayText = GetComponentInChildren<TextMeshProUGUI>();
            Singletons.Require<IPhaseKeeper>().PhaseChanged += args =>
            {
                var isVisible = args.NewPhase == PlayerPhase.Eval;
                gameObject.SetActive(isVisible);

                var score = Singletons.Require<ILiveCrowdKeeper>().CurrentHahaScore;
                displayText.text = score.ToString();
            };
        }
    }
}