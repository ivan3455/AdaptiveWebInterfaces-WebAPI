using AdaptiveWebInterfaces_WebAPI.Services.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AdaptiveWebInterfaces_WebAPI.Services.Health_Check
{
    public class CustomHealthCheckResultWriter : ICustomHealthCheckResultWriter
    {
        public Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                Status = result.Status.ToString(),
                TotalChecks = result.Entries.Count,
                Results = result.Entries
            };

            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return httpContext.Response.WriteAsync(jsonResult);
        }
    }
}