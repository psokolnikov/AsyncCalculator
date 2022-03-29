namespace AsyncCalculator.CalculatorOperationTypes;

public record SumCalculatorOperationType : CalculatorOperationType
{
    public override string Code => "Sum";
    public override decimal Process(decimal arg1, decimal arg2) => arg1 + arg2;
}