using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Components;

namespace api.HealthChecks
{
    public class FileWriterCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var writer = new FileWriter();
                return Task.FromResult(HealthCheckResult.Healthy("Passed"));
            }
            catch
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("File Writer failed."));
            }
        }
    }
}