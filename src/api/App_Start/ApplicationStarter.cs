using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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