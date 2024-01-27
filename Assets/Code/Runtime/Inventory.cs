using System.Collections.Immutable;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// Contains information about what props the player brings into the act.
    /// </summary>
    public record Inventory(IImmutableList<IProp> Props)
    {
        public static readonly Inventory Empty =
            new Inventory(ImmutableList<IProp>.Empty);
    }
}