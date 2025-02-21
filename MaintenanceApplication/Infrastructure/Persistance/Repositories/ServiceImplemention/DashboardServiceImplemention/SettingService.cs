using Application.Interfaces.IUnitOFWork;
using AutoMapper;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ApplicationSetting.SettingSpecification;
using Maintenance.Application.ViewModel;
using Maintenance.Domain.Entity.SettingEntities;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention.DashboardServiceImplemention
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string?> Get(string key)
        {
            var setting = await _unitOfWork.SettingRepository.GetSettingByKeyAsync(key);
            return setting?.Value;
        }

        public async Task Set(SettingViewModel model, CancellationToken cancellationToken)
        {
            await SetSettingData(model, cancellationToken);
        }

        public async Task SetInBulk(List<SettingViewModel> models, CancellationToken cancellationToken)
        {
            foreach (var model in models)
            {
                await SetSettingData(model, cancellationToken);
            }
        }

        public async Task<List<SettingViewModel>> GetAllSettingsAsync()
        {
            var settings = await _unitOfWork.SettingRepository.GetAllSettingsAsync();
            return settings.Select(s => new SettingViewModel { Key = s.Key, Value = s.Value }).ToList();
        }

        private async Task SetSettingData(SettingViewModel model, CancellationToken cancellationToken)
        {
            var existingSetting = await _unitOfWork.SettingRepository.GetSettingByKeyAsync(model.Key, cancellationToken);
            if (existingSetting == null)
            {
                var newSetting = new Setting
                {
                    Key = model.Key,
                    Value = model.Value
                };
                var createResult = await _unitOfWork.SettingRepository.AddSetting(newSetting, cancellationToken);
                if (!createResult) throw new CustomException("Failed to set a new setting.");
            }
            else
            {
                existingSetting.Value = model.Value;
                var updatedResult = await _unitOfWork.SettingRepository.UpdateSetting(existingSetting, cancellationToken);
                if (!updatedResult) throw new CustomException("Failed to update a setting.");
            }
        }
    }
}
