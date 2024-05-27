using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AdaptiveWebInterfaces_WebAPI.Services.Health_Check
{
    public class ServiceHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
        {
            bool isHealthy = true;

            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Service is healthy."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Service is unhealthy."));
            }
        }
    }
}
