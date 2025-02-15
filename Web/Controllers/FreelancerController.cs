using Domain.Enums;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            ViewBag.FreelancerServices = await _serviceManager.AdminSevService.GetAllServicesAsync().ConfigureAwait(false);
            ViewBag.Countries = await _serviceManager.CountryService.GetAllAsync().ConfigureAwait(false);
        }
        public IActionResult Index()
        {
            return View(new FreelancerDatatableFilterViewModel());
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
            return View(new FreelancerCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(FreelancerCreateViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    await PrepareViewBags();
                    return View(model);
                }

                await _serviceManager.AdminFreelancerService.CreateFreelancerAsync(model, cancellationToken);
                this.NotifySuccess("Freelancer created successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                await PrepareViewBags();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding freelancer");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                await PrepareViewBags();
                return View(model);
            }
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

        public async Task<IActionResult> Approve(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.AdminFreelancerService.ApproveFreelancerAsync(id, cancellationToken);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Successfully approved.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Freelancer Approve");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
            }
        }
        public async Task<IActionResult> Suspend(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.AdminFreelancerService.SuspendFreelancerAsync(id, cancellationToken);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Successfully suspended.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Freelancer Suspend");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
            }
        }
    }
}
