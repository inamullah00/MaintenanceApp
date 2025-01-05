using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.AdminOrderInterfaces;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.FreelancerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUnitOFWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IOfferedServiceCategoryRepository OfferedServiceCategoryRepository { get; }
        public IOfferedServiceRepository OfferedServiceRepository { get; }
        public IFreelancerRepository FreelancerRepository  { get; }

        public IOrderRepository OrderRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
