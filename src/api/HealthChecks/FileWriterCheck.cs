using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Components;
using System.IO;

namespace api.HealthChecks
{
    public class FileWriterCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var writer = new FileWriter();
                var filepath = $"{Directory.GetCurrentDirectory()}/logs/";
                writer.EnsureThatFilePathExists(filepath, "health_cheack.txt");
                writer.AppendTextToFile("Appending Text to File", $"{filepath}health_cheack.txt", FileMode.Append);
                writer.DeleteFile($"{filepath}/health_cheack.txt");
                return Task.FromResult(HealthCheckResult.Healthy("Passed"));
            }
            catch
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("File Writer failed."));
            }
        }
    }
}