

using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.Services;

namespace AdminArea.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        private readonly IPageService _pageService;
        public HomeController(ILogger<HomeController> logger , IArticleService articleService , IPageService pageService)
        {
            _logger = logger;
            this._articleService = articleService;
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var  articles = await _articleService.GetActiveArticlesAsync();
            return View(articles);
        }

        [HttpGet("/{name}")]
        public async Task<IActionResult> Page(string name)
        {
            var pageViewModel = await _pageService.GetPageByTagAsync(name);

            if (pageViewModel == null)
            {
                return NotFound();
            }

            return View(pageViewModel);
        }


		[HttpGet]
        [Route("/Article/Detail/{id}")]
		public IActionResult Detail()
		{
			return View();
		}

      

        [HttpGet]
        public IActionResult Category()
        {
            return View();
        }


    }
}
