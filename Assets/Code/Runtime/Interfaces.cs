﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public interface IPerformanceOrchestrator
    {
        public event Action? PerformanceCompleted;
    }

    /// <summary>
    /// Synonymous to "level". Contains information about the props and the
    /// crowd.
    /// </summary>
    public interface IAct
    {
        /// <summary>
        /// The state of the stage that is the default for this act.
        /// </summary>
        public Stage InitialStage { get; }

        /// <summary>
        /// The crowd for this act.
        /// </summary>
        public ICrowd Crowd { get; }
    }

    /// <summary>
    /// Contains data about a prop.
    /// </summary>
    public interface IProp
    {
        /// <summary>
        /// The props prefab.
        /// </summary>
        public GameObject Prefab { get; }

        /// <summary>
        /// Thumbnail for displaying the prop in the UI.
        /// </summary>
        public Sprite? Thumbnail { get; }

        /// <summary>
        /// Description of the prop. This may be multiline text.
        /// </summary>
        public string Description { get; }
    }

    /// <summary>
    /// A person that is visiting an act. Has opinions about what they like.
    /// </summary>
    public interface IPerson
    {
        /// <summary>
        /// Information about what type of humor this person likes.
        /// </summary>
        public HumorPreferences Preferences { get; }
    }

    /// <summary>
    /// Contains information about a crowd of people.
    /// </summary>
    public interface ICrowd
    {
        /// <summary>
        /// The people that are visiting the act.
        /// </summary>
        public ImmutableArray<IPerson> People { get; }
    }

    public interface IPhaseKeeper
    {
        public record PhaseChangedArgs(PlayerPhase NewPhase);


        public event Action<PhaseChangedArgs>? PhaseChanged;
    }

    /// <summary>
    /// Keeps track of the current stage. Only tracks the stage on a
    /// data level. Game-objects of props are held by <see cref="ILiveStageKeeper"/>.
    /// </summary>
    public interface IStageKeeper
    {
        public record StageChangedArgs(Stage NewStage);


        public event Action<StageChangedArgs>? StageChanged;


        public Stage Stage { get; set; }


        public bool CanPlaceAt(int slotIndex);
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

        public int? TryGetSlotFor(float x);

        public GameObject? TryGetLivePropAtSlot(int slotIndex);
    }

    /// <summary>
    /// Responsible for instantiating props. This is a singleton.
    /// </summary>
    public interface IPropBuilder
    {
        public GameObject BuildProp(IProp prop, int slotIndex);
    }

    /// <summary>
    /// Keeps information about what act the player is on.
    /// </summary>
    public interface IActKeeper
    {
        public record ActChangedArgs(IAct Act);


        public event Action<ActChangedArgs>? ActChanged;
    }

    /// <summary>
    /// Contains functions for querying and interacting with the crowd.
    /// </summary>
    public interface ILiveCrowdKeeper
    {
        public HahaScore CurrentHahaScore { get; }

        
        void Register(ISet<HumorEffect> humorEffects);
    }

    /// <summary>
    /// Contains functions for spawning people
    /// </summary>
    public interface IPersonSpawner
    {
        /// <summary>
        /// Spawns a person at a specific position. The person will
        /// have a random appearance. This does not initialize the person
        /// in any other way.
        /// </summary>
        public LivePerson SpawnPerson(Vector3 position);
    }

    /// <summary>
    /// Keeps track of the current stored and live inventory.
    /// The stored inventory is the inventory that the player assembles
    /// during the <see cref="PlayerPhase.PropSelection"/> phase.
    /// The live inventory is the inventory that the player uses during
    /// the <see cref="PlayerPhase.Setup"/> phase.
    /// </summary>
    public interface IInventoryKeeper
    {
        public record StoredInventoryChangedArgs(Inventory Inventory);

        public record LiveInventoryChangedArgs(Inventory Inventory, int? SelectedPropIndex);


        public event Action<StoredInventoryChangedArgs>? StoredInventoryChanged;
        public event Action<LiveInventoryChangedArgs>? LiveInventoryChanged;


        public Inventory StoredInventory { get; }

        public IProp? LiveSelectedProp { get; }


        /// <param name="updateF">An update functions. This function
        /// is provided the current state of the inventory as a parameter.
        /// The inventory that is returned from this function will
        /// be used as the new state.</param>
        public void ModifyStoredInventory(Func<Inventory, Inventory> updateF);

        public void TryUseSelectedProp();
    }

    public interface IPropInteractable
    {
        public PropInteraction? TryInteraction(int currentSlotIndex, PerformanceState performanceState);
    }
}