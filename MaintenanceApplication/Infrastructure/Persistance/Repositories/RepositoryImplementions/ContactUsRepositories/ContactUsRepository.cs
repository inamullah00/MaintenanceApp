using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Maintenance.Application.Helper;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.SettingEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.ConactUsRepositories
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploaderService _fileUploaderService;
        private readonly string _baseImageUrl;

        public ContactUsRepository(ApplicationDbContext context, IFileUploaderService fileUploaderService)
        {
            _context = context;
            _fileUploaderService = fileUploaderService;
            _baseImageUrl = _fileUploaderService.GetImageBaseUrl();

        }
        public async Task<PaginatedResponse<ContactUsResponseViewModel>> GetAllListAsync(ContactUsFilterViewModel filter, ISpecification<ContactUs>? specification = null)
        {
            var query = SpecificationEvaluator.Default.GetQuery(query: _context.ContactUs.AsNoTracking().AsQueryable(), specification: specification);
            var filteredQuery = query.OrderByDescending(a => a.Id)
                    .Select(d => new ContactUsResponseViewModel
                    {
                        Id = d.Id,
                        FullName = d.FullName,
                        Email = d.Email,
                        Message = d.Message,
                        PhoneNumber = d.PhoneNumber,
                        CreatedDate = d.CreatedAt,
                        ProfileImageLink = d.FreelancerId.HasValue ? d.Freelancer.ProfilePicture : d.ClientId.HasValue ? d.Client.ProfilePicture : null,
                        ImageBaseUrl = _baseImageUrl,
                        IsRead = d.IsRead

                    });

            var totalCount = await filteredQuery.CountAsync();
            var contactUsList = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PaginatedResponse<ContactUsResponseViewModel>(contactUsList, totalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<PaginatedResponse<ContactUsResponseViewModel>> GetPagedListAsync(ContactUsFilterViewModel filter)
        {
            var filteredQuery = _context.ContactUs.Where(r => r.IsRead == false).OrderByDescending(a => a.Id)
                .Select(d => new ContactUsResponseViewModel
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Email = d.Email,
                    Message = d.Message,
                    PhoneNumber = d.PhoneNumber,
                    CreatedDate = d.CreatedAt,
                    ProfileImageLink = d.FreelancerId.HasValue ? d.Freelancer.ProfilePicture : d.ClientId.HasValue ? d.Client.ProfilePicture : null,
                    ImageBaseUrl = _baseImageUrl,
                    IsRead = d.IsRead
                });
            var totalCount = await filteredQuery.CountAsync();
            var contactUsList = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            return new PaginatedResponse<ContactUsResponseViewModel>(contactUsList, totalCount, filter.PageNumber, filter.PageSize);

        }

        public async Task<int> NotificationCount()
        {
            return await _context.ContactUs.Where(r => r.IsRead == false).CountAsync().ConfigureAwait(false);
        }

        public async Task<ContactUs?> GetContactUsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ContactUs.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<bool> AddContactUs(ContactUs contactUs, CancellationToken cancellationToken = default)
        {
            await _context.ContactUs.AddAsync(contactUs, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdateContactUs(ContactUs contactUs, CancellationToken cancellationToken = default)
        {
            _context.ContactUs.Update(contactUs);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
