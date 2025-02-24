using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Maintenance.Application.Helper;
using Maintenance.Application.Interfaces.RepositoryInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.ClientInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.DisputeInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.FreelancerInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Infrastructure.Persistance.Data;
using Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.ConactUsRepositories;
using Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.ClientAuthRepositoryImplementation;
using Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.CountryRepositories;
using Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.DashboardRepositories;
using Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.FreelancerServiceImplementation;
using Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.OfferedServiceImplementation;
using Maintenance.Infrastructure.Repositories;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.UnitofWorkImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IOfferedServiceCategoryRepository OfferedServiceCategoryRepository { get; private set; }

        public IOfferedServiceRepository OfferedServiceRepository { get; private set; }
        public IFreelancerRepository FreelancerRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public IDisputeRepository DisputeRepository { get; }

        public IContentRepository ContentRepository { get; }

        public IAdminFreelancerRepository AdminFreelancerRepository { get; }

        public IFeedbackRepository FeedbackRepository { get; }

        public IFreelancerAuthRepository FreelancerAuthRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IAdminClientRepository AdminClientRepository { get; }
        public IAdminServiceRepository AdminServiceRepository { get; }
        public IAdminPackageRepository AdminPackageRepository { get; }
        public ISettingRepository SettingRepository { get; }
        public IContactUsRepository ContactUsRepository { get; }

     
        public IClientAuthRepository ClientAuthRepository { get; }
        public UnitOfWork(ApplicationDbContext dbContext, IFileUploaderService fileUploaderService)
        {
            _dbContext = dbContext;
            OfferedServiceCategoryRepository = new OfferedServiceCategoryRepository(dbContext);
            OfferedServiceRepository = new OfferedServiceRepository(dbContext);
            FreelancerRepository = new FreelancerRepository(dbContext);
            OrderRepository = new OrderRepository(dbContext);
            DisputeRepository = new DisputeRepository(dbContext);
            ContentRepository = new ContentRepository(dbContext);
            AdminFreelancerRepository = new AdminFreelancerRepository(dbContext);
            FeedbackRepository = new FeedbackRepository(dbContext);
            FreelancerAuthRepository = new FreelancerAuthRepository(dbContext);
            CountryRepository = new CountryRepository(dbContext);
            AdminClientRepository = new AdminClientRepository(dbContext);
            AdminServiceRepository = new AdminServiceRepository(dbContext);
            AdminPackageRepository = new AdminPackageRepository(dbContext);
            SettingRepository = new SettingRepository(dbContext);
            ContactUsRepository = new ContactUsRepository(dbContext, fileUploaderService);
            ClientAuthRepository = new ClientAuthRepository(dbContext);

        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext?.SaveChangesAsync();
        }
    }
}
