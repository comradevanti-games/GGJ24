using System.Collections.Immutable;

namespace Dev.ComradeVanti.GGJ24
{
    public record HumorEffect(
        HumorTypes Type,
        float Strength);

    public record PropInteraction(
        PlayerState NewPlayerState,
        IImmutableSet<HumorEffect> Effects);
}