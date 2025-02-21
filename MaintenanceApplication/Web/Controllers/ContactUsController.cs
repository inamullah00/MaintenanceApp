using Domain.Enums;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace StarBooker.Web.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ILogger<ContactUsController> _logger;
        private readonly IServiceManager _serviceManager;

        public ContactUsController(ILogger<ContactUsController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ContactUsFilterViewModel filter)
        {
            // Set default values if not provided
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageSize > 0 ? filter.PageSize : 10;

            // Fetch paginated results from the service
            var paginatedResult = await _serviceManager.ContactUsService.GetAllListAsync(filter);

            // Prepare the ViewModel
            var viewModel = new ContactUsIndexAndFilterViewModel
            {
                Filter = filter,
                Result = paginatedResult
            };

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetNotification()
        {
            var result = await _serviceManager.ContactUsService.GetPagedListAsync(new ContactUsFilterViewModel());
            var notificationView = this.RenderViewAsync("~/Views/ContactUs/_contactUsNotificationsPartial.cshtml", result, true).GetAwaiter().GetResult();
            return this.ApiSuccessResponse(HttpStatusCode.OK, "Notifications  retrieved successfully", notificationView);
        }
        [HttpGet]
        public async Task<IActionResult> GetNotificationCount()
        {
            try
            {
                var count = await _serviceManager.ContactUsService.NotificationCountAsync();
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Notification count fetched successfully.", count);
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Get Notification Count");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Failed to update ContactUs." }, Notify.Error.ToString());
            }
        }



        [HttpPatch]
        public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var notificationCount = await _serviceManager.ContactUsService.MarkAsRead(id, cancellationToken);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Notification  marked as read", notificationCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Mark As Read");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong.Please contact to administrator" }, Notify.Error.ToString());
            }
        }
    }
}