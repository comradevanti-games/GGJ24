using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class RakeInteractable : MonoBehaviour, IPropInteractable
    {
        public PropInteraction TryInteraction(int currentSlotIndex, PerformanceState performanceState)
        {
            transform.localEulerAngles = new Vector3(90, 270, 0);
            return new PropInteraction(
                performanceState, ImmutableHashSet<HumorEffect>.Empty);
        }
    }
}