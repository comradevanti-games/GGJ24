namespace Dev.ComradeVanti.GGJ24
{
    public record PerformanceState(int TargetSlot, bool IsInAir)
    {
        public static readonly PerformanceState initial =
            new PerformanceState(0, false);
    }
}