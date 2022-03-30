using System.Text;
using AsyncCalculator.Events;
using AsyncCalculator.Helpers;
using Disruptor;

namespace AsyncCalculator.Handlers;

public class CalculatorEventHandlerMetricsDecorator : IWorkHandler<CalculatorEvent>
{
    private readonly IWorkHandler<CalculatorEvent> origin;
    private readonly MetricsBuilder metricsBuilder;

    public CalculatorEventHandlerMetricsDecorator(IWorkHandler<CalculatorEvent> origin, MetricsBuilder metricsBuilder)
    {
        this.origin = origin;
        this.metricsBuilder = metricsBuilder;
    }

    public void OnEvent(CalculatorEvent evt)
    {
        if (evt.OperationId == null)
            return;

        var handlerName = origin.GetType().Name;
        var processingTime = TimeMeasurer.GetTimeInMilliseconds(() => origin.OnEvent(evt));
        metricsBuilder.AppendMetric(evt.OperationId.Value, handlerName, processingTime);
    }
}