using System.Threading.Tasks;
using Web.Application.ViewModels.Admin.PageContent;

namespace Web.Application.Interfaces.Services
{
    public interface IPageContentService
    {
		public Task<PageContentToViewModel?> AddPageContentAsync(PageContentCreateToViewModel model);
		Task<List<PageContentToViewModel>> GetAllPageContentAsync();
		Task<bool> DeletePageContentAsync(int id);
        Task<PageContentEditToViewModel?> GetPageContentAsync(int id);
        Task<PageContentToViewModel?> UpdatePageContentAsync(PageContentEditToViewModel model);
       
    }
}
