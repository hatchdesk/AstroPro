using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.PageContent;

namespace AdminArea.Areas.Admin.Controllers
{
    //[Authorize]
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageService _PageService;
        public PageController(IPageService PageService)
        {
            _PageService = PageService;
        }

        [HttpGet]
        public async Task<IActionResult>List()
        {
            var pages = await _PageService.GetAllPagesAsync();
            return View(pages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PageToCreateViewModel model)
        {
            var isAdded = await _PageService.AddPagesAsync(model);
            if(isAdded != null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var isDeleted = await _PageService.DeletePageAsync(Id);
            if (isDeleted)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var pageWithServices = await _PageService.GetPageAsync(Id);
            if (pageWithServices != null)
            {
                return View(pageWithServices);
            }
            return View(new PageParentModel());
        }



        [HttpPost]
        public async Task<IActionResult> Edit(PageToEditViewModel model)
        {

            var edited = await _PageService.UpdatePageAsync(model);
            if (edited != null)
            {
                return RedirectToAction("List");
            }
            return View();

        }
    }
}
