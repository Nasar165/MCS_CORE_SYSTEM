using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Components;
using System.IO;

namespace api.HealthChecks
{
    public class FileWriterCheck : IHealthCheck
    {
        private void EnsureThatFilePathExists(string filepath, FileWriter writer)
        {
            if (!Validation.DirecortyPathExists(filepath))
                writer.CreateDirectoryPath(filepath);
            if (!Validation.FilePathExists($"{filepath}health_cheack.txt"))
                writer.CreateFile($"{filepath}health_cheack.txt");
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var writer = new FileWriter();
                var filepath = $"{Directory.GetCurrentDirectory()}/logs/";
                EnsureThatFilePathExists(filepath, writer);
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