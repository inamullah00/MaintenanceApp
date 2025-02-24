using Domain.Enums;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Maintenance.Web.Controllers
{
    public class PackageController : Controller
    {
        private readonly ILogger<PackageController> _logger;
        private readonly IServiceManager _serviceManager;

        public PackageController(ILogger<PackageController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        public async Task PrepareViewBag()
        {
            ViewBag.Freelancers = await _serviceManager.AdminFreelancerService.GetFreelancersForDropdown();

        }

        public async Task<IActionResult> Index()
        {
            await PrepareViewBag();
            return View(new PackageDatatableFilterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredPackages(PackageDatatableFilterViewModel model)
        {
            var result = await _serviceManager.AdminPackageService.GetFilteredPackagesAsync(new PackageFilterViewModel
            {
                Name = model.Name,
                FreelancerId = model.FreelancerId,
                PageNumber = (model.start / model.length) + 1,
                PageSize = model.length,
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
            return View(new PackageCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PackageCreateViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    await PrepareViewBag();
                    return View(model);
                }

                await _serviceManager.AdminPackageService.AddPackageAsync(model, cancellationToken);
                this.NotifySuccess("Package added successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                await PrepareViewBag();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding package");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                await PrepareViewBag();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await PrepareViewBag();
                var response = await _serviceManager.AdminPackageService.GetPackageForEditAsync(id, cancellationToken);
                return View(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service details");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PackageEditViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    await PrepareViewBag();
                    return View(model);
                }

                await _serviceManager.AdminPackageService.UpdatePackageAsync(model, cancellationToken);
                this.NotifySuccess("Package updated successfully.");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                await PrepareViewBag();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                await PrepareViewBag();
                return View(model);
            }
        }


        [HttpPatch]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.AdminPackageService.DeletePackageAsync(id, cancellationToken);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Package deleted successfully.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on service approval");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact the administrator." }, Notify.Error.ToString());
            }
        }
    }
}