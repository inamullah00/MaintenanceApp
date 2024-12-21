using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface.OfferedServiceCategoryInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories.RepositoryImplementions.OfferedServiceImplementation;
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


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            OfferedServiceCategoryRepository = new OfferedServiceCategoryRepository(dbContext);
            OfferedServiceRepository = new OfferedServiceRepository(dbContext);
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
