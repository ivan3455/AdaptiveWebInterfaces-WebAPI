using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Services.HealthCheck
{
    public class DatabaseConnectionHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public DatabaseConnectionHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync(cancellationToken);
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        return HealthCheckResult.Healthy();
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy("Database connection state is not open");
                    }
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database connection failure", ex);
            }
        }
    }
}
