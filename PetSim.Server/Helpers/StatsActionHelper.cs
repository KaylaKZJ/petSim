public static class StatsActionHelper
{
    public static int AddStat(int current, int change)
    {
        return current <= 0 ? 0 : current + change;
    }

    public static double AddStat(double current, double change)
    {
        return current <= 0.0 ? 0.0 : current + change;
    }
}
