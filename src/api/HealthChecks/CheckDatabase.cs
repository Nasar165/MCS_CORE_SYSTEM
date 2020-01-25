using System.Threading;
using System.Threading.Tasks;
using api.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace api.HealthChecks
{
    public sealed class CheckDatabase : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var sql = new NpgsqlConnection(AppConfigHelper.Instance.GetDbConnection());
            try
            {
                sql.Open();
                return Task.FromResult(HealthCheckResult.Healthy("Passed"));
            }
            catch
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Failed to Validate Database Connection."));
            }
            finally
            {
                sql.Close();
            }
        }
    }
}