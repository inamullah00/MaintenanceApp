using Domain.Entity.UserEntities;
using Maintenance.API.Controllers.ClientController;
using Maintenance.Application.Exceptions;
using Maintenance.Application.ViewModel;
using Maintenance.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Maintenance.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ClientOrderController> _logger;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ClientOrderController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Login(string ReturnUrl = "")
        {
            var loginModel = new UserLoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };

            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await _userManager.Users.Where(a => a.UserName == model.UserName).FirstOrDefaultAsync() ?? throw new CustomException("Incorrect Username or Password");

                    var isSucceeded = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, true);

                    if (isSucceeded.Succeeded)
                    {
                        this.NotifySuccess("Logged In Successfully");
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {

                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home");

                    }
                    else if (isSucceeded.IsLockedOut)
                    {
                        return RedirectToAction(nameof(LockOut));
                    }
                    else
                    {
                        this.NotifyInfo($"Incorrect Username or Password . No of attemp remaining {5 - user.AccessFailedCount}");

                    }
                }
            }
            catch (CustomException ex)
            {
                this.NotifyInfo(ex.Message);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred on User Login.");
                this.NotifyError("Something went wrong. Please contact to administrator");
            }
            return RedirectToAction(nameof(Login));
        }



        public IActionResult LockOut()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            this.NotifySuccess("Logged Out Successfully");
            return RedirectToAction(nameof(Login));
        }
    }
}