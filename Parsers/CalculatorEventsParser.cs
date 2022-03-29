using AsyncCalculator.Events;

namespace AsyncCalculator.Parsers;

public class CalculatorEventsParser
{
    private readonly CalculatorEventParser calculatorEventParser;

    public CalculatorEventsParser(CalculatorEventParser calculatorEventParser)
    {
        this.calculatorEventParser = calculatorEventParser;
    }

    public void TryParse(string[] commands, CalculatorEvent[] targetEvents)
    {
        for (var i = 0; i < targetEvents.Length; i++)
        {
            if (i < commands.Length)
            {
                calculatorEventParser.TryParse(command: commands[i], target: targetEvents[i]);
            }
            else
            {
                targetEvents[i].Clear();
            }
        }
    }
}