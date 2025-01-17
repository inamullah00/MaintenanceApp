using Maintenance.Application.Exceptions;
using Maintenance.Application.Services.Account;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.Web.Controllers
{

    public class UsersController : Controller
    {
        private readonly ILogger<IRegisterationService> _logger;
        private readonly IRegisterationService _userService;

        public UsersController(ILogger<IRegisterationService> logger, IRegisterationService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _userService.UsersAsync(null, pageNumber, pageSize);

            if (!result.IsSuccess)
            {
                return View("Error", result.Message);
            }

            return View(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            try
            {
                var response = await _userService.CreateUser(model);
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
                _logger.LogError("Error on Creating User", ex);
                this.NotifyError("Something went wrong. Please contact to administrator");
                return View(model);
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    try
        //    {
        //        await SetRolesDropdown();
        //        var userResponse = await _userService.GetById(id);
        //        var editViewModel = new UpdateUserViewModel
        //        {
        //            Id = id,
        //            FullName = userResponse.FullName,
        //            EmailAddress = userResponse.EmailAddress,
        //            PhoneNumber = userResponse.PhoneNumber,
        //            UserName = userResponse.UserName,
        //            RoleId = userResponse.RoleId
        //        };

        //        return View(editViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error on Update User", ex);
        //        this.NotifyError("Something went wrong. Please contact to administrator");
        //        return RedirectToAction(nameof(Index));
        //    }

        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(UpdateUserViewModel model)
        //{
        //    try
        //    {
        //        var editDto = new UpdateUserViewModel
        //        {
        //            Id = model.Id,
        //            FullName = model.FullName,
        //            EmailAddress = model.EmailAddress,
        //            PhoneNumber = model.PhoneNumber,
        //            UserName = model.UserName,
        //            RoleId = model.RoleId
        //        };
        //        await _userService.Edit(editDto);
        //        this.NotifySuccess("User updated Successfully");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (CustomException ex)
        //    {
        //        await SetRolesDropdown();
        //        this.NotifyInfo(ex.Message);
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error on Update User", ex);
        //        await SetRolesDropdown();
        //        this.NotifyError("Something went wrong. Please contact to administrator");
        //        return View(model);
        //    }
        //}

        //[HttpPatch]
        //public async Task<IActionResult> BlockUser(string id)
        //{
        //    try
        //    {
        //        await _userService.BlockUser(id);
        //        return this.ApiSuccessResponse(HttpStatusCode.OK, "Successfully blocked user");
        //    }
        //    catch (CustomException ex)
        //    {
        //        return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error on Block User", ex);
        //        return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
        //    }
        //}


        //[HttpPatch]
        //public async Task<IActionResult> UnBlockUser(string id)
        //{
        //    try
        //    {
        //        await _userService.UnblockUser(id);
        //        return this.ApiSuccessResponse(HttpStatusCode.OK, "Successfully unblocked user");
        //    }
        //    catch (CustomException ex)
        //    {
        //        return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error on Unblock User", ex);
        //        return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
        //    }
        //}



        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    try
        //    {
        //        await _userService.ChangePassword(model);
        //        this.NotifySuccess("Password Changed Successfully.");
        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch (CustomException ex)
        //    {
        //        this.NotifyInfo(ex.Message);
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error on Change Password", ex);
        //        this.NotifyError("Something went wrong. Please contact to administrator");
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    try
        //    {
        //        await _userService.ResetPassword(model);
        //        return this.ApiSuccessResponse(HttpStatusCode.OK, "Password Reset Successfull.");
        //    }
        //    catch (CustomException ex)
        //    {
        //        return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { ex.Message }, Notify.Info.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error on Reset Password", ex);
        //        return this.ApiErrorResponse(HttpStatusCode.BadRequest, new List<string> { "Something went wrong. Please contact to administrator" }, Notify.Error.ToString());
        //    }
        //}
    }
}