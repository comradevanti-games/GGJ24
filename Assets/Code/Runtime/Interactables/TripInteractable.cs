using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class TripInteractable : MonoBehaviour, IPropInteractable
    {
        public PropInteraction TryInteraction(int currentSlotIndex, PerformanceState performanceState)
        {
            return new PropInteraction(
                performanceState with {IsTripped = true},
                ImmutableHashSet<HumorEffect>
                    .Empty
                    .Add(new HumorEffect(HumorTypes.Schadenfreude, 1)));
        }
    }
}