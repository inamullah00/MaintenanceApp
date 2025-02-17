using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class AdminClientRepository : IAdminClientRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Client> GetClientByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(a => a.Email.ToLower().Trim().Equals(email.ToLower().Trim()), cancellationToken);
        }
        public async Task<Client> GetClientByPhoneNumberAsync(string phoneNumber, Guid? countryId, CancellationToken cancellationToken)
        {
            return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(f => !string.IsNullOrEmpty(f.PhoneNumber) && f.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim()) && f.CountryId == countryId, cancellationToken);
        }

        public async Task<Client?> GetClientByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Clients.Include(f => f.Country).FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateClient(Client client, CancellationToken cancellationToken = default)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> AddClient(Client client, CancellationToken cancellationToken = default)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


        public async Task<PaginatedResponse<ClientResponseViewModel>> GetFilteredClientAsync(ClientFilterViewModel filter, ISpecification<Client>? specification = null)
        {
            var query = SpecificationEvaluator.Default.GetQuery(query: _context.Clients.AsNoTracking().AsQueryable(), specification: specification);

            var filteredQuery = (from Client in query
                                 join country in _context.Countries.AsNoTracking()
                                     on Client.CountryId equals country.Id into countryGroup
                                 from country in countryGroup.DefaultIfEmpty()
                                 orderby Client.FullName
                                 select new ClientResponseViewModel
                                 {
                                     Id = Client.Id.ToString(),
                                     FullName = Client.FullName ?? string.Empty,
                                     Email = Client.Email ?? string.Empty,
                                     DialCode = country != null ? country.DialCode : string.Empty,
                                     CountryId = Client.CountryId,
                                     PhoneNumber = Client.PhoneNumber ?? string.Empty,
                                     IsActive = Client.IsActive,
                                 });
            var totalCount = await filteredQuery.CountAsync();

            var clients = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PaginatedResponse<ClientResponseViewModel>(clients, totalCount, filter.PageNumber, filter.PageSize);
        }



    }
}
