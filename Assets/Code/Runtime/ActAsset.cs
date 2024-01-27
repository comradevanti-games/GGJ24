#nullable enable

using System;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    [CreateAssetMenu(menuName = "GGJ24/Act", fileName = "New Act")]
    public class ActAsset : ScriptableObject, IAct
    {
        [SerializeField] private PropAsset?[] initialProps =
            new PropAsset?[Stage.SlotsPerStage];


        public Stage InitialStage => new Stage(
            initialProps.OfType<IProp?>().ToImmutableArray());


        private void OnValidate()
        {
            if (initialProps.Length == Stage.SlotsPerStage) return;

            Debug.LogWarning($"Act must define exactly {Stage.SlotsPerStage} props!");
            Array.Resize(ref initialProps, Stage.SlotsPerStage);
        }
    }
}