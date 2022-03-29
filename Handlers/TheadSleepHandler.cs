using AsyncCalculator.Events;
using Disruptor;

namespace AsyncCalculator.Handlers;

public class TheadSleepHandler : IWorkHandler<CalculatorEvent[]>
{
    public void OnEvent(CalculatorEvent[] evt)
    {
        Thread.Sleep(2000);
    }
}