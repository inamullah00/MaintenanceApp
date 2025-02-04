using Maintenance.Application.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;

namespace StarBooker.Web.Controllers
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
            ViewBag.Countries = await _serviceManager.CountryService.GetAllAsync().ConfigureAwait(false);
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> Pending()
        {
            return View();
        }

        public async Task<IActionResult> Approved()
        {
            return View();
        }

        public async Task<IActionResult> Rejected()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetFilteredFreelancers()
        {


            return View();
        }

        public async Task<IActionResult> Create()
        {
            await PrepareViewBags().ConfigureAwait(false);
            return View(new FreelancerCreateViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditBasicDetail()
        {
            return View();
        }

        public async Task<IActionResult> ViewDetails()
        {
            return View();
        }



        public async Task<IActionResult> Approve(int id)
        {
            return View();
        }


        [HttpPatch]
        public async Task<IActionResult> Reject(int id, string comment)
        {
            return View();
        }


        [HttpPatch]
        public async Task<IActionResult> Activate(int id)
        {
            return View();
        }

        [HttpPatch]
        public async Task<IActionResult> Deactivate(int id)
        {
            return View();
        }
    }
}
