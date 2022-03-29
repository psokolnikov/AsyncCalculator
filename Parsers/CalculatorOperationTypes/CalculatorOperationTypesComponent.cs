using AsyncCalculator.Events;

namespace AsyncCalculator.Parsers.CalculatorOperationTypes;

public class CalculatorOperationTypesComponent
{
    private readonly List<CalculatorOperationType> items = new();

    public CalculatorOperationType? TryGetByOperationCode(string code)
    {
        return items.SingleOrDefault(x => x.Code == code);
    }

    public void Add<T>() where T : CalculatorOperationType, new()
    {
        var calculatorOperationType = new T();

        if (items.Any(x => x.Code == calculatorOperationType.Code))
            throw new InvalidOperationException(
                $"CalculatorOperationType with code {calculatorOperationType.Code} already registered");

        items.Add(calculatorOperationType);
    }
}