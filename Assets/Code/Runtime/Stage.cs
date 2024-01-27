#nullable enable

using System;
using System.Collections.Immutable;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Includes information about the currently placed props.
    /// </summary>
    /// <param name="PlacedProps">The props placed on this stage. The
    /// length of this array should always be <see cref="SlotsPerStage"/>.
    /// </param>
    public record Stage(ImmutableArray<PlacedProp?> PlacedProps)
    {
        /// <summary>
        /// The fixed amount of slots per stage.
        /// </summary>
        public const int SlotsPerStage = 20;


        public static readonly Stage Empty = new Stage(
            new PlacedProp?[SlotsPerStage].ToImmutableArray());


        private static bool IsValidSlotIndex(int slotIndex) =>
            slotIndex is >= 0 and < SlotsPerStage;

        public static Stage? TryPlaceProp(Stage stage, int slotIndex, IProp prop)
        {
            if (!IsValidSlotIndex(slotIndex)) return null;

            var placedProp = new PlacedProp(prop);
            return stage with
            {
                PlacedProps = stage.PlacedProps.SetItem(slotIndex, placedProp)
            };
        }

        public static Stage? TryClearProp(Stage stage, int slotIndex)
        {
            if (!IsValidSlotIndex(slotIndex)) return null;

            return stage with
            {
                PlacedProps = stage.PlacedProps.SetItem(slotIndex, null)
            };
        }

        public static Stage? TryUpdateProp(Stage stage, int slotIndex, Func<PlacedProp, PlacedProp> updateF)
        {
            if (!IsValidSlotIndex(slotIndex)) return null;

            var prop = stage.PlacedProps[slotIndex];
            if (prop == null) return stage;

            return stage with
            {
                PlacedProps = stage.PlacedProps.SetItem(slotIndex, updateF(prop))
            };
        }
    }
}