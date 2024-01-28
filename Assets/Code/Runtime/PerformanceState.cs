namespace Dev.ComradeVanti.GGJ24
{
    public record PerformanceState(int TargetSlot, bool IsInAir, bool IsTripped)
    {
        public static readonly PerformanceState initial =
            new PerformanceState(0, false, false);
    }
}