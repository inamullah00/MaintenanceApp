using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Domain.Entity.UserEntities;
using Maintenance.Application.Common;
using Maintenance.Application.Communication;
using Maintenance.Application.Security;
using Maintenance.Application.Services.Account;
using Maintenance.Application.Services.Admin.ContentSpecification;
using Maintenance.Application.Services.Admin.DisputeSpecification;
using Maintenance.Application.Services.Admin.FeedbackSpecification;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Maintenance.Application.Services.Admin.SetOrderLimit_Performance_Report_Specification;
using Maintenance.Application.Services.Client;
using Maintenance.Application.Services.ClientPayment;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Services.OffereServiceCategory;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Infrastructure.Persistance.Data;
using Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class ServiceManager : IServiceManager
    {
        public IOfferedServiceCategory OfferedServiceCategory { get; private set; }
        public IFreelancerService FreelancerService { get; private set; }
        public IRegisterationService RegisterationService { get; private set; }
        public IClientService OfferedServices { get; private set; }
        public IOrderService OrderService { get; private set; }
        public IContentService ContentService { get; private set; }
        public IDisputeService DisputeService { get; private set; }
        public IFeedbackService FeedbackService { get; private set; }
        public IAdminFreelancerService AdminFreelancerService { get; private set; }
        public IPaymentService PaymentService { get; private set; }
        public INotificationService NotificationService { get; private set; }
       public IFreelancerAuthService FreelancerAuthService { get; private set; }

       public IPasswordService PasswordService { get; private set; }

        public ITokenService TokenService { get; private set; }

        public IEmailService EmailService { get; private set; }

        public ServiceManager(IUnitOfWork unitOfWork,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              SignInManager<ApplicationUser> signInManager,
                              IConfiguration configuration,
                              IHttpContextAccessor httpContextAccessor,
                              IMemoryCache memoryCache,
                              ApplicationDbContext dbContext,
                              IMapper mapper,
                              ILogger<NotificationService> logger , 
                             ITokenService tokenService,
                              IPasswordService PasswordService
            
            )
        {
            OfferedServiceCategory = new OfferedServiceCategory(unitOfWork, mapper);
            FreelancerService = new FreelancerService(mapper, unitOfWork);
            RegisterationService = new RegistrationService(userManager, roleManager, signInManager, configuration, httpContextAccessor, memoryCache, dbContext, mapper);
            OfferedServices = new OfferedServices(unitOfWork, mapper);
            OrderService = new OrderService(unitOfWork, mapper);
            ContentService = new ContentService(unitOfWork, mapper);
            DisputeService = new DisputeService(unitOfWork, mapper);
            FeedbackService = new FeedbackService(unitOfWork, mapper);
            AdminFreelancerService = new AdminFreelancerService(unitOfWork, mapper);
            PaymentService = new PaymentService(unitOfWork, mapper);
            NotificationService = new NotificationService(unitOfWork, mapper, logger);
            FreelancerAuthService = new FreelancerAuthService(unitOfWork, mapper , PasswordService , tokenService);
            PasswordService = new PasswordService();
            TokenService = new TokenService(configuration);
            EmailService = new EmailService(configuration);
        }
    }
}

