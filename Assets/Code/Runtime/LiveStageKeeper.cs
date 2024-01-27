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

        public Vector3? TryGetPositionForSlot(int slotIndex) =>
            firstSlotTransform.position + stageDirection * UnitsPerSlot;
    }
}