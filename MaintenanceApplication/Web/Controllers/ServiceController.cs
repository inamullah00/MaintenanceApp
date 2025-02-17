using Domain.Enums;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Maintenance.Web.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceManager _serviceManager;

        public ServiceController(ILogger<ServiceController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            return View(new ServiceDatatableFilterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredServices(ServiceDatatableFilterViewModel model)
        {
            var isUserCreated = model.IsUserCreated == "true" ? true : model.IsUserCreated == "false" ? false : (bool?)null;
            var result = await _serviceManager.AdminSevService.GetFilteredServicesAsync(new ServiceFilterViewModel
            {
                Name = model.Name,
                IsUserCreated = isUserCreated,
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
            return View(new ServiceCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    return View(model);
                }

                await _serviceManager.AdminSevService.AddServiceAsync(model);
                this.NotifySuccess("Service added successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding service");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var response = await _serviceManager.AdminSevService.GetServiceForEditAsync(id);
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
        public async Task<IActionResult> Edit(ServiceEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    return View(model);
                }

                await _serviceManager.AdminSevService.UpdateServiceAsync(model);
                this.NotifySuccess("Service updated successfully.");
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                return View(model);
            }
        }

        public async Task<IActionResult> Approve(Guid id)
        {
            try
            {
                await _serviceManager.AdminSevService.ApproveServiceAsync(id);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Service successfully approved.");
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

        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                await _serviceManager.AdminSevService.ActivateServiceAsync(id);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Service successfully activated.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on service activation");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact the administrator." }, Notify.Error.ToString());
            }
        }

        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                await _serviceManager.AdminSevService.DeactivateServiceAsync(id);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Service successfully deactivated.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on service deactivation");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact the administrator." }, Notify.Error.ToString());
            }
        }
    }
}