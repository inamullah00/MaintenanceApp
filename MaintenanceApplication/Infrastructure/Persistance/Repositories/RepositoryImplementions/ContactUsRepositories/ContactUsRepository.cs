using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Infrastructure.Persistance.Data;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.ConactUsRepositories
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactUsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
