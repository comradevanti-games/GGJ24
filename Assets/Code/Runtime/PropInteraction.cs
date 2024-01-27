using System.Collections.Immutable;

namespace Dev.ComradeVanti.GGJ24
{
    public record HumorEffect(
        HumorTypes Type,
        HahaScore Score);

    public record PropInteraction(
        PlayerState NewPlayerState,
        IImmutableSet<HumorEffect> Effects);
}