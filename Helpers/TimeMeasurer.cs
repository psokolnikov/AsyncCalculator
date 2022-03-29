using System.Diagnostics;

namespace AsyncCalculator.Helpers;

public static class TimeMeasurer
{
    public static long GetTimeInMilliseconds(Action action)
    {
        var timer = Stopwatch.StartNew();
        action();
        timer.Stop();
        return (long)timer.Elapsed.TotalMilliseconds;
    }

    public static long GetTimeInMilliseconds(DateTime start, DateTime finish)
    {
        return (long)(finish - start).TotalMilliseconds;
    }
}