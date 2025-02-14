using Maintenance.Application.Services.ServiceManager;
using Maintenance.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ILogger<CountryController> _logger;
        private readonly IServiceManager _serviceManager;

        public CountryController(ILogger<CountryController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            return View(new CountryDatatableFilterViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> GetFilteredCountries(CountryDatatableFilterViewModel model)
        {
            var result = await _serviceManager.CountryService.GetFilteredCountriesAsync(new CountryFilterViewModel
            {
                Name = model.Name,
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

    }
}