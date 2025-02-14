using Domain.Enums;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Maintenance.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IServiceManager _serviceManager;

        public ClientController(ILogger<ClientController> logger, IServiceManager serviceManager)
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
            return View(new ClientDatatableFilterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredClients(ClientDatatableFilterViewModel model)
        {
            var result = await _serviceManager.AdminClientService.GetFilteredClientsAsync(new ClientFilterViewModel
            {
                FullName = model.FullName,
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
            return View(new ClientCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    await PrepareViewBags();
                    return View(model);
                }

                await _serviceManager.AdminClientService.CreateClientAsync(model, cancellationToken);
                this.NotifySuccess("Client created successfully.");
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
                _logger.LogError(ex, "Error adding client");
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
                var response = await _serviceManager.AdminClientService.GetClientForEditAsync(id, cancellationToken);
                return View(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on retrieving client details");
                this.NotifyError("Something went wrong. Please contact the administrator.");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClientEditViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.NotifyModelStateErrors();
                    await PrepareViewBags();
                    return View(model);
                }
                await _serviceManager.AdminClientService.EditClientAsync(model, cancellationToken);
                this.NotifySuccess("Client updated successfully.");
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
                _logger.LogError(ex, "Error updating client");
                this.NotifyError($"Error: {ex.Message}");
                await PrepareViewBags();
                return View(model);
            }
        }

        public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.AdminClientService.ActivateClientAsync(id, cancellationToken);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Client successfully activated.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on client activation");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact the administrator." }, Notify.Error.ToString());
            }
        }

        public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.AdminClientService.DeactivateClientAsync(id, cancellationToken);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Client successfully deactivated.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on client deactivation");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact the administrator." }, Notify.Error.ToString());
            }
        }
    }
}
