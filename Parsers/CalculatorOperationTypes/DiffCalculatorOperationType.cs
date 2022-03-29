using AsyncCalculator.Events;

namespace AsyncCalculator.Parsers.CalculatorOperationTypes;

public record DiffCalculatorOperationType : CalculatorOperationType
{
    public override string Code => "Diff";
    public override decimal Process(decimal arg1, decimal arg2) => arg1 - arg2;
}