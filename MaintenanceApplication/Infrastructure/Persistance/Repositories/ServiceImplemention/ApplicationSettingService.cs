using Application.Interfaces.IUnitOFWork;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ApplicationSetting;
using Maintenance.Application.ViewModel;
using Maintenance.Domain.Entity.SettingEntities;
using Maintenance.Infrastructure.Extensions;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class ApplicationSettingService : IApplicationSettingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationSettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TermsAndConditonsViewModel> GetTermsAndConditions()
        {
            var termAndConditionSettings = await GetByKeys(new List<string>
            {
                SettingKeyConstants.KeyTermsAndConditions,
                SettingKeyConstants.KeyTermsAndConditionsArabic
            }).ConfigureAwait(false);
            var termAndCondition = new TermsAndConditonsViewModel
            {
                TermsAndCondition = termAndConditionSettings.GetValueOrDefault(SettingKeyConstants.KeyTermsAndConditions),
                TermsAndConditionArabic = termAndConditionSettings.GetValueOrDefault(SettingKeyConstants.KeyTermsAndConditionsArabic),
            };
            return termAndCondition;
        }

        public async Task SetTermsAndConditions(TermsAndConditonsViewModel model, CancellationToken cancellationToken)
        {
            var settingModels = new List<SettingViewModel>();
            AddSettingModel(SettingKeyConstants.KeyTermsAndConditions, model.TermsAndCondition ?? string.Empty, settingModels);
            AddSettingModel(SettingKeyConstants.KeyTermsAndConditionsArabic, model.TermsAndConditionArabic ?? string.Empty, settingModels);
            await SetInBulk(settingModels, cancellationToken);
        }

        public async Task<AboutUsViewModel> GetAboutUs()
        {
            var aboutUsSettings = await GetByKeys(new List<string>
            {
                SettingKeyConstants.KeyAboutUs,
                SettingKeyConstants.KeyAboutUsArabic
            }).ConfigureAwait(false);

            var aboutUsViewModel = new AboutUsViewModel
            {
                AboutUs = aboutUsSettings.GetValueOrDefault(SettingKeyConstants.KeyAboutUs),
                AboutUsArabic = aboutUsSettings.GetValueOrDefault(SettingKeyConstants.KeyAboutUsArabic),
            };
            return aboutUsViewModel;
        }

        public async Task SetAboutUs(AboutUsViewModel model, CancellationToken cancellationToken)
        {
            var settingModels = new List<SettingViewModel>();
            AddSettingModel(SettingKeyConstants.KeyAboutUs, model.AboutUs ?? string.Empty, settingModels);
            AddSettingModel(SettingKeyConstants.KeyAboutUsArabic, model.AboutUsArabic ?? string.Empty, settingModels);
            await SetInBulk(settingModels, cancellationToken);
        }

        public async Task<PrivacyAndPolicyViewModel> GetPrivacyPolicy()
        {
            var privacySettings = await GetByKeys(new List<string>
            {
                SettingKeyConstants.KeyPrivacyAndPolicy,
                SettingKeyConstants.KeyPrivacyAndPolicyArabic
            }).ConfigureAwait(false);

            var privacyViewModel = new PrivacyAndPolicyViewModel
            {
                Privacy = privacySettings.GetValueOrDefault(SettingKeyConstants.KeyPrivacyAndPolicy),
                PrivacyArabic = privacySettings.GetValueOrDefault(SettingKeyConstants.KeyPrivacyAndPolicyArabic),
            };
            return privacyViewModel;
        }

        public async Task SetPrivacyPolicy(PrivacyAndPolicyViewModel model, CancellationToken cancellationToken)
        {
            var settingModels = new List<SettingViewModel>();
            AddSettingModel(SettingKeyConstants.KeyPrivacyAndPolicy, model.Privacy ?? string.Empty, settingModels);
            AddSettingModel(SettingKeyConstants.KeyPrivacyAndPolicyArabic, model.PrivacyArabic ?? string.Empty, settingModels);
            await SetInBulk(settingModels, cancellationToken);
        }

        public async Task<Dictionary<string, string>> GetByKeys(List<string> keys)
        {
            var allSettings = await GetAllSettingsAsync();
            var settingsByKeys = allSettings.ToDictionary(s => s.Key, s => s.Value);
            return settingsByKeys;
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

        //PRIVATE METHODS
        private async Task SetSettingData(SettingViewModel model, CancellationToken cancellationToken)
        {
            var adminId = AppHttpContext.GetAdminCurrentUserId();
            var user = await _unitOfWork.AdminServiceRepository.GetAdminByIdAsync(adminId) ?? throw new CustomException("User Not Found.");
            var existingSetting = await _unitOfWork.SettingRepository.GetSettingByKeyAsync(model.Key, cancellationToken);
            if (existingSetting == null)
            {
                var newSetting = new Setting
                {
                    Key = model.Key,
                    Value = model.Value
                };
                newSetting.SetActionBy(user);
                var createResult = await _unitOfWork.SettingRepository.AddSetting(newSetting, cancellationToken);
                if (!createResult) throw new CustomException("Failed to set a new setting.");
            }
            else
            {
                existingSetting.Value = model.Value;
                existingSetting.UpdatedAt = DateTime.UtcNow;
                existingSetting.SetActionBy(user);
                var updatedResult = await _unitOfWork.SettingRepository.UpdateSetting(existingSetting, cancellationToken);
                if (!updatedResult) throw new CustomException("Failed to update a setting.");
            }
        }
        private static void AddSettingModel(string key, string value, List<SettingViewModel> settingModels)
        {
            settingModels.Add(new SettingViewModel
            {
                Key = key,
                Value = value
            });
        }
    }
}
