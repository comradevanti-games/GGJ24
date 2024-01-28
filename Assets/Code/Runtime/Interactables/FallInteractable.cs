using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class FallInteractable : MonoBehaviour, IPropInteractable
    {
        public PropInteraction TryInteraction(int currentSlotIndex, PerformanceState performanceState)
        {
            return new PropInteraction(
                performanceState with {IsInAir = true, TargetSlot = currentSlotIndex + 1},
                ImmutableHashSet<HumorEffect>.Empty);
        }
    }
}