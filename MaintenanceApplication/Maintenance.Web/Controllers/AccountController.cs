using AutoMapper;
using Maintenance.Application.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StarBooker.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IRegisterationService _registerationService;
        private readonly IMapper _mapper;

        public AccountController(IRegisterationService registerationService)
        {

            _registerationService = registerationService;

        }

    }
}