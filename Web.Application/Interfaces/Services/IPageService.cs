using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.PageContent;

namespace Web.Application.Interfaces.Services
{
    public interface IPageService
    {
        Task<List<PageToViewModel>> GetAllPagesAsync();
        Task<PageToViewModel?> AddPagesAsync(PageToCreateViewModel model);
        Task<PageToEditViewModel?> GetPageAsync(int id);
        Task<PageToViewModel?> UpdatePageAsync(PageToEditViewModel model);
        Task<PageParentModel?> GetPageByNameAsync(string name);
        Task<bool> DeletePageAsync(int Id);
      
    }
}
