using System.Text;
using AsyncCalculator.Events;
using AsyncCalculator.Helpers;
using Disruptor;

namespace AsyncCalculator.Handlers;

public class CalculatorEventHandlerMetricsDecorator : IWorkHandler<CalculatorEvent>
{
    private const string MetricsTemplate = "Operation Id: {0}, CalculationTime: {1}ms, TotalEventProcessingTime: {2}ms";

    private readonly IWorkHandler<CalculatorEvent> origin;
    private readonly StringBuilder metricsBuilder;

    public CalculatorEventHandlerMetricsDecorator(IWorkHandler<CalculatorEvent> origin, StringBuilder metricsBuilder)
    {
        this.origin = origin;
        this.metricsBuilder = metricsBuilder;
    }

    public void OnEvent(CalculatorEvent evt)
    {
        if (evt.OperationId == null)
            return;

        var processingTime = TimeMeasurer.GetTimeInMilliseconds(() => origin.OnEvent(evt));
        var totalTime = TimeMeasurer.GetTimeInMilliseconds(evt.CreatedDateTimeUtc, evt.ProcessedDateTimeUtc!.Value);
        metricsBuilder.AppendFormat(MetricsTemplate, evt.OperationId, processingTime, totalTime).AppendLine();
    }
}