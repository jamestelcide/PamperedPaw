using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PamperedPaw.Core.Domain.IdentityEntities;
using PamperedPaw.Core.Domain.RepositoryContracts;
using PamperedPaw.Core.ServiceContracts;
using PamperedPaw.Core.Services;
using PamperedPaw.Infrastructure.DbContext;
using PamperedPaw.Infrastructure.Repositories;
using Serilog;
using System.Configuration;

namespace PamperedPaw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Serilog
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
            {

                loggerConfiguration
                .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
                .ReadFrom.Services(services); //read out current app's services and make them available to serilog
            });

            builder.Services.AddScoped<IPetGuestRepository, PetGuestRepository>();
            builder.Services.AddScoped<IPetGuestService, PetGuestService>();

            builder.Services.AddControllers(options => {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            })
            .AddXmlSerializerFormatters();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Swagger
            builder.Services.AddEndpointsApiExplorer(); //Generates description for all endpoints

            builder.Services.AddSwaggerGen(options => {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
            }); //generates OpenAPI specification

            //CORS: localhost:4200, localhost:4100
            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                    .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
                    .WithHeaders("Authorization", "origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    ;
                });
            });

            //Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();
          
            var app = builder.Build();

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseSwagger(); //creates endpoint for swagger.json
            app.UseSwaggerUI(); //creates swagger UI for testing all Web API endpoints / action methods
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
