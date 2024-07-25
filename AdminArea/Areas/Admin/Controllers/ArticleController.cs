using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Articles;
using System.IO;
using Web.Domian.Enums;

namespace AdminArea.Areas.Admin.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task <IActionResult> Detail(int id)
        {
            var article = await _articleService.GetArticle(id);
			if (article == null)
			{
				return NotFound();
			}
            return View(article);
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
                    Category = ((int)Enum.Parse(typeof(ArticleCategory),articles.Category!)).ToString(),
                    ImageUrl = articles.ImageUrl,
                };
                return View(updateArticle);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleToEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {   
                    string imageUrl = UploadedFile(model);
                    model.ImageUrl = imageUrl;
                } else
                {
                    var existingArticle = await _articleService.GetArticle(model.Id);
                    if (existingArticle != null)
                    {
                        model.ImageUrl = existingArticle.ImageUrl;
                    }
                }
                var edited = await _articleService.UpdateArticleAsync(model);
                if (edited != null)
                {
                    return RedirectToAction("List");
                }
            }
                return View(model);
            
        }

        private string UploadedFile(ArticleToCreateViewModel model)
        {
    
            string uniqueFileName = "Image/default-image.jpg";

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
                        uniqueFileName = "Image/" + uniqueFileName;
                    }
                    else
                    {
                        TempData["SizeError"] = "Image must be less than 1MB";
                        uniqueFileName = "default-image.jpg";
                    }
                }
                else
                {
                    TempData["ExtError"] = "Only jpg, jpeg and png images are allowed";
                    uniqueFileName = "default-image.jpg";
                }
            }
            return uniqueFileName;
        }

        private string UploadedFile(ArticleToEditViewModel model)
        {
            string defaultImagePath = "default-image.jpg";
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
                        uniqueFileName = "Image/" + uniqueFileName;
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
