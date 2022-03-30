using System.Text;
using AsyncCalculator.Events;
using Disruptor;

namespace AsyncCalculator.Handlers;

public class CalculatorEventProcessingHandler : IWorkHandler<CalculatorEvent>
{
    private const string ResultTemplate = "Operation Id: {0}, Code: {1}, Result: {2}";

    private readonly CalculatorEventProcessingStrategy EventProcessingStrategy = new();

    public void OnEvent(CalculatorEvent evt)
    {
        if (evt.OperationId == null)
            return;

        var (code, result) = EventProcessingStrategy.Process(evt);

        var outputStringBuilder = new StringBuilder()
            .AppendFormat(ResultTemplate, evt.OperationId, code, result)
            .AppendLine();

        Console.Out.WriteAsync(outputStringBuilder);
    }
}