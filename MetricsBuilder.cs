using System.Text;

namespace AsyncCalculator;

public class MetricsBuilder
{
    private const string MetricsTemplate = "Operation Id: {0}, Handler: {1}, ProcessingTime: {2}ms";

    private readonly StringBuilder builder = new StringBuilder("Calculation time metrics:").AppendLine();

    public void AppendMetric(int operationId, string handlerName, long totalMilliseconds)
    {
        builder.AppendFormat(MetricsTemplate, operationId, handlerName, totalMilliseconds).AppendLine();
    }

    public void WriteMetricsAsync()
    {
        Console.Out.WriteAsync(builder);
    }
}