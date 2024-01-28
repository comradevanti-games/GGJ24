using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class RakeInteractable : MonoBehaviour, IPropInteractable
    {
        public PropInteraction TryInteraction(int currentSlotIndex, PerformanceState performanceState) => new PropInteraction(
            performanceState with {IsTripped = true, TargetSlot = currentSlotIndex + 1},
            ImmutableHashSet<HumorEffect>
                .Empty
                .Add(new HumorEffect(HumorTypes.Schadenfreude, 1)));
    }
}