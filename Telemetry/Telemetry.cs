using System.Diagnostics;

namespace AdaptiveWebInterfaces_WebAPI
{
    public static class Telemetry
    {
        public static readonly ActivitySource ActivitySource = new ActivitySource("AdaptiveWebInterfaces_WebAPI");
    }
}
