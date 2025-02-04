using Domain.Enums;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Maintenance.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IServiceManager _serviceManager;

        public UsersController(ILogger<UsersController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Users = await _serviceManager.AdminService.GetUsersForDropdown();
            return View(new UsersDatatableFilterViewModel());
        }

        public IActionResult Create()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredUsers(UsersDatatableFilterViewModel model)
        {
            var result = await _serviceManager.AdminService.GetFilteredUsers(new UserFilterViewModel
            {
                UserId = model.UserId,
                Page = (model.start / model.length) + 1,
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            try
            {
                await _serviceManager.AdminService.CreateAdmin(model);
                this.NotifySuccess("User created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Creating User");
                this.NotifyError("Something went wrong. Please contact to administrator");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {

                var userResponse = await _serviceManager.AdminService.GetAdminById(id);
                var editViewModel = new UpdateUserViewModel
                {
                    Id = id,
                    FullName = userResponse.FullName,
                    EmailAddress = userResponse.EmailAddress,
                    PhoneNumber = userResponse.PhoneNumber,
                };

                return View(editViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Update User");
                this.NotifyError("Something went wrong. Please contact to administrator");
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewModel model)
        {
            try
            {
                await _serviceManager.AdminService.EditAdminProfileAsync(model);

                this.NotifySuccess("User updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Update User");
                this.NotifyError($"Error: {ex.Message}");
                return View(model);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> BlockUser(string id)
        {
            try
            {
                await _serviceManager.AdminService.BlockUser(id);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Successfully blocked user");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Block User");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
            }
        }


        [HttpPatch]
        public async Task<IActionResult> UnBlockUser(string id)
        {
            try
            {
                await _serviceManager.AdminService.UnblockUser(id);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Successfully unblocked user");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Unblock User");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
            }
        }

        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                await _serviceManager.AdminService.ChangePassword(model);
                this.NotifySuccess("Password Changed Successfully.");
                return RedirectToAction("Index", "Home");
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Change Password");
                this.NotifyError("Something went wrong. Please contact to administrator");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                await _serviceManager.AdminService.ResetPassword(model);
                return this.ApiSuccessResponse(HttpStatusCode.OK, "Password Reset Successfull.");
            }
            catch (CustomException ex)
            {
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on Reset Password");
                return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
            }
        }
    }
}