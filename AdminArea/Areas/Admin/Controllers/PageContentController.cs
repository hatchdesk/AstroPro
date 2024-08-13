using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.Services;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.PageContent;

namespace AdminArea.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class PageContentController : Controller
    {
        private readonly IPageContentService _pageContentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PageContentController(IPageContentService pageContentService, IWebHostEnvironment webHostEnvironment)
        {
            _pageContentService = pageContentService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
		public async Task<IActionResult> List()
		{
			var pageContents  = await _pageContentService.GetAllPageContentAsync();
            return View(pageContents);
           
			
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create(PageContentCreateToViewModel model)
        {
            if (ModelState.IsValid)
            {

                var article = await _pageContentService.AddPageContentAsync(model);
                if (article != null)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
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

        //private string UploadedFile(PageContentCreateToViewModel model)
        //{

        //    string uniqueFileName = "Image/default-logo.png";

        //    if (model.brandlogo != null)
        //    {
        //        var ext = Path.GetExtension(model.brandlogo.FileName).ToLowerInvariant();
        //        var size = model.brandlogo.Length;

        //        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
        //        {
        //            if (size <= 1000000)
        //            {
        //                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");
        //                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.brandlogo.FileName;
        //                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //                using (var fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    model.brandlogo.CopyTo(fileStream);
        //                }
        //                uniqueFileName = "Image/" + uniqueFileName;
        //            }
        //            else
        //            {
        //                TempData["SizeError"] = "Image must be less than 1MB";
        //                uniqueFileName = "default-image.jpg";
        //            }
        //        }
        //        else
        //        {
        //            TempData["ExtError"] = "Only jpg, jpeg and png images are allowed";
        //            uniqueFileName = "default-image.jpg";
        //        }
        //    }
        //    return uniqueFileName;
        //}

    }
}
