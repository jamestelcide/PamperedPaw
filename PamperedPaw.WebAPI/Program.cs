using PamperedPaw.WebAPI.StartupExtensions;
using Serilog;

namespace PamperedPaw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration
                .ReadFrom.Configuration(context.Configuration) //reads configuration settings from built-in IConfiguration
                .ReadFrom.Services(services); //reads current app's services and make them available to serilog
            });

            builder.Services.ConfigureServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
