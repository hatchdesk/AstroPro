

using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.Service;
using Web.Application.ViewModels.Consultation;


namespace AdminArea.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        private readonly IServiceService _serviceService;
        private readonly IPageService _pageService;
        public HomeController(ILogger<HomeController> logger , IArticleService articleService , IPageService pageService , IServiceService serviceService)
        {
            _logger = logger;
            this._articleService = articleService;
            _pageService = pageService;
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            //var  articles = await _articleService.GetActiveArticlesAsync();
            var pageViewModel = await _articleService.GetHomePage();

            var HomeView = new PageParentModel()
            {
              ArticleToModel = pageViewModel!.ArticleToModel,
              ServiceToModel = pageViewModel!.ServiceToModel,
              PageModel = pageViewModel.PageModel,
              ConsultationToModel = pageViewModel.ConsultationToModel,
            };
            return View(HomeView);
        }

        public async Task<IActionResult> NavigationMenu()
        {
            var pages = await _pageService.GetAllPagesAsync();
            var pageViewModels = pages.Select(page => new PageToViewModel
            {
                Name = page.Name,
            }).ToList();

            return PartialView("_NavigationMenu", pageViewModels);
        }

        [HttpGet("/{name}")]
        public async Task<IActionResult> Page(string name)
        {

            if (name == "Consultation")
            {
				var services = await _serviceService.GetAllServiceAsync();
				var consultationModel = new PageParentModel
                {
                    ConsultationToModel = new ConsultationSendToViewModel(),
                    ServiceToModel = services
				};
                return View("Consultation", consultationModel);
            }


            var pageViewModel = await _pageService.GetPageByNameAsync(name);
            if (pageViewModel == null)
            {
                return NotFound();
            }

            return View("page" , pageViewModel);
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
