using AsyncCalculator.Events;
using AsyncCalculator.Handlers;
using Disruptor;
using Disruptor.Dsl;

namespace AsyncCalculator;

public class AsyncCalculator
{
    private const int MaxCommandsCount = 3;
    private const int RingBufferSize = 16;

    private RingBuffer<CalculatorEvent[]>? ringBuffer;

    public void Start()
    {
        ringBuffer = StartDisruptor();
    }

    public void Execute(string[] arguments)
    {
        if (ringBuffer == null)
            throw new InvalidOperationException("AsyncCalculator.Start() must be called");

        if (arguments.Length > MaxCommandsCount)
        {
            Console.Out.WriteLineAsync($"Can't process more than {MaxCommandsCount} arguments");
            return;
        }

        if (!arguments.Any())
            return;

        var sequenceNo = ringBuffer.Next();
        var targetEvents = ringBuffer[sequenceNo];

        ApplicationController.CalculatorEventsParser!.TryParse(arguments, targetEvents);

        ringBuffer.Publish(sequenceNo);
    }

    private RingBuffer<CalculatorEvent[]> StartDisruptor()
    {
        var disruptor = new Disruptor<CalculatorEvent[]>(() => Enumerable.Range(0, MaxCommandsCount)
            .Select(x => new CalculatorEvent())
            .ToArray(), RingBufferSize);

        disruptor
            .HandleEventsWithWorkerPool(new TheadSleepHandler())
            .ThenHandleEventsWithWorkerPool(new CalculatorEventHandler());
        return disruptor.Start();
    }
}