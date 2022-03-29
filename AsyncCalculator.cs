using System.Text;
using AsyncCalculator.Events;
using AsyncCalculator.Handlers;
using Disruptor;
using Disruptor.Dsl;

namespace AsyncCalculator;

public class AsyncCalculator
{
    private const int MaxCommandsCount = 3;
    private const int RingBufferSize = 16;

    private readonly StringBuilder metricsBuilder = new();
    private RingBuffer<CalculatorEvent>? ringBuffer;
    private ISequenceBarrier? sequenceBarrier;

    public string Metrics => metricsBuilder.ToString();

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

        foreach (var argument in arguments)
        {
            var sequenceNo = ringBuffer.Next();
            var targetEvent = ringBuffer[sequenceNo];

            ApplicationController.CalculatorEventParser.TryParse(argument, targetEvent);

            ringBuffer.Publish(sequenceNo);
        }
    }

    public void WriteMetrics()
    {
        if (ringBuffer == null)
            throw new InvalidOperationException("AsyncCalculator.Start() must be called");
        if (sequenceBarrier == null)
            throw new InvalidOperationException("AsyncCalculator.Execute(...) must be called");

        while (ringBuffer.Cursor != sequenceBarrier.Cursor)
        {
            Thread.Sleep(200);
        }

        Console.Out.WriteAsync(metricsBuilder);
    }

    private RingBuffer<CalculatorEvent> StartDisruptor()
    {
        var disruptor = new Disruptor<CalculatorEvent>(() => new CalculatorEvent(), RingBufferSize);

        sequenceBarrier = disruptor
            .HandleEventsWithWorkerPool(new ThreadSleepHandler())
            .ThenHandleEventsWithWorkerPool(new CalculatorEventHandlerMetricsDecorator(
                new CalculatorEventProcessingHandler(),
                metricsBuilder
            ))
            .AsSequenceBarrier();

        return disruptor.Start();
    }
}