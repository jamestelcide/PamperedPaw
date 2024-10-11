using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PamperedPaw.Core.Domain.IdentityEntities;
using PamperedPaw.Core.Domain.RepositoryContracts;
using PamperedPaw.Core.ServiceContracts;
using PamperedPaw.Core.Services;
using PamperedPaw.Infrastructure.DbContext;
using PamperedPaw.Infrastructure.Repositories;

namespace PamperedPaw.WebAPI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPetGuestRepository, PetGuestRepository>();
            services.AddScoped<IPetGuestService, PetGuestService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Enables identity for the project
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 3;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole,
                ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole,
                ApplicationDbContext, Guid>>();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser().Build();

                options.AddPolicy("NotAuthenticated", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return !context.User.Identity.IsAuthenticated;
                    });
                });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            return services;
        }
    }
}
