using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Maintenance.Application.Interfaces.RepositoryInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.DisputeInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.FreelancerInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;

namespace Application.Interfaces.IUnitOFWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IOfferedServiceCategoryRepository OfferedServiceCategoryRepository { get; }
        public IOfferedServiceRepository OfferedServiceRepository { get; }
        public IFreelancerRepository FreelancerRepository { get; }
        public IDisputeRepository DisputeRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IContentRepository ContentRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }
        public IAdminFreelancerRepository AdminFreelancerRepository { get; }
        public IFreelancerAuthRepository FreelancerAuthRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IAdminClientRepository AdminClientRepository { get; }
        public IAdminServiceRepository AdminServiceRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
