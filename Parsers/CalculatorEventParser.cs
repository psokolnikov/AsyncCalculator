using AsyncCalculator.CalculatorOperationTypes;
using AsyncCalculator.Events;

namespace AsyncCalculator.Parsers;

public class CalculatorEventParser
{
    private delegate void CalculatorEventPropertyParser(string propertyStringValue, CalculatorEvent targetEvent);

    private readonly List<CalculatorEventPropertyParser> propertiesParsers;

    public CalculatorEventParser(Func<string, CalculatorOperationType?> tryGetOperationTypeByCode)
    {
        propertiesParsers = new()
        {
            (propertyStringValue, targetEvent) =>
                targetEvent.OperationId = int.TryParse(propertyStringValue, out var operationId) ? operationId : null,
            (propertyStringValue, targetEvent) =>
                targetEvent.OperationType = tryGetOperationTypeByCode(propertyStringValue),
            (propertyStringValue, targetEvent) =>
                targetEvent.Argument1 = decimal.TryParse(propertyStringValue, out var argument1) ? argument1 : null,
            (propertyStringValue, targetEvent) =>
                targetEvent.Argument2 = decimal.TryParse(propertyStringValue, out var argument2) ? argument2 : null
        };
    }

    public void TryParse(string command, CalculatorEvent target)
    {
        if (string.IsNullOrWhiteSpace(command))
            throw new ArgumentNullException(nameof(command));

        var propertyStringValues = command.Trim('\"').Split(',');

        for (var i = 0; i < propertiesParsers.Count; i++)
        {
            if (i > propertyStringValues.Length - 1)
                break;

            propertiesParsers[i](propertyStringValues[i], target);
        }
    }
}