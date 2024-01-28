using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PropBuilder : MonoBehaviour, IPropBuilder
    {
        private ILiveStageKeeper liveStageKeeper;


        public GameObject BuildProp(IProp prop, int slotIndex)
        {
            var position = liveStageKeeper.TryGetPositionForSlot(slotIndex);
            if (position == null) throw new Exception("Slot index out of range!");

            return Instantiate(prop.Prefab, position.Value, prop.Prefab.transform.rotation);
        }


        private void Awake()
        {
            liveStageKeeper = Singletons.Require<ILiveStageKeeper>();
        }
    }
}