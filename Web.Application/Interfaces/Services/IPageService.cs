
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.PageContent;
namespace Web.Application.Interfaces.Services
{
    public interface IPageService
    {
        public Task<List<PageToViewModel>> GetAllPagesAsync();
        public Task<PageToViewModel?> AddPagesAsync(PageToCreateViewModel model);
        Task <PageToViewModel? > GetPageAsync(int id);
        
        Task<PageToViewModel?> UpdatePageAsync(PageToEditViewModel  model);

        Task<PageToViewModel?> GetPageByTagAsync(string tag);

        Task<bool> DeletePageAsync(int  Id);
    }
}
