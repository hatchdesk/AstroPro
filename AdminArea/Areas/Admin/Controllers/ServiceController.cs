using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Service;

namespace Web.Areas.Admin.Controllers
{

    //[Authorize]
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var services = await _serviceService.GetAllServiceAsync();
            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceToCreateViewModel model)
        {
            var createdService = await _serviceService.AddServicesAsync(model);
            if (createdService != null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }
    }
}
