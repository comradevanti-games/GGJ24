using System.Collections.Immutable;
using NUnit.Framework;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Contains information about what props the player brings into the act.
    /// </summary>
    public record Inventory(IImmutableList<IProp> Props)
    {
        public static readonly Inventory Empty =
            new Inventory(ImmutableList<IProp>.Empty);


        public static Inventory Add(Inventory inventory, IProp prop) =>
            inventory with {Props = inventory.Props.Add(prop)};

        public static Inventory Remove(Inventory inventory, IProp prop) =>
            inventory with {Props = inventory.Props.Remove(prop)};
    }
}