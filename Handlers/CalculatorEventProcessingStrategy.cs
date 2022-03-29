using System.Globalization;
using AsyncCalculator.Events;

namespace AsyncCalculator.Handlers;

public class CalculatorEventProcessingStrategy
{
    public (string Code, string? Result) Process(CalculatorEvent calculatorEvent)
    {
        if (calculatorEvent.OperationType == null)
            return ("OperationNotFound", null);

        if (calculatorEvent.Argument1 == null)
            return ("FirstArgumentNotFound", null);

        if (calculatorEvent.Argument2 == null)
            return ("SecondArgumentNotFound", null);

        var result = calculatorEvent.OperationType
            .Process(calculatorEvent.Argument1.Value, calculatorEvent.Argument2.Value);

        return ("Success", result.ToString(CultureInfo.InvariantCulture));
    }
}