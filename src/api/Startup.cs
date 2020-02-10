using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using api.Models;
using Components;
using Components.Security;
using api.Middleware;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfigHelper.Instance.SetIConfiguration(configuration);
            EncrypterStarter.SetupEncryption();
            var fileTest = new SHA256FileHash(new FileWriter());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtStarter.InitJwtTokenAuth(services);
            services.AddControllers();
            services.AddHttpContextAccessor();
            SingletonStarter.RegisterSingleton(services);
            HealthCheckStarter.InstallHealthChecks(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseMiddleware<SecureHeaderMiddleware>();
            ApplicationStarter.SetApplicationSettings(app);

        }
    }
}
