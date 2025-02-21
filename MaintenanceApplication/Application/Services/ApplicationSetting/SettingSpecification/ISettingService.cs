using Maintenance.Application.ViewModel;

namespace Maintenance.Application.Services.ApplicationSetting.SettingSpecification
{
    public interface ISettingService
    {
        Task<string?> Get(string key);
        Task<List<SettingViewModel>> GetAllSettingsAsync();
        Task Set(SettingViewModel model, CancellationToken cancellationToken);
        Task SetInBulk(List<SettingViewModel> models, CancellationToken cancellationToken);
    }
}
