using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.Service;

namespace AdminArea.Areas.Admin.Controllers
{

    [Authorize]
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
            if (ModelState.IsValid)
            {
                var createdService = await _serviceService.AddServicesAsync(model);
                if (createdService != null)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var EditedService = await _serviceService.GetServiceAsync(id);
            if (EditedService != null)
            {
                var updateddata = new ServiceToEditViewModel()
                {
                    Id = EditedService.Id,
                    Title = EditedService.Title,
                    Content = EditedService.Content,
                    Icon = EditedService.Icon

                };
                return View(updateddata);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceToEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isEdited = await _serviceService.UpdateServiceAsync(model);
                if (isEdited != null)
                {
                    return RedirectToAction("List");
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            var isDeleted = await _serviceService.DeleteServiceAsync(id);
            if (isDeleted)
            {
                return RedirectToAction("List");
            }
            return View();
        }


      

    }
}
