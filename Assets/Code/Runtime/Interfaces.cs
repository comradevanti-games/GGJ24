using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Contains data about a prop.
    /// </summary>
    public interface IProp
    {
        /// <summary>
        /// The props prefab.
        /// </summary>
        public GameObject Prefab { get; }
    }

    public interface IPhaseKeeper
    {
        public record PhaseChangedArgs();


        public event Action<PhaseChangedArgs> PhaseChanged;
    }

    /// <summary>
    /// Keeps track of the current stage. Only tracks the stage on a
    /// data level. Game-objects of props are held by <see cref="ILiveStageKeeper"/>.
    /// </summary>
    public interface IStageKeeper
    {
        public record StageChangedArgs(Stage NewStage);


        public event Action<StageChangedArgs> StageChanged;


        public Stage Stage { get; }
    }

    /// <summary>
    /// Manages the live (game-object) representation of the stage and props.
    /// </summary>
    public interface ILiveStageKeeper
    {
        /// <summary>
        /// Attempts to find the world position of a slot index. Null if
        /// the slot index is out of bounds.
        /// </summary>
        public Vector3? TryGetPositionForSlot(int slotIndex);
    }

    /// <summary>
    /// Responsible for instantiating props. This is a singleton.
    /// </summary>
    public interface IPropBuilder
    {
        public GameObject BuildProp(IProp prop, int slotIndex);
    }
}