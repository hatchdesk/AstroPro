using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Application.Interfaces.Services;
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
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger , IArticleService articleService , IPageService pageService , IServiceService serviceService , HttpClient httpClient)
        {
            _logger = logger;
            this._articleService = articleService;
            _pageService = pageService;
            _serviceService = serviceService;
            _httpClient = httpClient;
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


        [HttpGet("Home/{name}")]
        public async Task<IActionResult> Page(string name , string selectedService)
        {
            if(name.ToLower() == "index")
            {
                return RedirectToAction("Index");
            }
            else if (name.ToLower() == "consultation")
            {
				var services = await _serviceService.GetAllServiceAsync();
				ViewBag.Services = services.Select(d => new SelectListItem()
				{
					Text = d.Title,
					Value = d.Id.ToString()
				});

				if(selectedService != null)
                {
					ViewBag.selectedService = selectedService;
				}
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
            var services = await _serviceService.GetAllServiceAsync();
     
            var article = await _articleService.GetArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            var pageViewModel = await _articleService.GetHomePage();

            var HomeView = new PageParentModel()
            {
                ArticleToModel = new List<ArticleToViewModel> { article },
                ServiceToModel = services,
                PageModel = pageViewModel!.PageModel,
            };

            return View(HomeView);
        }
               


        [HttpGet]
        [Route("/Service/Detail/{id}")]
        public async Task<IActionResult> ServiceDetail(int id)
        {
            var services = await _serviceService.GetAllServiceAsync();

            var service = await _serviceService.GetServiceAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var pageViewModel = await _articleService.GetHomePage();
            var HomeView = new PageParentModel()
            {
                ServiceToModel = new List<ServiceToViewModel> { service },
                PageModel = pageViewModel!.PageModel,
            };

            return View(HomeView);
        }

    }
}
