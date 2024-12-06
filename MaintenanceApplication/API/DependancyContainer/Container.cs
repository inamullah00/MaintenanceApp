using Application.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories.ServiceImplemention;

namespace API.DependancyContainer
{
    public static class Container
    {

     

        public static IServiceCollection RegistrationServices(this IServiceCollection services)
        {

            services.AddScoped<IRegisterationService, RegistrationService>();
            return services;
        }

    }
}
