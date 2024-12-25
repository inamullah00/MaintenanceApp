using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Application.Interfaces.ServiceInterfaces.OfferedServiceCategoryInterfaces;
using Application.Interfaces.ServiceInterfaces.RegisterationInterfaces;
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
        Task<int> SaveChangesAsync();
    }
}
