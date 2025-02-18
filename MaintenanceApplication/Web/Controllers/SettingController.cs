using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.Web.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public SettingController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> TermsAndCondition()
        {
            var termAndCondition = await _serviceManager.ApplicationSettingService.GetTermsAndConditions();
            return View(termAndCondition);
        }

        [HttpPost]
        public async Task<IActionResult> TermsAndCondition(TermsAndConditonsViewModel model, CancellationToken cancellationToken)
        {
            await _serviceManager.ApplicationSettingService.SetTermsAndConditions(model, cancellationToken);
            this.NotifySuccess("Saved Successfully");
            return RedirectToAction(nameof(TermsAndCondition));
        }

        public async Task<IActionResult> AboutUs()
        {
            var aboutUs = await _serviceManager.ApplicationSettingService.GetAboutUs();
            return View(aboutUs);
        }

        [HttpPost]
        public async Task<IActionResult> AboutUs(AboutUsViewModel model, CancellationToken cancellationToken)
        {
            await _serviceManager.ApplicationSettingService.SetAboutUs(model, cancellationToken);
            this.NotifySuccess("Saved Successfully");
            return RedirectToAction(nameof(AboutUs));
        }

        public async Task<IActionResult> PrivacyAndPolicy()
        {
            var privacy = await _serviceManager.ApplicationSettingService.GetPrivacyPolicy();
            return View(privacy);
        }

        [HttpPost]
        public async Task<IActionResult> PrivacyAndPolicy(PrivacyAndPolicyViewModel model, CancellationToken cancellationToken)
        {
            await _serviceManager.ApplicationSettingService.SetPrivacyPolicy(model, cancellationToken);
            this.NotifySuccess("Saved Successfully");
            return RedirectToAction(nameof(PrivacyAndPolicy));
        }

    }
}
