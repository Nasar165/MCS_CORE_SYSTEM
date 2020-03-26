using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using api.Models;
using xheaderSecurity;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfigHelper.Instance.SetIConfiguration(configuration);
            EncrypterStarter.SetupEncryption();
            StartFileHasher.RegisterFiles();
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
