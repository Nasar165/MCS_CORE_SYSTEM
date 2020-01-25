using Microsoft.AspNetCore.Builder;

namespace api
{
    public class ApplicationStarter
    {
        public static void SetApplicationSettings(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            HealthCheckStarter.InitializeHealthChecks(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
        }
    }
}