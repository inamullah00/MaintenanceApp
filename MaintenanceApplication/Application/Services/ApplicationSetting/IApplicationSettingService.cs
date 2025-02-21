using Maintenance.Application.ViewModel;

namespace Maintenance.Application.Services.ApplicationSetting
{
    public interface IApplicationSettingService
    {
        Task<AboutUsViewModel> GetAboutUs();
        Task<PrivacyAndPolicyViewModel> GetPrivacyPolicy();
        Task<TermsAndConditonsViewModel> GetTermsAndConditions();
        Task SetAboutUs(AboutUsViewModel model, CancellationToken cancellationToken);
        Task SetPrivacyPolicy(PrivacyAndPolicyViewModel model, CancellationToken cancellationToken);
        Task SetTermsAndConditions(TermsAndConditonsViewModel model, CancellationToken cancellationToken);
    }
}
