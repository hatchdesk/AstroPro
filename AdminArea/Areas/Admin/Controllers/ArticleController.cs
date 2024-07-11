using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Articles;
using System.IO;

namespace AdminArea.Areas.Admin.Controllers
{
    //[Authorize]
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticleController(IArticleService articleService, IWebHostEnvironment webHostEnvironment)
        {
            _articleService = articleService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var isDeleted = await _articleService.DeleteArticleAsync(Id);
            if (isDeleted)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleToCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = UploadedFile(model);
                model.ImageUrl = imageUrl;
                var article = await _articleService.AddArticleAsync(model);
                if (article != null)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var articles = await _articleService.GetArticle(Id);
            if (articles != null)
            {
                var updateArticle = new ArticleToEditViewModel
                {
                    Id = articles.Id,
                    Title = articles.Title,
                    Content = articles.Content,
                    IsPublished = articles.IsPublished,
                    Category = articles.Category,
                };
                return View(updateArticle);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleToEditViewModel model)
        {
            var edited = await _articleService.UpdateArticleAsync(model);
            if (edited != null)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        private string UploadedFile(ArticleToCreateViewModel model)
        {
            string defaultImagePath = "~/Image/default-image.jpg";
            string uniqueFileName = defaultImagePath;

            if (model.Image != null)
            {
                var ext = Path.GetExtension(model.Image.FileName).ToLowerInvariant();
                var size = model.Image.Length;

                if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                {
                    if (size <= 1000000)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            model.Image.CopyTo(fileStream);
                        }
                        uniqueFileName = Path.Combine("Image", uniqueFileName);
                    }
                    else
                    {
                        TempData["SizeError"] = "Image must be less than 1MB";
                        uniqueFileName = defaultImagePath;
                    }
                }
                else
                {
                    TempData["ExtError"] = "Only jpg, jpeg and png images are allowed";
                    uniqueFileName = defaultImagePath;
                }
            }
            return uniqueFileName;
        }
    }
}
