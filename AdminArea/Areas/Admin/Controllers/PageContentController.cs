using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.PageContent;

namespace Web.Area.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class PageContentController : Controller
    {
        private readonly IPageContentService _pageContentService;

        public PageContentController(IPageContentService pageContentService)
        {
            _pageContentService = pageContentService;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var pageContent = await _pageContentService.DeletePageContentAsync(id);
            if (pageContent)
            {
                return RedirectToAction("List", "Page");
               
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var pageContent = await _pageContentService.GetPageContentAsync(id);
            if (pageContent == null)
            {
                return NotFound();
            }

            return View(pageContent);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PageContentEditToViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updatedContent = await _pageContentService.UpdatePageContentAsync(model);
            if (updatedContent != null)
            {
                return RedirectToAction("Edit","Page" , new { Id = updatedContent.PageId });
            }

            return View(model);
        }

    }
}
