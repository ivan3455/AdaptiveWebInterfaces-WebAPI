using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class CustomTraceExporter : BaseExporter<Activity>
{
    private readonly HttpClient _httpClient;

    public CustomTraceExporter()
    {
        _httpClient = new HttpClient();
    }

    public override ExportResult Export(in Batch<Activity> batch)
    {
        foreach (var activity in batch)
        {
            var traceData = FormatActivity(activity);

            var response = _httpClient.PostAsync("backend-endpoint", new StringContent(traceData)).Result;

            if (!response.IsSuccessStatusCode)
            {
                return ExportResult.Failure;
            }
        }

        return ExportResult.Success;
    }

    protected override bool OnShutdown(int timeoutMilliseconds)
    {
        _httpClient.Dispose();
        return true;
    }

    private string FormatActivity(Activity activity)
    {
        var activityData = new Dictionary<string, object>
        {
            { "TraceId", activity.TraceId.ToString() },
            { "SpanId", activity.SpanId.ToString() },
            { "ParentSpanId", activity.ParentSpanId.ToString() },
            { "Name", activity.DisplayName },
            { "StartTime", activity.StartTimeUtc },
            { "Duration", activity.Duration },
            { "Attributes", activity.TagObjects }
        };

        return System.Text.Json.JsonSerializer.Serialize(activityData);
    }
}
