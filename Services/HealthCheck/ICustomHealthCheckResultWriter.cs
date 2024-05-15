using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AdaptiveWebInterfaces_WebAPI.Services.HealthCheck
{
    public interface ICustomHealthCheckResultWriter
    {
        Task WriteResponse(HttpContext httpContext, HealthReport result);
    }
}
