using System.Text;
using AsyncCalculator.Events;
using Disruptor;

namespace AsyncCalculator.Handlers;

public class CalculatorEventProcessingHandler : IWorkHandler<CalculatorEvent>
{
    private const string ResultTemplate = "Operation Id: {0}, Code: {1}, Result: {2}";

    private readonly CalculatorEventProcessingStrategy EventProcessingStrategy = new();

    public void OnEvent(CalculatorEvent calculatorEvent)
    {
        if (calculatorEvent.OperationId == null)
            return;

        var resultBuilder = new StringBuilder();

        var (code, result) = EventProcessingStrategy.Process(calculatorEvent);

        resultBuilder.AppendFormat(ResultTemplate, calculatorEvent.OperationId, code, result).AppendLine();

        Console.Out.WriteAsync(resultBuilder);

        calculatorEvent.ProcessedDateTimeUtc = ApplicationController.CurrentDateTimeUtc;
    }
}