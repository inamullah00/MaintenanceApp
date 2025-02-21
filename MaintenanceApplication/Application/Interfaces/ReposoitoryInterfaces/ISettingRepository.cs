using Maintenance.Domain.Entity.SettingEntities;

namespace Maintenance.Application.Interfaces.ReposoitoryInterfaces
{
    public interface ISettingRepository
    {
        Task<bool> AddSetting(Setting setting, CancellationToken cancellationToken = default);
        Task<List<Setting>> GetAllSettingsAsync(CancellationToken cancellationToken = default);
        Task<Setting?> GetSettingByKeyAsync(string key, CancellationToken cancellationToken = default);
        Task<bool> UpdateSetting(Setting setting, CancellationToken cancellationToken = default);
    }
}
