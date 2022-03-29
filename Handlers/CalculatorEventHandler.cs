using System.Diagnostics;
using System.Text;
using AsyncCalculator.Events;
using Disruptor;

namespace AsyncCalculator.Handlers;

public class CalculatorEventHandler : IWorkHandler<CalculatorEvent[]>
{
    private const string ResultTemplate = "Operation Id: {0}, Code: {1}, Result: {2}";
    private const string MetricsTemplate = "Operation Id: {0}, ProcessingTime: {1}ms, TotalTime: {2}ms";

    private readonly CalculatorEventProcessingStrategy EventProcessingStrategy = new();

    public void OnEvent(CalculatorEvent[] calculatorEvents)
    {
        var timer  = new Stopwatch();
        var resultBuilder = new StringBuilder();
        var metricsBuilder = new StringBuilder();

        for (int i = 0; i < calculatorEvents.Length; i++)
        {
            var calculatorEvent = calculatorEvents[i];

            if (calculatorEvent.OperationId == null)
                break;

            timer.Restart();
            var (code, result) = EventProcessingStrategy.Process(calculatorEvent);
            timer.Stop();

            var totalTime = (long)(ApplicationController.CurrentDateTimeUtc - calculatorEvent.CreatedAt).TotalMilliseconds;
            var processingTime = timer.ElapsedMilliseconds;

            resultBuilder.AppendFormat(ResultTemplate, calculatorEvent.OperationId, code, result).AppendLine();
            metricsBuilder.AppendFormat(MetricsTemplate, calculatorEvent.OperationId, processingTime, totalTime).AppendLine();
        }

        resultBuilder.AppendLine("------------------------------------------").Append(metricsBuilder);

        Console.Out.WriteAsync(resultBuilder);
    }
}