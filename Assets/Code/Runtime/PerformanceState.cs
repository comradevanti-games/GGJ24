namespace Dev.ComradeVanti.GGJ24
{
    public record PerformanceState(int TargetSlot)
    {
        public static readonly PerformanceState initial =
            new PerformanceState(0);
    }
}