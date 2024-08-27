using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Domian.Entities;
using Web.Application.ViewModels.Admin.PageContent;
using Web.Application.ViewModels.Admin.Service;
using Web.Application.ViewModels.Admin.Articles;


namespace Web.Application.Services
{
    public class PageService : IPageService
    {

        private readonly IPageRepository _pageRepository;
        private readonly IServiceRepository _serviceRepository;
        
        private readonly IArticleRepository _articleRepository;
        private readonly IPageContentRepository pageContentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork, IPageContentRepository pageContentRepository, IServiceRepository serviceRepository , IArticleRepository articleRepository)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            this.pageContentRepository = pageContentRepository;
            _serviceRepository = serviceRepository;
            _articleRepository = articleRepository;
        }

        public async Task<PageToViewModel?> AddPagesAsync(PageToCreateViewModel model)
        {
            var page = new Page()
            {
                Name = model.Name,
                Tag = model.Tag,

            };
            var addedpages = await _pageRepository.AddAsync(page);
            var savedpages = await _unitOfWork.SaveChangesAsync();
            if (savedpages > 0)
            {
                return new PageToViewModel
                {
                    Name = page.Name,
                    Tag = page.Tag,
                };

            }
            return null;
        }

        public async Task<bool> DeletePageAsync(int Id)
        {
            var isDeleted = await _pageRepository.DeleteAsync(Id);
            var deletedRecored = await _unitOfWork.SaveChangesAsync();
            if (deletedRecored > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<PageToViewModel>> GetAllPagesAsync()
        {
            var pages = await _pageRepository.GetAllAsync();
            return pages.Select(x => new PageToViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Tag = x.Tag,
            }).ToList();
        }

        public async Task<PageToViewModel?> UpdatePageAsync(PageToEditViewModel model)
        {
            var updated = await _pageRepository.GetAsync(model.Id);
            if (updated == null)
            {
                return new PageToViewModel();
            }
            updated.Tag = model.Tag;
            updated.Name = model.Name;

            _pageRepository.UpdateAsync(updated);
            await _unitOfWork.SaveChangesAsync();

            return new PageToViewModel
            {
                Tag = updated.Tag,
                Name = updated.Name,
            };
        }

        public async Task<PageParentModel?> GetPageByNameAsync(string name)
        {
            var page = await _pageRepository.GetByNameAsync(name);

            if (page == null)
                return null;

            var pageContents = page.Contents?.Select(x => new PageContentToViewModel
            {
                Id = x.Id,
                Tag = x.Tag,
                Content = x.Content,
                PageId = x.PageId
            }).ToList();
            var homePage = await _pageRepository.GetByNameAsync("Home");
            var homeContents = homePage?.Contents?.Select(x => new PageContentToViewModel
            {
                Id = x.Id,
                Tag = x.Tag,
                Content = x.Content,
                PageId = x.PageId
            }).ToList();

            pageContents?.AddRange(homeContents!);
            var services = await _serviceRepository.GetServicesAsync();
            var serviceToViewModels = services?.Select(s => new ServiceToViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Icon = s.Icon,
                Content = s.Content
            }).ToList() ?? new List<ServiceToViewModel>();

            var articles = await _articleRepository.GetArticlesAsync();
            var Articles = articles?.Select(s => new ArticleToViewModel
            {
              Id = s.Id,
              Title = s.Title,
              Content = s.Content,
              IsPublished = s.IsPublished,
              ImageUrl = s.Image 
            }).ToList() ?? new List<ArticleToViewModel>();

            return new PageParentModel
            {
                PageModel = new PageToViewModel
                {
                    Id = page.Id,
                    Name = page.Name,
                    Tag = page.Tag,
                    Contents = pageContents ?? new List<PageContentToViewModel>(),
                },
               Services = serviceToViewModels,
                ServiceToModel = serviceToViewModels,
                ArticleToModel =  Articles
            };
        }
        public async Task<PageToEditViewModel?> GetPageAsync(int id)
        {
            var page = await _pageRepository.FindAsync(
                x => x.Id == id,
                includes: x => x.Contents!
            );

            if (page == null)
                return null;

            var pageContents = page.Contents?.Select(x => new PageContentToViewModel
            {
                Id = x.Id,
                Tag = x.Tag,
                Content = x.Content,
                PageId = x.PageId
            }).ToList();

         

            return new PageToEditViewModel
            {
                    Id = page.Id,
                    Name = page.Name,
                    Tag = page.Tag,
                    Contents = pageContents ?? new List<PageContentToViewModel>()
               
            };
        }
    }
       
}
 