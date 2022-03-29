using AsyncCalculator.Parsers;
using AsyncCalculator.Parsers.CalculatorOperationTypes;

namespace AsyncCalculator;

public static class ApplicationController
{
    public static DateTime CurrentDateTimeUtc => DateTime.UtcNow;

    public static CalculatorOperationTypesComponent? CalculatorOperationTypesComponent { get; private set; }

    public static CalculatorEventsParser? CalculatorEventsParser { get; private set; }

    public static void Init()
    {
        RegisterCalculatorOperationTypesComponent();
        RegisterCalculatorEventsParser();
    }

    private static void RegisterCalculatorOperationTypesComponent()
    {
        CalculatorOperationTypesComponent = new CalculatorOperationTypesComponent();
        CalculatorOperationTypesComponent.Add<SumCalculatorOperationType>();
        CalculatorOperationTypesComponent.Add<DiffCalculatorOperationType>();
    }

    private static void RegisterCalculatorEventsParser()
    {
        var calculatorEventParser = new CalculatorEventParser(CalculatorOperationTypesComponent!.TryGetByOperationCode);
        CalculatorEventsParser = new CalculatorEventsParser(calculatorEventParser);
    }
}