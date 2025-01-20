using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.DisputeInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.Order_Limit_PerformanceReportin_interfaces;
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
        Task<int> SaveChangesAsync();
    }
}
