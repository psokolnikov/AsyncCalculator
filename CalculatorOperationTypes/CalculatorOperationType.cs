namespace AsyncCalculator.CalculatorOperationTypes;

public abstract record CalculatorOperationType
{
    public abstract string Code { get; }
    public abstract decimal Process(decimal arg1, decimal arg2);
}