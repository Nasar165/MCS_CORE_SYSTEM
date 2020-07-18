using System.Linq;
using api.HealthChecks;
using api.HealthChecks.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace api
{
    public class HealthCheckStarter
    {
        public static void InstallHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<CheckDatabase>("Database Checkup")
                .AddCheck<FileWriterCheck>("FileWriter Check");
        }

        private static void AddSecurityToHealthCheck(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints => endpoints.MapHealthChecks(
                "/healthcheck").RequireAuthorization());
        }

        public static void InitializeHealthChecks(IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var Response = new HealthCheckResponse()
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheck
                        {
                            Status = x.Value.Status.ToString(),
                            Component = x.Key,
                            Description = x.Value.Description,
                            Duration = x.Value.Duration
                        }),
                        Duration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(Response));
                }
            });
            AddSecurityToHealthCheck(app);
        }
    }
}