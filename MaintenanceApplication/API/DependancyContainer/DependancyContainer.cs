using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Application.Interfaces.ServiceInterfaces.ClientInterfaces;
using Application.Interfaces.ServiceInterfaces.OfferedServiceCategoryInterfaces;
using Application.Interfaces.ServiceInterfaces.RegisterationInterfaces;
using Infrastructure.Repositories.RepositoryImplementions;
using Infrastructure.Repositories.RepositoryImplementions.OfferedServiceImplementation;
using Infrastructure.Repositories.ServiceImplemention;
using Microsoft.OpenApi.Models;

namespace API.DependancyContainer
{
    public static class DependancyContainer
    {
       
        public static IServiceCollection RegistrationServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddScoped<IRegisterationService, RegistrationService>();
            services.AddScoped<IClientService, OfferedServices>();
            services.AddScoped<IOfferedServiceCategory,OfferedServiceCategory>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
            services.AddScoped<IOfferedServiceRepository ,OfferedServiceRepository>();
            services.AddScoped<IOfferedServiceCategoryRepository ,OfferedServiceCategoryRepository>();


            return services;
        }

    }
}
