using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories.RepositoryImplementions.OfferedServiceImplementation;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.DisputeInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.Order_Limit_PerformanceReportin_interfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using Maintenance.Application.Services.Admin.ContentSpecification;
using Maintenance.Infrastructure.Repositories.RepositoryImplementions.DashboardRepositories;
using Maintenance.Infrastructure.Repositories.RepositoryImplementions.FreelancerServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.RepositoryImplementions.UnitofWorkImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IOfferedServiceCategoryRepository OfferedServiceCategoryRepository { get; private set; }

        public IOfferedServiceRepository OfferedServiceRepository { get; private set; }
        public IFreelancerRepository FreelancerRepository { get; private set; }

       public IOrderRepository OrderRepository {  get; private set; }

        public IDisputeRepository DisputeRepository { get; }

       public IContentRepository ContentRepository {  get; }

       public IAdminFreelancerRepository AdminFreelancerRepository { get; }

       public IFeedbackRepository FeedbackRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
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
