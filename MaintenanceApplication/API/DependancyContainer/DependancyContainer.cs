using Application.Interfaces.ServiceInterfaces;

using Infrastructure.Repositories.ServiceImplemention;
using Microsoft.OpenApi.Models;

namespace API.DependancyContainer
{
    public static class DependancyContainer
    {
        public static IServiceCollection RegistrationServices(this IServiceCollection services)
        {

            services.AddScoped<IRegisterationService, RegistrationService>();


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MaintenanceAapp API'S", Version = "v1" });

                // Add JWT Authentication
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });


                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
            });

            return services;
        }

    }
}
