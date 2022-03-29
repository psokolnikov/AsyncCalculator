using AsyncCalculator.Parsers.CalculatorOperationTypes;

namespace AsyncCalculator.Events;

public class CalculatorEvent
{
    public DateTime CreatedAt { get; set; }
    public int? OperationId { get; set; }
    public CalculatorOperationType? OperationType { get; set; }
    public decimal? Argument1 { get; set; }
    public decimal? Argument2 { get; set; }

    public void Clear()
    {
        CreatedAt = DateTime.MinValue;
        OperationId = null;
        OperationType = null;
        Argument1 = null;
        Argument2 = null;
    }
}