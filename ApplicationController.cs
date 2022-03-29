using AsyncCalculator.CalculatorOperationTypes;
using AsyncCalculator.Parsers;

namespace AsyncCalculator;

public static class ApplicationController
{
    static ApplicationController()
    {
        RegisterCalculatorOperationTypesComponent();
        RegisterCalculatorEventParser();
    }

    public static CalculatorOperationTypesComponent CalculatorOperationTypesComponent { get; private set; } = null!;

    public static CalculatorEventParser CalculatorEventParser { get; private set; } = null!;

    private static void RegisterCalculatorOperationTypesComponent()
    {
        CalculatorOperationTypesComponent = new CalculatorOperationTypesComponent();
        CalculatorOperationTypesComponent.Add<SumCalculatorOperationType>();
        CalculatorOperationTypesComponent.Add<DiffCalculatorOperationType>();
    }

    private static void RegisterCalculatorEventParser()
    {
        CalculatorEventParser = new CalculatorEventParser(CalculatorOperationTypesComponent.TryGetByOperationCode);
    }
}