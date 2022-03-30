using System.Text;

namespace AsyncCalculator;

public class MetricsBuilder
{
    private readonly StringBuilder builder = new StringBuilder("Calculation time metrics:").AppendLine();

    public static implicit operator StringBuilder(MetricsBuilder metricsBuilder)
    {
        return metricsBuilder.builder;
    }
}