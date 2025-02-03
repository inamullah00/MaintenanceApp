using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.Web.Controllers
{
    public class FreelancerController : Controller
    {
        private readonly ILogger<FreelancerController> _logger;
        private readonly IServiceManager _serviceManager;

        public FreelancerController(ILogger<FreelancerController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }
        private async Task PrepareViewBags()
        {
            ViewBag.Countries = await _serviceManager.CountryService.GetAllAsync().ConfigureAwait(false);
        }
        public IActionResult Index()
        {
            return View(new FreelancerDatatableFilterViewModel());
        }

        public async Task<IActionResult> Pending()
        {
            return View();
        }

        public async Task<IActionResult> Approved()
        {
            return View();
        }

        public async Task<IActionResult> Rejected()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetFilteredFreelancers(FreelancerDatatableFilterViewModel model)
        {
            var result = await _serviceManager.AdminFreelancerService.GetFilteredFreelancersAsync(new FreelancerFilterViewModel
            {
                FullName = model.FullName,
                AccountStatus = model.AccountStatus,
                PageNumber = (model.start / model.length) + 1,
                PageSize = model.length
            });

            return Json(new
            {
                draw = model.draw,
                recordsTotal = result.TotalCount,
                recordsFiltered = result.TotalCount,
                data = result.Data
            });
        }

        public async Task<IActionResult> Create()
        {
            await PrepareViewBags();
            return View(new FreelancerEditViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await PrepareViewBags();
                var response = await _serviceManager.AdminFreelancerService.GetFreelancerForEditAsync(id, cancellationToken);
                return View(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on retrieving freelancer details");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FreelancerEditViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    await PrepareViewBags();
                    return View(model);
                }
                await _serviceManager.AdminFreelancerService.EditFreelancerAsync(model, cancellationToken);
                this.NotifySuccess("Freelancer updated successfully.");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                await PrepareViewBags();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating freelancer");
                this.NotifyError($"Error: {ex.Message}");
                await PrepareViewBags();
                return View(model);
            }
        }

        public async Task<IActionResult> ViewDetails()
        {
            return View();
        }



        public async Task<IActionResult> Approve(int id)
        {
            return View();
        }


        [HttpPatch]
        public async Task<IActionResult> Reject(int id, string comment)
        {
            return View();
        }


        [HttpPatch]
        public async Task<IActionResult> Activate(int id)
        {
            return View();
        }

        [HttpPatch]
        public async Task<IActionResult> Deactivate(int id)
        {
            return View();
        }
    }
}
