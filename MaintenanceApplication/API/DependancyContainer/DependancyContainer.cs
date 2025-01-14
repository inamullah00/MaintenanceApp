using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Infrastructure.Repositories.RepositoryImplementions;
using Infrastructure.Repositories.RepositoryImplementions.OfferedServiceImplementation;
using Infrastructure.Repositories.RepositoryImplementions.UnitofWorkImplementation;
using Infrastructure.Repositories.ServiceImplemention;
using Maintenance.Application.Services.Account;
using Maintenance.Application.Services.Admin.ContentSpecification;
using Maintenance.Application.Services.Admin.DisputeSpecification;
using Maintenance.Application.Services.Admin.FeedbackSpecification;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Maintenance.Application.Services.Admin.SetOrderLimit_Performance_Report_Specification;
using Maintenance.Application.Services.Client;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.OffereServiceCategory;
using Maintenance.Infrastructure.Repositories.ServiceImplemention;
using Maintenance.Infrastructure.Repositories.ServiceImplemention.DashboardServiceImplemention;
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
            services.AddScoped<IFreelancerService,FreelancerService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IDisputeService,DisputeService>();
            services.AddScoped<IContentService,ContentService>();
            services.AddScoped<IAdminFreelancerService,AdminFreelancerService>();
            services.AddScoped<IFeedbackService,FeedbackService>();

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

            return services;
        }

    }
}
