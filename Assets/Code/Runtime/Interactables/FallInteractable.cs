using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class FallInteractable : MonoBehaviour, IPropInteractable
    {
        public PropInteraction TryInteraction(int currentSlotIndex, PerformanceState performanceState) =>
            new PropInteraction(
                performanceState with {IsInAir = true, TargetSlot = currentSlotIndex + 1},
                ImmutableHashSet<HumorEffect>
                    .Empty
                    .Add(new HumorEffect(HumorTypes.Schadenfreude, 1)));
    }
}