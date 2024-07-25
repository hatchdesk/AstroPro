

using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.Services;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.Service;
using Web.Application.ViewModels.Consultation;
using Web.Domian.Entities;


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
                var page = await _pageService.GetPageByNameAsync(name);

                if(page == null)
                {
                    return NotFound();   
                }

                page.ConsultationToModel = new ConsultationSendToViewModel();
                page.ServiceToModel = services;
               
                return View("Consultation", page);
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
		public async Task<IActionResult> Detail(int id)
		{
            var article = await _articleService.GetArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            var model = new PageParentModel()
            {
                ArticleToModel = new List<ArticleToViewModel> { article }
            };

            return View(model);
        }


        [HttpGet]
        [Route("/Service/Detail/{id}")]
        public async Task<IActionResult> ServiceDetail(int id)
        {
            var service = await _serviceService.GetServiceAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var model = new PageParentModel()
            {
                ServiceToModel = new List<ServiceToViewModel> { service }
            };

            return View(model);
        }


    }
}
