using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.PageContent;
using Web.Domian.Entities;

namespace Web.Application.Services
{
    public class PageContentService : IPageContentService
    {
        private readonly IPageContentRepository _pageContentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PageContentService(IPageContentRepository pageContentRepository, IUnitOfWork unitOfWork)
        {
            _pageContentRepository = pageContentRepository;
            _unitOfWork = unitOfWork;
        }

		public async Task<PageContentToViewModel?> AddPageContentAsync(PageContentCreateToViewModel model)
		{
            var pageContents = new PageContent()
            {
                Tag = model.Tag,
                Content = model.Content,
                PageId = model.PageId,
                Image = model.ImageUrl,

            };

             var added = await   _pageContentRepository.AddAsync(pageContents);
            var savedrecords = await _unitOfWork.SaveChangesAsync();
            if(savedrecords >0)
            {
                return new PageContentToViewModel
                {
                    Id = pageContents.Id,
                };
            }
            return null;
		}

		public async Task<bool> DeletePageContentAsync(int id)
        {
            var isDeleted = await _pageContentRepository.DeleteAsync(id);
            var savedRecored = await _unitOfWork.SaveChangesAsync();
            if (savedRecored > 0)
            {
                return true;
            }
            return false;
        }

		public async Task<List<PageContentToViewModel>> GetAllPageContentAsync()
		{
			var pageContents = await _pageContentRepository.GetAllAsync();
            return pageContents.Select(x => new PageContentToViewModel()
            {
                Id = x.Id,
                Content = x.Content,
                Tag = x.Tag,
                 PageId = x.PageId,
                ImageUrl = x.Image,
            }).ToList();
           
		}

		public async Task<PageContentEditToViewModel?> GetPageContentAsync(int id)
        {
            var contents = await _pageContentRepository.GetAsync(id);
            if (contents == null)
                return null;

            return new PageContentEditToViewModel()
            {
                Id = contents.Id,
                Tag = contents.Tag,
                Content = contents.Content,
                PageId = contents.PageId,
                ImageUrl = contents.Image,

            };
        }

        public async Task<PageContentToViewModel?> UpdatePageContentAsync(PageContentEditToViewModel model)
        {
            var edited = await _pageContentRepository.GetAsync(model.Id);
            if (edited == null)
            {
                return null;
            }

            edited.Tag = model.Tag;
            edited.Content = model.Content;
            edited.Image = model.ImageUrl;

            _pageContentRepository.UpdateAsync(edited);
            await _unitOfWork.SaveChangesAsync();

            return new PageContentToViewModel()
            {
                Id = edited.Id,
                Tag = edited.Tag,
                Content = edited.Content,
                PageId = edited.PageId,
                ImageUrl = edited.Image,
            };
        }


      



    }

}
