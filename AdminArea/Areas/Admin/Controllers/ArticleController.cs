using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Articles;

namespace AdminArea.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ArticleController : Controller
    {

        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService )
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async  Task<IActionResult> List()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return View(articles);
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var isDeleted  = await _articleService.DeleteArticleAsync(Id);
            if(isDeleted)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public  IActionResult Create()
        {  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleToCreateViewModel model)
        {
            var articles = await _articleService.AddArticleAsync(model);
          if(articles != null ) 
            {
                return RedirectToAction("List");
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
                    Id= articles.Id,
                    Title = articles.Title,
                    Content = articles.Content,
                    IsPublished  = articles.IsPublished,    
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
            if(edited != null )
            {
                return RedirectToAction("List");
            }
            return View();

        }
    }
}
