using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class DamageInteractable : MonoBehaviour, IPropInteractable
    {
        public PropInteraction TryInteraction(int currentSlotIndex, PerformanceState performanceState)
        {
            return new PropInteraction(
                performanceState, ImmutableHashSet<HumorEffect>.Empty);
        }
    }
}