using AsyncCalculator.CalculatorOperationTypes;

namespace AsyncCalculator.Events;

public class CalculatorEvent
{
    public DateTime CreatedDateTimeUtc { get; set; }
    public DateTime? ProcessedDateTimeUtc { get; set; }
    public int? OperationId { get; set; }
    public CalculatorOperationType? OperationType { get; set; }
    public decimal? Argument1 { get; set; }
    public decimal? Argument2 { get; set; }
}