using Maintenance.Application.Interfaces.ReposoitoryInterfaces;
using Maintenance.Domain.Entity.SettingEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly ApplicationDbContext _context;

        public SettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSetting(Setting setting, CancellationToken cancellationToken = default)
        {
            await _context.Settings.AddAsync(setting);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdateSetting(Setting setting, CancellationToken cancellationToken = default)
        {
            _context.Settings.Update(setting);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<Setting?> GetSettingByKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            return await _context.Settings.FirstOrDefaultAsync(s => s.Key == key, cancellationToken);
        }

        public async Task<List<Setting>> GetAllSettingsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Settings.ToListAsync(cancellationToken);
        }
    }

}
