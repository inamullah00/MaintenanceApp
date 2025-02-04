using Maintenance.Application.Common;
using Maintenance.Application.Communication;
using Maintenance.Application.Security;
using Maintenance.Application.Services.Account;
using Maintenance.Application.Services.Admin.AdminSpecification;
using Maintenance.Application.Services.Admin.ContentSpecification;
using Maintenance.Application.Services.Admin.DisputeSpecification;
using Maintenance.Application.Services.Admin.FeedbackSpecification;
using Maintenance.Application.Services.Admin.OrderSpecification;
using Maintenance.Application.Services.Admin.SetOrderLimit_Performance_Report_Specification;
using Maintenance.Application.Services.Client;
using Maintenance.Application.Services.ClientPayment;
using Maintenance.Application.Services.Country;
using Maintenance.Application.Services.Freelance;
using Maintenance.Application.Services.FreelancerAuth;
using Maintenance.Application.Services.OffereServiceCategory;

namespace Maintenance.Application.Services.ServiceManager
{
    public interface IServiceManager
    {
        public IOfferedServiceCategory OfferedServiceCategory { get; }
        public IFreelancerService FreelancerService { get; }
        public IRegisterationService RegisterationService { get; }
        public IClientService OfferedServices { get; }
        public IOrderService OrderService { get; }
        public IContentService ContentService { get; }
        public IDisputeService DisputeService { get; }
        public IFeedbackService FeedbackService { get; }
        public IAdminFreelancerService AdminFreelancerService { get; }
        public IPaymentService PaymentService { get; }
        public INotificationService NotificationService { get; }
        public IFreelancerAuthService FreelancerAuthService { get; }
        public IPasswordService PasswordService { get; }
        public ITokenService TokenService { get; }
        public IEmailService EmailService { get; }
        public IAdminService AdminService { get; }
        public ICountryService CountryService { get; }


    }
}
