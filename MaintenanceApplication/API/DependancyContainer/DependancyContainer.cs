using Application.Interfaces.IUnitOFWork;
using Maintenance.Application.Common;
using Maintenance.Application.Security;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.UnitofWorkImplementation;
using Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention;
using Microsoft.OpenApi.Models;

namespace API.DependancyContainer
{
    public static class DependancyContainer
    {

        public static IServiceCollection RegistrationServices(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddScoped<IRegisterationService, RegistrationService>();
            //services.AddScoped<IClientService, OfferedServices>();
            //services.AddScoped<IOfferedServiceCategory,OfferedServiceCategory>();
            //services.AddScoped<IFreelancerService,FreelancerService>();
            //services.AddScoped<IOrderService,OrderService>();
            //services.AddScoped<IDisputeService,DisputeService>();
            //services.AddScoped<IContentService,ContentService>();
            //services.AddScoped<IAdminFreelancerService,AdminFreelancerService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ITokenService, TokenService>();


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


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }

    }
}
