using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Domian.Entities;
using Web.Application.ViewModels.Admin.PageContent;


namespace Web.Application.Services
{
    public class PageService : IPageService
    {

        private readonly IPageRepository _pageRepository;
        private readonly IPageContentRepository pageContentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork , IPageContentRepository pageContentRepository)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            this.pageContentRepository = pageContentRepository;
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
           var isDeleted   =  await _pageRepository.DeleteAsync(Id);
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

        public async Task<PageToViewModel?> GetPageAsync(int id)
        {
            var page = await _pageRepository.FindAsync(x=>x.Id == id, includes:(x=>x.Contents!));
            if (page == null)
                return null;

            var pageContents = page?.Contents?.Select(x => new PageContentToViewModel()
            {
                Id = x.Id,
                Tag = x.Tag,
                Content = x.Content,
                PageId = x.PageId
            }).ToList();

            return new PageToViewModel()
            {
                Id = page!.Id,
                Name = page.Name,
                Tag = page.Tag,
                Contents = pageContents??new List<PageContentToViewModel>()
            };

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

        public async Task<PageToViewModel?> GetPageByNameAsync(string name)
        {
            var page = await _pageRepository.GetByNameAsync(name);

            if (page == null)
                return null;

            var viewModel = new PageToViewModel
            {
                Id = page.Id,
                Name = page.Name,
                Tag = page.Tag,
                Contents = page.Contents?.Select(pc => new PageContentToViewModel
                {
                    Id = pc.Id,
                    Tag = pc.Tag,
                    Content = pc.Content,
                }).ToList()
            };

            return viewModel;
        }

    }

       
}
