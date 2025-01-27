using Domain.Enums;
using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.Admin.AdminSpecification;
using Maintenance.Application.ViewModel;
using Maintenance.Application.ViewModel.User;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Maintenance.Web.Controllers
{

    public class UsersController : Controller
    {
        private readonly ILogger<IAdminService> _logger;
        private readonly IAdminService _adminService;

        public UsersController(ILogger<IAdminService> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = await _adminService.GetUsersForDropdown();
            return View(new UsersDatatableFilterViewModel());
        }

        public IActionResult Create()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredUsers([FromForm] UsersDatatableFilterViewModel model)
        {
            var response = await _adminService.GetFilteredUsers(new UserFilterViewModel
            {
                UserId = model.UserId,
                Skip = model.start,
                Take = model.length
            });

            var jsonData = new { draw = model.draw, recordsFiltered = response.TotalCount, recordsTotal = response.TotalCount, data = response.Data };
            return Ok(jsonData);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            try
            {
                await _adminService.CreateAdmin(model);
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

                var userResponse = await _adminService.GetAdminById(id);
                var editViewModel = new UpdateUserViewModel
                {
                    Id = id,
                    FirstName = userResponse.FirstName,
                    LastName = userResponse.LastName,
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
                await _adminService.EditAdminProfileAsync(model);

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
                await _adminService.BlockUser(id);
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
                await _adminService.UnblockUser(id);
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
                await _adminService.ChangePassword(model);
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
                await _adminService.ResetPassword(model);
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